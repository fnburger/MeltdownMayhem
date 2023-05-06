using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainPlayerScript : MonoBehaviour
{
    
    public AudioSource sfx_source;              //Item get sfx
    public GameObject obj_play_sfx;

    [SerializeField] GameObject item_get_particles;


    //This variable holds the current item.
    //If it's -1, the player is not holding an item. When using items, you need to set it back to -1.
    //Set when getting an item in OnTriggerEnter event.
    public int current_item = -1;
    
    // integer that holds the player ID; can either be "1" for player one or "2" for player 2
    // gets set when a player first joins using the gamemanager script
    public int playerID;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //print(hit.gameObject.name);
    }

    void OnTriggerEnter(Collider other)
    {
        print("entered");


        //ITEM COLLISION
        if (other.gameObject.layer == 12)
        {
            //PLAY SFX
            sfx_source = GetComponent<AudioSource>();
            sfx_source.Play();

            //GIVE PLAYER AN ITEM
            current_item = Random.Range(0, 3);
            print("---GOT ITEM: " + current_item);


            //CREATE PARTICLES AND DESTROY ITEM BOX
            GameObject particles = Instantiate(item_get_particles, transform.position, transform.rotation);
            Destroy(other.gameObject);
            Destroy(particles, 0.7f);
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

    public void UseItem()
    {
        Debug.Log("Player " + playerID + " used an item!");

        if (current_item == -1)
        {
            var sfx = Instantiate(obj_play_sfx);
            //Destroy(sfx);
        }

        else
        {

        }
        // check if player has an item, if no play a sound
        // else:
            // check which item was used and call the specific effect function inside the corresponding game object version
         
    }
}
