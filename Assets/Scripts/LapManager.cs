using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LapManager : MonoBehaviour
{
    [Tooltip("You must set this manually in the editor. The checkpoints have to be placed in the scene.")]
    public List<Checkpoint> checkpoints;
    [Tooltip("You must set this manually in the editor. The amount of laps the players must complete to win the race.")]
    public int totalLaps;


    void OnTriggerEnter(Collider collision)
    {
        // if the other object has layer "Character"
        if (collision.gameObject.layer == 8)
        {
            PlayerInput player = collision.gameObject.GetComponent<PlayerInput>();
            MainPlayerScript ps = player.transform.parent.GetComponentInChildren<MainPlayerScript>();
            // if the players last visited checkpoint is the last one
            if (ps.checkpointIndex == checkpoints.Count)
            {
                // set last visited to none. checkpoint #1 must now be visited next
                ps.checkpointIndex = 0;
                // the player is now in the next lap
                ps.lapNumber++;
                Debug.Log("You are now on lap " + ps.lapNumber);

                // if the player has completed the last lap
                if(ps.lapNumber > totalLaps)
                {
                    // End the race
                    Debug.Log("You won!");
                }
            }
        }
    }
}
