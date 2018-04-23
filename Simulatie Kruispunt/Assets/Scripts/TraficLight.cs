using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraficLight : MonoBehaviour {

	public bool IsConnection;
	//private BoxCollider2D myBoxCollider;
	private CapsuleCollider2D myCapsuleCollider;
	private Car Car;
    private Car Car2;
    private Car Car3;
    private Car Car4;
    private Pedestrian Pedestrian;
    private Pedestrian Pedestrian2;
    private Pedestrian PedestrianTemp;
    private Bike Bike;
    private Bike Bike2;
    private Bike BikeTemp;
    private Boat Boat;
    private Boat BoatTemp;
    public Sprite Red;
    public Sprite Green;
    public bool greenLight;
    Renderer rend;
    private SpriteRenderer spriteRenderer;


    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //rend = GetComponent<Renderer>();
        //rend.material.SetColor("_SpecColor", Color.green);
        myCapsuleCollider = GetComponent<CapsuleCollider2D> ();
	}
	// Update is called once per frame
	void Update () {
        try
        {
            if (greenLight && Bike != null)
            {
                Bike.StartEngine();
                Bike = null;
            }
        }
        catch
        {
        }
        try
        {
            if (greenLight && Car != null)
            {
                Car.StartEngine();
                Car2.StartEngine();
                Car2 = null;
                Car = null;
            }
        }
        catch
        {
        }
        try
        {
            if (greenLight && Pedestrian != null)
            {
                Pedestrian.StartEngine();
                Pedestrian = null;
            }
        }
        catch
        {
        }
        try
        {
            if (greenLight && Boat != null)
            {
                Boat.StartEngine();
                Boat = null;
            }
        }
        catch
        {
        }
    }

	public void TraficLightState(bool greenLight)
	{
		this.greenLight = greenLight;
		if (!greenLight) {
			myCapsuleCollider.isTrigger = true;
            spriteRenderer.sprite = Red;
            //Debug.Log ("green");
        } 
		else {
			myCapsuleCollider.isTrigger = false;
            spriteRenderer.sprite = Green;
            //Debug.Log ("red");
        }
	}
    void OnTriggerStay2D(Collider2D other)
    {
        try
        {
            if (Car == null && Car != other)
            {
                Car = other.gameObject.GetComponent<Car>();
                Car2 = other.gameObject.GetComponent<Car>();
                Car3 = other.gameObject.GetComponent<Car>();
                Car4 = other.gameObject.GetComponent<Car>();
            }
            if (Car == Car2 && other != Car)
            {
                Car2 = other.gameObject.GetComponent<Car>();
            }
            if (Car == Car3 || Car == Car2 && other != Car || other != Car2)
            {
                Car3 = other.gameObject.GetComponent<Car>();
            }
            if (Car == Car4 || Car == Car3 || Car == Car2 && other != Car || other != Car2 || other != Car3)
            {
                Car4 = other.gameObject.GetComponent<Car>();
            }
        }
        catch
        {
        }
        try { PedestrianTemp = other.gameObject.GetComponent<Pedestrian>(); }
        catch
        {
        }
        try { BikeTemp = other.gameObject.GetComponent<Bike>(); }
        catch
        {
        }
        try { BoatTemp = other.gameObject.GetComponent<Boat>(); }
        catch {}

        if (PedestrianTemp != null)
        {
            Pedestrian = PedestrianTemp;
        }
        if (Pedestrian != null && PedestrianTemp != Pedestrian)
        {
            Pedestrian2 = PedestrianTemp;
        }
        if (BikeTemp != null)
        {
            Bike = BikeTemp;
        }
        if (Bike != null && BikeTemp != Bike)
        {
            Bike2 = BikeTemp;
        }
        if (BoatTemp != null)
        {
            Boat = BoatTemp;
        }


        if (greenLight && Car!= null) 
		{
            try
            {
                Car.StartEngine();
            }
            catch
            {
            }
            try
            {
                Car2.StartEngine();
            }
            catch
            {
            }
            try
            {
                Car3.StartEngine();
            }
            catch
            {
            }
            try
            {
                Car4.StartEngine();
            }
            catch
            {
            }
        } else if (Car != null) {
            try
            {
                Car.StopEngine();
            }
            catch
            {
            }
            try
            {
               Car2.StopEngine();
            }
            catch
            {
            }
            try
            {
                Car3.StopEngine();
            }
            catch
            {
            }
            try
            {
                Car4.StopEngine();
            }
            catch
            {
            }
        }

        if (greenLight && Pedestrian != null)
        {
            try
            {
                Pedestrian.StartEngine();
            }
            catch
            {
            }
            try
            {
                Pedestrian2.StartEngine();
            }
            catch
            {
            }
        }
        else if(Pedestrian != null)
        {
            try
            {
                Pedestrian.StopEngine();
            }
            catch
            {
            }
            try
            {
                Pedestrian2.StopEngine();
            }
            catch
            {
            }
        }
        if (greenLight && Bike != null)
        {
            try
            {
                Bike.StartEngine();
            }
            catch
            {
            }
            try
            {
                Bike2.StartEngine();
            }
            catch
            {
            }
        }
        else if (Bike != null)
        {
            try
            {
                Bike.StopEngine();
            }
            catch
            {
            }
            try
            {
                Bike2.StopEngine();
            }
            catch
            {
            }
        }

        if (greenLight && Boat != null)
        {
            Boat.StartEngine();
        }
        else if (Boat != null)
        {
            Boat.StopEngine();
        }


    }

	void OnTriggerExit2D (Collider2D other)
	{
        try { Car.StartEngine(); }
        catch
        {
        }
        try { Pedestrian.StartEngine(); }
        catch
        {
        }
        try { Bike.StartEngine(); }
        catch
        {
        }
        try { Boat.StartEngine(); }
        catch { }
		//Debug.Log ("its out gogogo");
	}
}