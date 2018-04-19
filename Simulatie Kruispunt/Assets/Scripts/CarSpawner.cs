using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour {


	public GameObject Object;
	public float spawnRate = 2f;

	float nextSpawn = 0.0f;
    Renderer rend;

    Vector2 whereToSpawn;
	// Use this for initialization
	void Start () 
	{
        rend = GetComponent<Renderer>();
        rend.material.color = new Color(255, 255, 255, 0);
    }

	// Update is called once per frame
	void Update () 
	{
		if (Time.time > nextSpawn)
		{
			nextSpawn = Time.time + spawnRate;
		
			whereToSpawn = new Vector2 (transform.position.x, transform.position.y);
			Instantiate (Object, whereToSpawn, Quaternion.identity);
		}
	}

}
