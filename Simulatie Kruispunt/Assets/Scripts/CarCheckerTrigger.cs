using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCheckerTrigger : MonoBehaviour {

	public GameObject Object;
	// Use this for initialization
	void Start () {

	}

	void OnTriggerEnter2D (Collider2D other)
	{
		Debug.Log ("Object Entered the trigger");
		//Object.transform.position.x = Object.transform.position.x+1;
	}

	void OnTriggerExit2D (Collider2D other)
	{
		Debug.Log ("Object left the trigger");
	}
}
