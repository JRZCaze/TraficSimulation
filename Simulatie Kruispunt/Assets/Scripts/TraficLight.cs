using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraficLight : MonoBehaviour {

	public bool IsConnection;
	//private BoxCollider2D myBoxCollider;
	private CapsuleCollider2D myCapsuleCollider;
	private Car Car;
    private Car Car2;
    private Pedestrian Pedestrian;
    private Bike Bike;
    private Boat Boat;
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
            }
            if (Car == Car2 && other != Car)
            {
                Car2 = other.gameObject.GetComponent<Car>();
            }
        }
        catch
        {
        }
        try { Pedestrian = other.gameObject.GetComponent<Pedestrian>(); }
        catch
        {
        }
        try { Bike = other.gameObject.GetComponent<Bike>(); }
        catch
        {
        }
        try { Boat = other.gameObject.GetComponent<Boat>(); }
        catch { Debug.Log("unknown entity"); }
        

		if (greenLight && Car!= null) 
		{
			Car.StartEngine ();
		} else if (Car != null) {
			Car.StopEngine ();
            try
            {
               Car2.StopEngine();
            }
            catch
            {
            }
        }

        if (greenLight && Pedestrian != null)
        {
            Pedestrian.StartEngine();
        }
        else if(Pedestrian != null)
        {
            Pedestrian.StopEngine();
        }

        if (greenLight && Bike != null)
        {
            Bike.StartEngine();
        }
        else if(Bike != null)
        {
            Bike.StopEngine();
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