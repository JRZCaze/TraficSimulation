using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityTrigger : MonoBehaviour {
    Renderer rend;
    private Bike Bike;
    private Car Car;
    private Pedestrian Pedestrian;
    private Boat Boat;
	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        rend.material.color = new Color(255, 255, 255, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D other)
    {
        try
        {
            Boat = other.gameObject.GetComponent<Boat>(); ;
        }
        catch { }
        try
        {
            Car = other.gameObject.GetComponent<Car>(); ;
        }
        catch { }
        try
        {
            Pedestrian = other.gameObject.GetComponent<Pedestrian>(); ;
        }
        catch { }
        try
        {
            Bike = other.gameObject.GetComponent<Bike>(); ;
        }
        catch { }
        //destroy
        try
        {
            Boat.DestroyGameObject();
        }
        catch { }
        try
        {
            Car.DestroyGameObject();
        }
        catch { }
        try
        {
            Pedestrian.DestroyGameObject();
        }
        catch { }
        try
        {
            Bike.DestroyGameObject();
        }
        catch { }


    }
}
