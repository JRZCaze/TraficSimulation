using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class JsonController : MonoBehaviour {

    public string ipAdress;
    public int adressPort;
    public bool LanTest;
    Socket sck;
    public trafficLightData CurrentData;
    TraficLightList Trafic;
    public string DataString;
    public List<triggers> CurrentTriggerStatus = new List<triggers>();
    Thread DataRetriever;
    Thread Bridge;
    public int tempCount;
    //private int msgRecieved; // 1 = trafic 2 = bridge 3 = timescaleverify
    trafficLightData resultTrafficLightData;
    bridgeData resultBridgeData;
    timeScaleVerify resultTimeScale;
    public GameObject bridge;
    Bridge BridgeScript;
    private bool wishedBridgeState;
   


    public GameObject TraficStance;
    // Use this for initialization
    void Start() {
        BridgeScript = bridge.GetComponent<Bridge>();
        Trafic = TraficStance.GetComponent<TraficLightList>();

        //threading
        ThreadStart DataRetrieve = new ThreadStart(DataRetrieval);
        Debug.Log("In Main: Creating the Child thread");
        DataRetriever = new Thread(DataRetrieve);
        if (LanTest)
        {
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ipAdress), adressPort);
            sck.Connect(endPoint);

            /*timeScale temp =  new timeScale();// dunno if ness
            temp.type = "TimeScaleData";
            temp.scale = 1;
            string msg = Newtonsoft.Json.JsonConvert.SerializeObject(temp) + "\n";
            byte[] msgBuffer = Encoding.Default.GetBytes(msg);
            sck.Send(msgBuffer, 0, msgBuffer.Length, 0);*/
        }
        for (int i = 0; i < 24; i++)//car triggers
        {
            int id = i / 2 + 1;

            if (i % 2 == 0)
            {
                CurrentTriggerStatus.Add(new triggers("PrimaryTrigger", false, "1." + id.ToString()));
            }
            else
            {
                CurrentTriggerStatus.Add(new triggers("SecondaryTrigger", false, "1." + id.ToString()));
            }           
        }
        for (int i = 0; i < 4; i++)// pedestrian triggers
        {
            for (int j = 0; j < 4; j++)
            {
                CurrentTriggerStatus.Add(new triggers("PrimaryTrigger", false, "3." + (i+1).ToString() + "." + (j+1).ToString()));
            }
        }

        for (int i = 0; i < 4; i++)// Bike triggers
        {
            CurrentTriggerStatus.Add(new triggers("PrimaryTrigger", false, "2." + (i + 1).ToString()));
        }

        for (int i = 0; i < 2; i++)// Boat
        {
            CurrentTriggerStatus.Add(new triggers("PrimaryTrigger", false, "4." + (i + 1).ToString()));
        }
        DataRetriever.Start();
    }

    // Update is called once per frame
    void Update() {

    }

    public void Send(triggers data)
    {
        string msg = Newtonsoft.Json.JsonConvert.SerializeObject(data) + "\n";
        //Debug.Log(msg);
        byte[] msgBuffer = Encoding.Default.GetBytes(msg);
        sck.Send(msgBuffer, 0, msgBuffer.Length, 0);
    }

    public void Send(bridgeDataSend data)
    {
        string msg = Newtonsoft.Json.JsonConvert.SerializeObject(data) + "\n";
        //Debug.Log(msg);
        byte[] msgBuffer = Encoding.Default.GetBytes(msg);
        sck.Send(msgBuffer, 0, msgBuffer.Length, 0);
    }

    void Recieve()//for thread
    {
        byte[] buffer = new byte[1023];
        if (buffer.Length > 0)
        {
            int rec = sck.Receive(buffer, 0, buffer.Length, 0);

            Array.Resize(ref buffer, rec);
            Debug.Log(Encoding.Default.GetString(buffer));
            Debug.Log(buffer);
            RecieveData(Encoding.Default.GetString(buffer));
        }
        //Debug.Log(Encoding.Default.GetString(buffer));
        //Debug.Log(buffer);
    }

    public void SendData()//test
    {
        string json = Newtonsoft.Json.JsonConvert.SerializeObject(CurrentTriggerStatus)+"\n";
        Debug.Log(json);
    }

    public void SendData(string data)//test
    {
        string json = Newtonsoft.Json.JsonConvert.SerializeObject(data) + "\n";
        Debug.Log("change");
    }

    public trafficLightData TraficLightinfo()
    {
        return CurrentData;
    }

    public void UpdateTriggerData(string Id, string type, bool status)//real
    {
        //Debug.Log(Id);
        int possition = CurrentTriggerStatus.FindIndex(x => x.id == Id && x.type == type);
        //try
        //{
            CurrentTriggerStatus[possition].triggered = status;
            //Debug.Log(CurrentTriggerStatus[possition].id);
            if (LanTest)
            {
                Send(CurrentTriggerStatus[possition]);
            }
        //}
        //catch { };
    }

    public void RecieveData(string Data)//real
    {
        try
        {
            resultTrafficLightData = JsonConvert.DeserializeObject<trafficLightData>(Data);
            if (resultTrafficLightData.trafficLights == null)
            {
                resultTrafficLightData = null;
            }
        }
        catch
        {
        }
        try { resultBridgeData = JsonConvert.DeserializeObject<bridgeData>(Data);
        }
        catch {
        }
        try {
             resultTimeScale = JsonConvert.DeserializeObject<timeScaleVerify>(Data);
            }
        catch { }
            
        Debug.Log(Data);
        CurrentData = JsonConvert.DeserializeObject<trafficLightData>(DataString);
        if (resultTrafficLightData != null && CurrentData != null)
        {
            for (int i = 0; i < CurrentData.trafficLights.Count; i++)
            {
                foreach (var resultItem in resultTrafficLightData.trafficLights)
                {
                    if (CurrentData.trafficLights[i].id == resultItem.id)
                    {
                        CurrentData.trafficLights[i] = resultItem;
                    }
                }
            }
        }
        DataString = Newtonsoft.Json.JsonConvert.SerializeObject(CurrentData);
        if (resultTrafficLightData != null)
        {
            for (int i = 0; i < resultTrafficLightData.trafficLights.Count; i++)
            {
                Debug.Log(resultTrafficLightData.trafficLights[i].id);
                Debug.Log(resultTrafficLightData.trafficLights[i].lightStatus);
            }
        }
        if (resultBridgeData != null && resultBridgeData.bridgeOpen != BridgeScript.BridgeOpen && resultBridgeData.type == "BridgeData") // bridgestuff
        {
            wishedBridgeState = resultBridgeData.bridgeOpen;
            ThreadStart BridgeRetrieve = new ThreadStart(BridgeUpdate);
            Debug.Log("In Main: Creating the Child thread");
            Bridge = new Thread(BridgeRetrieve);
            Bridge.Start();
        }

        if (resultTimeScale != null)
        {
            if (!resultTimeScale.status)
            {
                Debug.Log("give another timescale");
            }
            else
            {
                Debug.Log("it works");
            }
        }
        resultTimeScale = null;
        resultBridgeData = null;
        resultTrafficLightData = null;
    }

    private void BridgeUpdate()
    {
        System.Threading.Thread.Sleep(5000);
        BridgeScript.ChangeBridgeState(wishedBridgeState);
        JsonController.bridgeDataSend dataset = new JsonController.bridgeDataSend();
        dataset.type = "BridgeStatusData";
        dataset.opened = BridgeScript.BridgeOpen;
        Send(dataset);
    }

    public void connection()// test
    {
        triggers TestDataSet = new triggers("PrimaryTrigger", true, "1.1");
        CurrentTriggerStatus.Add(TestDataSet);
        Send(TestDataSet);
        //Recieve();
    }
    public void GenerateDataTemplate(string data)
    {
        DataString = data;
    }
    public void DataRetrieval()
    {
        if (LanTest)
        {
            while (true)
            {
                Debug.Log("startscan");
                Recieve();
                Debug.Log("stopscan");
            }
        }
    }

    public bool RetrieveBridgePos()
    {
        return resultBridgeData.bridgeOpen;
    }
    // classes
    public class triggers
    {
        public string type;
        public bool triggered;
        public string id;

        public triggers(string Type, bool Triggered, string Id)
        {
            type = Type;
            triggered = Triggered;
            id = Id;
        }
    }

    [System.Serializable]
    public class bridgeDataSend
    {
        public string type{ get; set; }
        public bool opened { get; set; }
    }

    public class bridgeData
    {
        public string type { get; set; }
        public bool bridgeOpen { get; set; }
    }

    public class trafficLightData
    {
        public string type { get; set; }
        public List<trafficLights> trafficLights { get; set; }
    }

    public class timeScaleVerify
    {
        public string type { get; set; }
        public bool status { get; set; }
    }

    public class timeScale
    {
        public string type { get; set; }
        public int scale { get; set; }
    }

    public class trafficLights
    {
        public string lightStatus;
        public string id;
        public trafficLights(string Id, string LightStatus)
        {
            lightStatus = LightStatus;
            id = Id;
        }
    }
}
