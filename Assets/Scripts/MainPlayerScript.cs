using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainPlayerScript : MonoBehaviour
{
    
    public AudioSource sfx_source;              //Item get sfx
    public GameObject obj_play_sfx;


    //Reference to script that holds all the item effects
    ItemEffects item_effects_script;
    apse sound_effects_script;

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
        Debug.Log("Player " + playerID + " collided with object of layer " + other.gameObject.layer);

        //ITEM COLLISION
        if (other.gameObject.layer == 12)
        {
            //PLAY SFX
            sfx_source = GetComponent<AudioSource>();
            sfx_source.Play();
            Debug.Log("Game object: " + this.gameObject);

            //GIVE PLAYER AN ITEM
            //current_item = Random.Range(0, 3);
            current_item = Random.Range(0,0);
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
        //Find the script where you want to call item use functions
        item_effects_script = GameObject.FindGameObjectWithTag("ItemUse").GetComponent<ItemEffects>();
        sound_effects_script = GameObject.FindGameObjectWithTag("SFX").GetComponent<apse>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(current_item);
    }

    public void UseItem()
    {
       

        //Nope
        if (current_item == -1)
        {
            Debug.Log("No item");
            sound_effects_script.play_sfx_no();
        }

        else
        {   
            //Use
            if (current_item == 0)
            item_effects_script.use_rock_item();       
            
            if (current_item == 1)
            item_effects_script.use_shaker_item();     
            
            if (current_item == 2)
            item_effects_script.use_distraction_item();


            //Play sfx and reset
            sound_effects_script.play_sfx_use_item();
            Debug.Log("Player " + playerID + " used item " + current_item);
            current_item = -1;
            
        }
   
         
    }
}
