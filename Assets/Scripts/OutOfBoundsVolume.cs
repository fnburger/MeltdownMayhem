using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class OutOfBoundsVolume : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            PlayerInput player = other.gameObject.GetComponent<PlayerInput>();
            MainPlayerScript ps = player.transform.parent.GetComponentInChildren<MainPlayerScript>();
            ps.MoveToCurrentCheckpoint();
        }
    }

}
