using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    //test bools 
    //triggers to check if the trigger is in contact with a car & traficlights to enable red light
    public bool SendDataSet;
    public bool ConnectionTest;
    public JsonController.trafficLightData CurrentData;
    public bool[] TriggersActiveCar = new bool[24]; // has a copy in jsoncontroller
    public bool[] TriggersActivePedestrians = new bool[16];
    public bool[] TriggersActiveBike = new bool[4];
    public bool[] TriggersActiveBoat = new bool[2];
    public bool[] CarTraficLightStatus = new bool[13];
    public bool[] PedestrianTraficLightStatus = new bool[16];
    public bool[] BikeTraficLightStatus = new bool[4];
    public bool[] BoatTraficLightStatus = new bool[2];
    //add GameObject per trigger checker
    public GameObject jsonController;
    JsonController DataController;

    GameObject Bridge;
    Bridge BridgeScript;

    //trafic light data

    GameObject[] CarTrafficLights = new GameObject[13];
    List<TraficLight> CarTraficLightScripts = new List<TraficLight>();

    GameObject[] PedestrianTrafficLights = new GameObject[16];
    List<TraficLight> PedestrianTraficLightScripts = new List<TraficLight>();

    GameObject[] BikeTrafficLights = new GameObject[4];
    List<TraficLight> BikeTraficLightScripts = new List<TraficLight>();

    GameObject[] BoatTrafficLights = new GameObject[2];
    List<TraficLight> BoatTraficLightScripts = new List<TraficLight>();

    // triggers

    GameObject[] TriggersCar = new GameObject[24];
    List<Trigger> TriggerScriptsCar = new List<Trigger>();

    GameObject[] TriggersPedestrians = new GameObject[16];
    List<Trigger> TriggerScriptsPedestrians = new List<Trigger>();

    GameObject[] TriggersBike = new GameObject[4];
    List<Trigger> TriggerScriptsBike = new List<Trigger>();

    GameObject[] TriggersBoat = new GameObject[4];
    List<Trigger> TriggerScriptsBoat = new List<Trigger>();


    // connecting to the object and script
    void Start () {

        DataController = jsonController.GetComponent<JsonController>();
        //simulation
        // searching all traficlights and getting them ready for use 
        for (int i = 0; i < CarTraficLightStatus.Length; i++) //  car
        {
            CarTrafficLights[i] = GameObject.FindGameObjectWithTag("TraficLight" + (i + 1));
            GameObject temp = CarTrafficLights[i];
            CarTraficLightScripts.Add(temp.GetComponent<TraficLight>());
        }

        for (int i = 0; i < BikeTraficLightStatus.Length; i++) //  bike
        {
            BikeTrafficLights[i] = GameObject.FindGameObjectWithTag("TraficLight2." + (i + 1)+"B");
            GameObject temp = BikeTrafficLights[i];
            BikeTraficLightScripts.Add(temp.GetComponent<TraficLight>());
        }

        for (int i = 0; i < BoatTraficLightStatus.Length; i++) //  boat
        {
            BoatTrafficLights[i] = GameObject.FindGameObjectWithTag("BoatTraficLight" + (i + 1));
            GameObject temp = BoatTrafficLights[i];
            BoatTraficLightScripts.Add(temp.GetComponent<TraficLight>());
        }

        for (int k = 0; k < 4; k++)// pedestrians
        {
            for (int j = 0; j < 4; j++)
            {
                int i = ((k) * 4) + j;
                int a = k + 1;
                int b = j + 1;
                PedestrianTrafficLights[i] = GameObject.FindGameObjectWithTag("TraficLight" + a + "." + b + "P");
                GameObject temp = PedestrianTrafficLights[i];
                PedestrianTraficLightScripts.Add(temp.GetComponent<TraficLight>());
            }
        }

        // searching all triggers and getting them ready for use 
        for (int i = 0; i < TriggersActiveCar.Length; i++)//car
        {
            if (i%2 == 0)
            {
                TriggersCar[i] = GameObject.FindGameObjectWithTag("Trigger" + (i/2 + 1) + "P");
                GameObject temp = TriggersCar[i];
                TriggerScriptsCar.Add(temp.GetComponent<Trigger>());
            }
            else
            {
                TriggersCar[i] = GameObject.FindGameObjectWithTag("Trigger" + (i / 2 + 1) + "S");
                GameObject temp = TriggersCar[i];
                TriggerScriptsCar.Add(temp.GetComponent<Trigger>());
            }
        }

        for (int i = 0; i < TriggersActivePedestrians.Length; i++)// pedestrians
        {
            for (int k = 0; k < 4; k++)
            {
                for (int j = 0; j < 4; j++)
                {
                    try
                    {
                        TriggersPedestrians[i] = GameObject.FindGameObjectWithTag("Trigger" + (k + 1) + "." + (j + 1) + "P");
                        GameObject temp = TriggersPedestrians[i];
                        TriggerScriptsPedestrians.Add(temp.GetComponent<Trigger>());
                    }
                    catch { }
                }
            }
        }

        for (int i = 0; i < 4; i++)// Bike
        {
            TriggersBike[i] = GameObject.FindGameObjectWithTag("Trigger2." + (i + 1) + "B");
            GameObject temp = TriggersBike[i];
            TriggerScriptsBike.Add(temp.GetComponent<Trigger>());

        }

        for (int i = 0; i < 2; i++)// Boat
        {
            TriggersBoat[i] = GameObject.FindGameObjectWithTag("BoatTrigger" + (i + 1));
            GameObject temp = TriggersBoat[i];
            TriggerScriptsBoat.Add(temp.GetComponent<Trigger>());
        }
    }
	
	// Update is called once per frame
	void Update () {
        CurrentData = DataController.TraficLightinfo();
        try
        {
            //ChangeBridgeState(DataController.RetrieveBridgePos());
        }
        catch
        {
        }
        if (ConnectionTest)
        {
            DataController.SendData();// is gonna b Send()
            ConnectionTest = false;
        }

        for (int i = 0; i < CarTraficLightStatus.Length; i++)//car
        {
            CarTraficLightScripts[i].TraficLightState(CarTraficLightStatus[i]);
        }
        for (int i = 0; i < PedestrianTraficLightStatus.Length; i++)//pedestrian
        {
            PedestrianTraficLightScripts[i].TraficLightState(PedestrianTraficLightStatus[i]);
        }
        for (int i = 0; i < BikeTraficLightStatus.Length; i++)//bike
        {
            BikeTraficLightScripts[i].TraficLightState(BikeTraficLightStatus[i]);
        }
        for (int i = 0; i < BoatTraficLightStatus.Length; i++)//boat
        {
            BoatTraficLightScripts[i].TraficLightState(BoatTraficLightStatus[i]);
        }

        for (int i = 0; i < TriggersActiveCar.Length; i++) // car
        {
            int id = i / 2 + 1;

            if (TriggersActiveCar[i] != TriggerScriptsCar[i].IsActive())
            {
                if (i % 2 == 0)
                {
                    DataController.UpdateTriggerData("1." + id.ToString(), "PrimaryTrigger", TriggerScriptsCar[i].IsActive());
                }
                else
                {
                    DataController.UpdateTriggerData("1." + id.ToString(), "SecondaryTrigger", TriggerScriptsCar[i].IsActive());
                }
            }
            TriggersActiveCar[i] = TriggerScriptsCar[i].IsActive();
        }

        for (int i = 0; i < 16; i++)// pedestrian
        {
            if (TriggersActivePedestrians[i] != TriggerScriptsPedestrians[i].IsActive())
            {
                var count = (i + 1);
                var temp = 1;
                if (count > 4)
                {
                    count -= 4;
                    temp += 1;
                }
                if (count > 4)
                {
                    count -= 4;
                    temp += 1;
                }
                if (count > 4)
                {
                    count -= 4;
                    temp += 1;
                }
                DataController.UpdateTriggerData("3." + temp + "." + count, "PrimaryTrigger", TriggerScriptsPedestrians[i].IsActive());
            }
            TriggersActivePedestrians[i] = TriggerScriptsPedestrians[i].IsActive();
        }


        for (int i = 0; i < 4; i++) // Bike
        {
            if (TriggersActiveBike[i] != TriggerScriptsBike[i].IsActive())
            {
                var index = i + 1;
                DataController.UpdateTriggerData("2." + index, "PrimaryTrigger", TriggerScriptsBike[i].IsActive());
            }
            TriggersActiveBike[i] = TriggerScriptsBike[i].IsActive();
        }

        for (int i = 0; i < 2; i++) // Boat
        {
            if (TriggersActiveBoat[i] != TriggerScriptsBoat[i].IsActive())
            {
                var index = i + 1;
                DataController.UpdateTriggerData("4." + index, "PrimaryTrigger", TriggerScriptsBoat[i].IsActive());
            }
            TriggersActiveBoat[i] = TriggerScriptsBoat[i].IsActive();
        }


        //Json connection
        if (SendDataSet) // all
        {
            DataController.SendData();
            SendDataSet = false;
        }
        RecievedDataUpdate();

    }

    //public void ChangeBridgeState(bool state)
    //{
    //    Bridge = GameObject.FindGameObjectWithTag("bridge");
    //    BridgeScript = Bridge.GetComponent<Bridge>();
    //    BridgeScript.ChangeBridgeState(state);
    //}
    // distributes the id's to the correct lists
    void RecievedDataUpdate () // needs refactor
    {
        if (CurrentData != null)
        {
            if (CurrentData.trafficLights != null)
            {
                for (int i = 0; i < CurrentData.trafficLights.Count; i++)
                {
                    if (CurrentData.type.ToLower() == "trafficlightdata")
                    {
                        string temp = "";
                        int check = 0;
                        int Lane = 0;
                        int Number = 0;
                        if (CurrentData.trafficLights[i].id[0] == '1')
                        {
                            temp = CurrentData.trafficLights[i].id.Replace("1.", "");// voor als niet alles word aangeleverd
                            check = 1;
                        }
                        else if (CurrentData.trafficLights[i].id[0] == '2')
                        {
                            Lane = CurrentData.trafficLights[i].id[2] -'0';
                            check = 2;
                        }
                        else if (CurrentData.trafficLights[i].id[0] == '3')
                        {
                            Lane = CurrentData.trafficLights[i].id[2] - '0';
                            Number = CurrentData.trafficLights[i].id[4] - '0';
                            check = 3;
                        }
                        else if (CurrentData.trafficLights[i].id[0] == '4')
                        {
                            Lane = CurrentData.trafficLights[i].id[2] - '0';
                            //Number = CurrentData.trafficLights[i].id[4] - '0';
                            check = 4;
                        }






                        // data update
                        if (CurrentData.trafficLights[i].lightStatus == "green" && check == 1) //  cars
                        {
                            CarTraficLightStatus[i] = true;
                        }
                        else if (CurrentData.trafficLights[i].lightStatus == "red" && check == 1)
                        {
                            CarTraficLightStatus[i] = false;
                        }
                        else if(CurrentData.trafficLights[i].lightStatus == "green" && check == 2) // bike
                        {
                            BikeTraficLightStatus[Lane-1] = true;
                        }
                        else if (CurrentData.trafficLights[i].lightStatus == "red" && check == 2)
                        {
                            BikeTraficLightStatus[Lane-1] = false;
                        }
                        else if (CurrentData.trafficLights[i].lightStatus == "green" && check == 3 && Lane !=0 && Number !=0) //  pedestrian
                        {
                            PedestrianTraficLightStatus[(Lane - 1) * 4 + Number - 1] = true;
                        }
                        else if (CurrentData.trafficLights[i].lightStatus == "red" && check == 3 && Lane != 0 && Number != 0)
                        {
                            PedestrianTraficLightStatus[(Lane - 1) * 4 + Number - 1] = false;
                        }
                        else if (CurrentData.trafficLights[i].lightStatus == "green" && check == 4 && Lane != 0) //  pedestrian
                        {
                            BoatTraficLightStatus[(Lane - 1)] = true;
                        }
                        else if (CurrentData.trafficLights[i].lightStatus == "red" && check == 4 && Lane != 0)
                        {
                            BoatTraficLightStatus[(Lane - 1)] = false;
                        }
                    }
                }
            }
        }
    }
}
