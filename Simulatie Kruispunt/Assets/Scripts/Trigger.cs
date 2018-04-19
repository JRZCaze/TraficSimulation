using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

	public bool active;
    Renderer rend;
    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        rend.material.color = new Color(255, 255, 255, 0);
    }
	
	void OnTriggerEnter2D (Collider2D other)
	{
		//Debug.Log ("Object Entered the trigger");
		active = true;
	}

	void OnTriggerExit2D (Collider2D other)
	{
		//Debug.Log ("Object left the trigger");
		active = false;
	}

	public bool IsActive()
	{
		return active;
	}
}
