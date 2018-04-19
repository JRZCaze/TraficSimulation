using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraficLightTrigger : MonoBehaviour {

	void Start () {

	}

	void OnTriggerEnter2D (Collider2D other)
	{
		Debug.Log ("Object Entered the trigger");
	}

	void OnTriggerExit2D (Collider2D other)
	{
		Debug.Log ("Object left the trigger");
	}
	// Use this for initialization
	
	// Update is called once per frame
	//void Update () {
		
	//}
}
