using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerScript : MonoBehaviour
{

    public AudioSource sfx_source;

    

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //print(hit.gameObject.name);
    }

    void OnTriggerEnter(Collider other)
    {
        print("entered");

        if (other.gameObject.layer == 12)
        {
            sfx_source = GetComponent<AudioSource>();
            sfx_source.Play();
            print("Got item");
            Destroy(other.gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        //print("trigger stay"); // will be executed every frame while player is still inside trigger
    }

    void OnTriggerExit(Collider other)
    {
        print("exited");
    }



   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
