using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainPlayerScript : MonoBehaviour
{

    public int lapNumber; // which lap the player is currently on. starts with 1
    public int checkpointIndex; // which checkpoint the player last passed. starts with 0.
    public Transform currentCheckpoint; // transform of the checkpoint the player last passed. gets updated when passing through checkpoints.
    public PlayerInput player; // reference to PlayerInput object of this component's owning player
    public GameManager gm;

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
        player = this.transform.parent.GetComponentInChildren<PlayerInput>();
        lapNumber = 1;
        checkpointIndex = 0;
        GameObject gmo = GameObject.Find("GameManager");
        gm = gmo.GetComponent<GameManager>();
    }

    public void MoveToCurrentCheckpoint()
    {
        if (!currentCheckpoint)
        {
            // move to start point instead
            gm.MovePlayerToStart(player);
            //gm.EnablePlayerController(player);
        }
        else
        {
            Transform tr = currentCheckpoint;
            tr.position = new Vector3(59, 9, -1);
            gm.MovePlayerToPosition(player, tr);
            // move player to last checkpoint position
            //Debug.Log("Moving a player back to last checkpoint");
            
            //Debug.Log("Checkpoint rotation: " + currentCheckpoint.rotation);
            //Transform playerParent = player.transform.parent;
            //playerParent.transform.position = currentCheckpoint.TransformPoint(0, 0, 0);
            //playerParent.transform.position = new Vector3(59, 8, -1);
            //Debug.Log("New position: " + playerParent.transform.position);
            //playerParent.transform.rotation = currentCheckpoint.rotation;
        }
        
    }

    // gets triggered when the jump animation starts
    void JumpEvent()
    {
        // TODO: play spin effect
        
    }
}
