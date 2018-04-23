using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

    //private Rigidbody2D myRigidbody;
    //private GameObject mySelf;
    public float CurrentAngle;
    public int StartAngle;
    public float Speed;
    public float MaxSpeed;
    //public float Y_speed2;
    //public float X_speed2;
    //public float Y_speed;
    //public float X_speed;
    public float TurnPos_X;
    public float TurnPos_Y;
    public float margingTurnCheck;

    private Car behindCar;
    private bool CanDrive;

    private float addAngle;
    private float turnAngle;
    private float turnSpeed;
    public bool TurnRight;
    public bool TurnLeft;

    private float StandingStillCount;
    private float StandingStillCount2;
    private float StandingStillX;
    private float StandingStillY;
    private float minspeed;
    public float random;

    //public GameObject Controller;
    //Controller DataController;

    //public float TurnPos_Y;
    // Use this for initialization
    void Start () 
	{
        //myRigidbody = GetComponent<Rigidbody2D> ();
        //mySelf = GetComponent<GameObject>();
        minspeed = 1 / 1000;
        StandingStillX = transform.position.x;
		StandingStillY = transform.position.y;
        transform.eulerAngles = new Vector3(0, 0, StartAngle);
        CanDrive = true;
        CurrentAngle = StartAngle;
        random = Random.Range(1, 5);
    }
	
	// Update is called once per frame
	void Update () 
	{
        StandingStillCount++;
        StandingStillCount2++;

        if (!CanDrive)
        {
            StandingStillCount2++;
        }

        if (StandingStillCount2 > 100)
        {
            CanDrive = true;
            StandingStillCount2 = 0;
        }

        transform.eulerAngles = new Vector3(0, 0, CurrentAngle);
        transform.Translate(Speed / 100, 0, 0);
        if (TurnLeft)
        {
            RotateLeft(turnAngle, turnSpeed);
        }
        if (TurnRight)
        {
            RotateRight(turnAngle, turnSpeed);
        }
        if (CanDrive)
        {
            if (Speed <= MaxSpeed)
            {
                Speed += MaxSpeed / 100;
            }
            else
            {
                Speed = MaxSpeed;
            }
        }
        if (!CanDrive)
        {
            if (Speed != minspeed)
            {
                if (Speed < 0)
                {
                    Speed = minspeed;
                }
                else
                {
                    Speed -= MaxSpeed / 20;
                }
            }
        }
        if (StandingStillCount == 5) {
			if (StandingStillX == transform.position.x && StandingStillY == transform.position.y) {
                //float speedtemp = 1/10000;
                Speed = 1 / 10000;
			} 
			//StandingStillX = transform.position.x;
			//StandingStillY = transform.position.y;
			StandingStillCount = 0;
		}
        if (transform.position.x < -100 || transform.position.x > 100 || transform.position.y < -100 || transform.position.y > 100)
        {
            //behindCar = null;
            DestroyGameObject();
        } 
	}
    // mb add a second collision box for the front and back part to pref multimple checks

    void OnCollisionEnter2D(Collision2D other)
    {
        //DestroyGameObject();
        Debug.Log("BOOOOM");
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (CurrentAngle < 45 || CurrentAngle >= 315)
        {
            if (other.gameObject.transform.position.x < transform.position.x && (other.gameObject.name.Contains("Car") || other.gameObject.name.Contains("Bus")) /*&& transform.position.y == other.gameObject.transform.position.y*/)
            {
                behindCar = other.gameObject.GetComponent<Car>();
                behindCar.StopEngine();
                //Debug.Log ("Engine stop");
            }
        }
        else if (CurrentAngle >= 135 && CurrentAngle < 225)
        {
            if (other.gameObject.transform.position.x > transform.position.x && (other.gameObject.name.Contains("Car") || other.gameObject.name.Contains("Bus")) /*&& transform.position.y == other.gameObject.transform.position.y*/)
            {
                behindCar = other.gameObject.GetComponent<Car>();
                behindCar.StopEngine();
                //Debug.Log ("Engine stop");
            }
        }
        else if (CurrentAngle >= 225 && CurrentAngle < 315)
        {
            if (other.gameObject.transform.position.y > transform.position.y && (other.gameObject.name.Contains("Car") || other.gameObject.name.Contains("Bus")) /*&& transform.position.x == other.gameObject.transform.position.x*/)
            {
                behindCar = other.gameObject.GetComponent<Car>();
                behindCar.StopEngine();
                //Debug.Log ("Engine stop");
            }
        }
        else if (CurrentAngle >= 45 && CurrentAngle < 135)
        {
            if (other.gameObject.transform.position.y < transform.position.y && (other.gameObject.name.Contains("Car") || other.gameObject.name.Contains("Bus")) /*&& transform.position.x == other.gameObject.transform.position.x*/)
            {
                behindCar = other.gameObject.GetComponent<Car>();
                behindCar.StopEngine();
                //Debug.Log ("Engine stop");
            }
        }
        behindCar = null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        /*if (CurrentAngle >= 135 && CurrentAngle < 225)
        {
            if (other.gameObject.transform.position.x > transform.position.x && (other.gameObject.name.Contains("Car") || other.gameObject.name.Contains("Bus")) /*&& transform.position.y == other.gameObject.transform.position.y*///)
            /*{
                //behindCar = other.gameObject.GetComponent<Car>();
                behindCar.StartEngine();
                //Debug.Log ("Engine stop");
            }
        }*/
    }

        void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.name.Contains("Car") || other.gameObject.name.Contains("Bus")) 
		{
            //Car behindCar = other.gameObject.GetComponent<Car> ();
            try
            {
            behindCar = other.gameObject.GetComponent<Car>();
            behindCar.StartEngine();
            behindCar = null;
            }
            catch
            {
            }
			//Debug.Log ("Engine start");
		}
    }
		
	public void StartEngine()
	{
		CanDrive = true;
	}

    public void ChangeMaxSpeed(float speed)// temp
    {
        MaxSpeed = speed;
    }

    public void StopEngine()
	{
		CanDrive = false;
	}
    public void RotateLeft(float angle, float speed)
    {
        if (addAngle != angle)
        {
            CurrentAngle += speed;
            addAngle+= speed;
        }
        else
        {
            TurnLeft = false;
            addAngle = 0;
        }
        if (addAngle > angle)
        {
            addAngle = angle;
        }
    }
    public void RotateRight(float angle, float speed)
    {
        if (addAngle != angle)
        {
            CurrentAngle -= speed;
            addAngle+= speed;
        }
        else
        {
            TurnRight = false;
            addAngle = 0;
        }

        if (addAngle > angle)
        {
            addAngle = angle;
        }
    }
    public void Rotate(float angle, float speed)
    {
        if (angle < 0)
        {
            turnAngle = angle * -1;
            turnSpeed = speed;
            TurnLeft = true;
        }
        else
        {
            turnAngle = angle;
            turnSpeed = speed;
            TurnRight = true;
        }
    }

    public void ForceRotate(float angle, float speed)
    {
        CurrentAngle = angle;
        TurnLeft = false;
        TurnRight = false;
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    public float randommatic3000()
    {
        return random;
    }
}