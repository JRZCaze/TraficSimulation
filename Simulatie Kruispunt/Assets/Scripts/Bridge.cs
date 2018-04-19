using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour {

    public GameObject jsonController;
    JsonController DataController;
    //public Sprite OpenBridge;
    public bool BridgeOpen;
    private SpriteRenderer spriteRenderer;
    private int delay;
    private bool changed;
    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        DataController = jsonController.GetComponent<JsonController>();
    }

	
	// Update is called once per frame
	void Update () {
        if (BridgeOpen)
        {
            spriteRenderer.enabled = true;
        }
        else if (!BridgeOpen)
        {
            spriteRenderer.enabled = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (BridgeOpen)
        {
            Debug.Log("Splash");
        }
    }

    public void ChangeBridgeState(bool state) {
        BridgeOpen = state;
    }
}
