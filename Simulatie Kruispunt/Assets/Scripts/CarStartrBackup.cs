using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStartrBackup : MonoBehaviour {
    Car Car;
    Renderer rend;
    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        rend.material.color = new Color(255, 255, 255, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D other)
    {
        try
        {
            Car = other.gameObject.GetComponent<Car>();
        }
        catch
        {
        }
        if (Car != null)
        {
            Car.StartEngine();
        }
    }
}
