using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraficLightList : MonoBehaviour {
    // Use this for initialization

        public JsonController.trafficLightData localData = new JsonController.trafficLightData();

        public GameObject jsonController;

        JsonController DataController;
        string dataSet = "green"; 

    void Start () {

        DataController = jsonController.GetComponent<JsonController>();

        JsonController.trafficLightData CurrentData = new JsonController.trafficLightData();
        CurrentData.type = "TrafficLightData";
        List<JsonController.trafficLights> CurrentTraficLights = new List<JsonController.trafficLights>();
        CurrentTraficLights.Add(new JsonController.trafficLights("1.1", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("1.2", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("1.3", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("1.4", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("1.5", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("1.6", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("1.7", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("1.8", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("1.9", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("1.10", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("1.11", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("1.12", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("1.13", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("2.1", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("2.2", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("2.3", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("2.4", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("3.1.1", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("3.1.2", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("3.1.3", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("3.1.4", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("3.2.1", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("3.2.2", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("3.2.3", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("3.2.4", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("3.3.1", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("3.3.2", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("3.3.3", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("3.3.4", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("3.4.1", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("3.4.2", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("3.4.3", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("3.4.4", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("4.1", "red"));
        CurrentTraficLights.Add(new JsonController.trafficLights("4.2", "red"));
        //{ "type":"TrafficLightData","trafficLights":[{"lightStatus":"green","id":"1.1"},{"lightStatus":"green","id":"3.1.2"},{"lightStatus":"green","id":"2.4"}]}
        CurrentData.type = "TrafficLightData";
        CurrentData.trafficLights = CurrentTraficLights;
        //DataController.GenerateDataTemplate(CurrentData);
        localData = CurrentData;
        dataSet = Newtonsoft.Json.JsonConvert.SerializeObject(CurrentData);
        DataController.GenerateDataTemplate(dataSet);

        //Debug.Log(CurrentData.type);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
