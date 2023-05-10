using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Checkpoint : MonoBehaviour
{
    [Tooltip("You must set this manually in the editor. The checkpoints have to be visited in ascending order. The first checkpoint must have index 1. The next one 2 and so on.")]
    public int index;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        // if the other object has layer "Character"
        if(collision.gameObject.layer == 8) 
        {
            PlayerInput player = collision.gameObject.GetComponent<PlayerInput>();
            MainPlayerScript ps = player.transform.parent.GetComponentInChildren<MainPlayerScript>();
            // if the players last visited checkpoint is the one before this one
            if (ps.checkpointIndex == index - 1)
            {
                // set the players last visited checkpoint to this one
                Debug.Log("a character entered checkpoint " + index + ". Setting that players checkpointID from " + (index-1) + " to " + index);
                ps.checkpointIndex = index;
            }
        }
        
    }
}
