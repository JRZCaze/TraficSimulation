using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour {

    public int CurrentAngle;
    public int StartAngle;

    public float Speed;
    public float MaxSpeed;
    public float TurnPos_X;
    public float TurnPos_Y;
    public float margingTurnCheck;

    public bool TurnRight;
    public bool TurnLeft;

    private Boat behindBoat;
    private bool CanWalk;
    private int addAngle;
    private float StandingStillCount;
    private float StandingStillX;
    private float StandingStillY;

    void Start()
    {
        StandingStillX = transform.position.x;
        StandingStillY = transform.position.y;
        transform.eulerAngles = new Vector3(0, 0, StartAngle);
        CanWalk = true;
        CurrentAngle = StartAngle;

    }

    // Update is called once per frame
    void Update()
    {
        StandingStillCount++;

        transform.eulerAngles = new Vector3(0, 0, CurrentAngle);
        transform.Translate(Speed / 100, 0, 0);
        if (TurnLeft)
        {
            RotateLeft();
        }
        if (TurnRight)
        {
            RotateRight();
        }
        if (CanWalk)
        {
            Speed = MaxSpeed;
        }
        if (!CanWalk)
        {
            Speed = 0;
        }
        if (StandingStillCount == 5)
        {
            if (StandingStillX == transform.position.x && StandingStillY == transform.position.y)
            {
                Speed = 0;
            }
            StandingStillX = transform.position.x;
            StandingStillY = transform.position.y;
            StandingStillCount = 0;
        }
        if (transform.position.x < -100 || transform.position.x > 100 || transform.position.y < -100 || transform.position.y > 100)
        {
            //behindCar = null;
            DestroyGameObject();
        }
    }
    // mb add a second collision box for the front and back part to pref multimple checks
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.transform.position.x < transform.position.x && other.gameObject.name.Contains("boat") /*&& transform.position.y == other.gameObject.transform.position.y*/)
        {
            behindBoat = other.gameObject.GetComponent<Boat>();
            behindBoat.StopEngine();
            //Debug.Log ("Engine stop");
        }
        if (CurrentAngle >= 135 && CurrentAngle < 225)
        {
            //if (other.gameObject.transform.position.x > transform.position.x && other.gameObject.name.Contains("boat") /*&& transform.position.y == other.gameObject.transform.position.y*/)
            //{
            //    behindBoat = other.gameObject.GetComponent<Boat>();
            //    behindBoat.StopEngine();
            //    //Debug.Log ("Engine stop");
            //}
        }
        if (CurrentAngle >= 225 && CurrentAngle < 315)
        {
            if (other.gameObject.transform.position.y > transform.position.y && other.gameObject.name.Contains("boat") /*&& transform.position.x == other.gameObject.transform.position.x*/)
            {
                behindBoat = other.gameObject.GetComponent<Boat>();
                behindBoat.StopEngine();
                //Debug.Log ("Engine stop");
            }
        }
        if (CurrentAngle >= 45 && CurrentAngle < 135)
        {
            if (other.gameObject.transform.position.y < transform.position.y && other.gameObject.name.Contains("boat") /*&& transform.position.x == other.gameObject.transform.position.x*/)
            {
                behindBoat = other.gameObject.GetComponent<Boat>();
                behindBoat.StopEngine();
                //Debug.Log ("Engine stop");
            }
        }
        //Debug.Log ("Collison");
        //Debug.Log (other.gameObject.name);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.transform.position.x < transform.position.x && other.gameObject.name.Contains("boat"))
        {
            //Car behindCar = other.gameObject.GetComponent<Pedestrian> ();
            behindBoat.StartEngine();
            //Debug.Log ("Engine start");
        }
    }

    public void StartEngine()
    {
        CanWalk = true;
    }

    public void StopEngine()
    {
        CanWalk = false;
    }
    void RotateLeft()
    {
        if (addAngle != 90)
        {
            CurrentAngle += 1;
            addAngle++;
        }
        else
        {
            TurnLeft = false;
            addAngle = 0;
        }
    }
    void RotateRight()
    {
        if (addAngle != 90)
        {
            CurrentAngle -= 1;
            addAngle++;
        }
        else
        {
            TurnRight = false;
            addAngle = 0;
        }
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
