using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bike : MonoBehaviour {

    public float CurrentAngle;
    public int StartAngle;

    public float Speed;
    public float MaxSpeed;
    public float TurnPos_X;
    public float TurnPos_Y;
    public float margingTurnCheck;

    public bool TurnRight;
    public bool TurnLeft;

    private Pedestrian behindPedestrian;
    private bool CanWalk;
    private int addAngle;
    private float StandingStillCount;
    private float StandingStillX;
    private float StandingStillY;
    private float turnAngle;
    private float turnSpeed;

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
        if (other.gameObject.transform.position.x < transform.position.x && other.gameObject.name.Contains("Pedestrian") /*&& transform.position.y == other.gameObject.transform.position.y*/)
        {
            behindPedestrian = other.gameObject.GetComponent<Pedestrian>();
            behindPedestrian.StopEngine();
            //Debug.Log ("Engine stop");
        }
        if (CurrentAngle >= 135 && CurrentAngle < 225)
        {
            if (other.gameObject.transform.position.x > transform.position.x && other.gameObject.name.Contains("Pedestrian") /*&& transform.position.y == other.gameObject.transform.position.y*/)
            {
                behindPedestrian = other.gameObject.GetComponent<Pedestrian>();
                behindPedestrian.StopEngine();
                //Debug.Log ("Engine stop");
            }
        }
        if (CurrentAngle >= 225 && CurrentAngle < 315)
        {
            if (other.gameObject.transform.position.y > transform.position.y && other.gameObject.name.Contains("Pedestrian") /*&& transform.position.x == other.gameObject.transform.position.x*/)
            {
                behindPedestrian = other.gameObject.GetComponent<Pedestrian>();
                behindPedestrian.StopEngine();
                //Debug.Log ("Engine stop");
            }
        }
        if (CurrentAngle >= 45 && CurrentAngle < 135)
        {
            if (other.gameObject.transform.position.y < transform.position.y && other.gameObject.name.Contains("Pedestrian") /*&& transform.position.x == other.gameObject.transform.position.x*/)
            {
                behindPedestrian = other.gameObject.GetComponent<Pedestrian>();
                behindPedestrian.StopEngine();
                //Debug.Log ("Engine stop");
            }
        }
        //Debug.Log ("Collison");
        //Debug.Log (other.gameObject.name);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.transform.position.x < transform.position.x && other.gameObject.name.Contains("Pedestrian"))
        {
            //Car behindCar = other.gameObject.GetComponent<Pedestrian> ();
            behindPedestrian.StartEngine();
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
        if (addAngle != turnAngle)
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
        if (addAngle != turnAngle)
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

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
