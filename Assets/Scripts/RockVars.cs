using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Holds variables for the boulder item


public class RockVars : MonoBehaviour
{
    public int target;      //playerID is being checked
    float damage = 0.2f;
    apse sound_effects_script;


    //Colliding with other boulders: both get destroyed
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 15)
        {
            Destroy(gameObject);

            Debug.Log("Rocks hit each other");
            sound_effects_script.play_sfx_rock_destroy();
        }

    }

    void Start()
    {
        sound_effects_script = GameObject.FindGameObjectWithTag("SFX").GetComponent<apse>();
    }
}
