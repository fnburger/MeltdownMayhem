using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerScript : MonoBehaviour
{

    public int lapNumber; // which lap the player is currently on. starts with 1
    public int checkpointIndex; // which checkpoint the player last passed. starts with 0.

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //print(hit.gameObject.name);
    }

    void OnTriggerEnter(Collider other)
    {
        //print("entered");
    }

    void OnTriggerStay(Collider other)
    {
        //print("trigger stay"); // will be executed every frame while player is still inside trigger
    }

    void OnTriggerExit(Collider other)
    {
        //print("exited");
    }

    // Start is called before the first frame update
    void Start()
    {
        lapNumber = 1;
        checkpointIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // gets triggered automatically when the jump animation starts
    void JumpEvent()
    {
        // TODO: play spin effect
        Debug.Log("SPIN SFX");
    }
}
