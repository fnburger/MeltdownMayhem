using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Holds variables for the boulder item


public class RockVars : MonoBehaviour
{
    public Transform target;
    float damage = 0.2;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            // get mainplayerscript von other --> mpc.TakeDamage(this, damage);

        }
    }
}
