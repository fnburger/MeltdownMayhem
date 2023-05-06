using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainPlayerScript : MonoBehaviour
{

    public int playerID;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //print(hit.gameObject.name);
    }

    void OnTriggerEnter(Collider other)
    {
        print("entered");
    }

    void OnTriggerStay(Collider other)
    {
        //print("trigger stay"); // will be executed every frame while player is still inside trigger
    }

    void OnTriggerExit(Collider other)
    {
        print("exited");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseItem()
    {
        Debug.Log("Player " + playerID + " used an item!");
        // check if player has an item, if no play a sound
        // else:
            // check which item was used and call the specific effect function inside the corresponding game object version
         
    }
}
