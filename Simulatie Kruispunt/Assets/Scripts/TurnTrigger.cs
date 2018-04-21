using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTrigger : MonoBehaviour {

    public string target;
    public float turnAngle;
    public float turnSpeed;
    public float forcedAngle;
    public bool RandomDistribute;
    public float TurnOption1;
    public float TurnOption2;
    public float TurnOption3;
    public float TurnOption4;
    public int random1;
    public int random2;
    public int random3;
    public int random4;
    public bool EitherOr;
    public float Either;
    public float Or;
    Renderer rend;

    float random;
    Car Car;
    Bike Bike;
    Pedestrian Pedestrian;
    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        rend.material.color = new Color(255, 255, 255, 0);
    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains(target))
        {
            try
            {
                Car = other.gameObject.GetComponent<Car>();
                random = Car.randommatic3000();
            }
            catch
            {
            }
            try
            {
                Bike = other.gameObject.GetComponent<Bike>();
            }
            catch
            {
            }
            try
            {
                Pedestrian = other.gameObject.GetComponent<Pedestrian>();
            }
            catch
            {
            }
            if (EitherOr && (random == random1 || random == random2))
            {
                if (turnAngle != 0 && RandomDistribute == false)
                {
                    try
                    {
                        Car.Rotate(Either, turnSpeed);
                    }
                    catch
                    {

                    }
                    try
                    {
                        Pedestrian.Rotate(Either, turnSpeed);
                    }
                    catch
                    {

                    }
                    try
                    {
                        Bike.Rotate(Either, turnSpeed);
                    }
                    catch
                    {

                    }
                }
            }
            if (EitherOr && (random == random3 || random == random4))
            {
                if (turnAngle != 0 && RandomDistribute == false)
                {
                    try
                    {
                        Car.Rotate(Or, turnSpeed);
                    }
                    catch
                    {

                    }
                    try
                    {
                        Pedestrian.Rotate(Or, turnSpeed);
                    }
                    catch
                    {

                    }
                    try
                    {
                        Bike.Rotate(Or, turnSpeed);
                    }
                    catch
                    {

                    }
                }
            }

            if (EitherOr ==  false)
            {
                if (turnAngle != 0 && RandomDistribute == false)
                {
                    try
                    {
                        Car.Rotate(turnAngle, turnSpeed);
                    }
                    catch
                    {

                    }
                    try
                    {
                        Pedestrian.Rotate(turnAngle, turnSpeed);
                    }
                    catch
                    {

                    }
                    try
                    {
                        Bike.Rotate(turnAngle, turnSpeed);
                    }
                    catch
                    {

                    }
                }
                else if (RandomDistribute == false)
                {
                    try
                    {
                        Car.ForceRotate(forcedAngle, turnSpeed);
                    }
                    catch
                    {
                    }
                    try
                    {
                        Pedestrian.ForceRotate(forcedAngle, turnSpeed);
                    }
                    catch
                    {
                    }
                    try
                    {
                        Bike.ForceRotate(forcedAngle, turnSpeed);
                    }
                    catch
                    {
                    }
                }
            }

            if (RandomDistribute == true)
            {
                //Debug.Log(random);
                if (random == 1)
                {
                    try
                    {
                        Car.ForceRotate(TurnOption1, turnSpeed);
                    }
                    catch
                    {
                    }
                    try
                    {
                        Pedestrian.ForceRotate(TurnOption1, turnSpeed);
                    }
                    catch
                    {
                    }
                    try
                    {
                        Bike.ForceRotate(TurnOption1, turnSpeed);
                    }
                    catch
                    {
                    }
                }
                else if (random == 2)
                {
                    try
                    {
                        Car.ForceRotate(TurnOption2, turnSpeed);
                    }
                    catch
                    {
                    }
                    try
                    {
                        Pedestrian.ForceRotate(TurnOption2, turnSpeed);
                    }
                    catch
                    {
                    }
                    try
                    {
                        Bike.ForceRotate(TurnOption2, turnSpeed);
                    }
                    catch
                    {
                    }
                }
                else if (random == 3)
                {
                    try
                    {
                        Car.ForceRotate(TurnOption3, turnSpeed);
                    }
                    catch
                    {
                    }
                    try
                    {
                        Pedestrian.ForceRotate(TurnOption3, turnSpeed);
                    }
                    catch
                    {
                    }
                    try
                    {
                        Bike.ForceRotate(TurnOption3, turnSpeed);
                    }
                    catch
                    {
                    }
                }
                else if (random == 4)
                {
                    try
                    {
                        Car.ForceRotate(TurnOption4, turnSpeed);
                    }
                    catch
                    {
                    }
                    try
                    {
                        Pedestrian.ForceRotate(TurnOption4, turnSpeed);
                    }
                    catch
                    {
                    }
                    try
                    {
                        Bike.ForceRotate(TurnOption4, turnSpeed);
                    }
                    catch
                    {
                    }
                }
            }

        }
    }
}
