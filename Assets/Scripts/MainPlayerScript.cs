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

    //Reference to script that holds player instance ids
    GameManager game_manager;                   

    [SerializeField] GameObject item_get_particles;


    //This variable holds the current item.
    //If it's -1, the player is not holding an item. When using items, you need to set it back to -1.
    //Set when getting an item in OnTriggerEnter event.
    public int current_item = -1;
    
    // integer that holds the player ID; can either be "1" for player one or "2" for player 2
    // gets set when a player first joins using the gamemanager script
    public int playerID;

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
        Debug.Log("Player " + playerID + " collided with object of layer " + other.gameObject.layer);

        //ITEM BOX COLLISION
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

        //COLLISION WITH BOULDER FROM ENEMY
        if (other.gameObject.layer == 13)
        {
            Debug.Log("TARGET: " + other.GetComponent<RockVars>().target);

            if (other.GetComponent<RockVars>().target == this.transform)
            {
                Debug.Log("Hit enemy");
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        //print("trigger stay"); // will be executed every frame while player is still inside trigger
    }

    void OnTriggerExit(Collider other)
    {
        //print("exited");
    }



   
    void Start()
    {
        player = this.transform.parent.GetComponentInChildren<PlayerInput>();
        lapNumber = 1;
        checkpointIndex = 0;
        GameObject gmo = GameObject.Find("GameManager");
        gm = gmo.GetComponent<GameManager>();
        //Find the script where you want to call item use functions
        item_effects_script = GameObject.FindGameObjectWithTag("ItemUse").GetComponent<ItemEffects>();
        sound_effects_script = GameObject.FindGameObjectWithTag("SFX").GetComponent<apse>();
        game_manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
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
            //SET USER AND TARGET------------------------------------------------------------------------------------------

            if (current_item == 0)
            { 
                //If playerID == 1, target = 2 
                int target_id = playerID == 1 ? 2 : 1;

                Transform other = transform;
                Transform id = transform;

                //Get target object for ROCK
                foreach (var x in game_manager.players)
                {
                    if (x.transform.parent.GetComponentInChildren<MainPlayerScript>().playerID == target_id)
                        other = x.transform;              //Player who gets item in the face

                    else id = x.transform;                //Player who used the item
                }

                //Play sfx and reset
                sound_effects_script.play_sfx_use_item();
                Debug.Log("Player " + playerID + " used item " + current_item); 

                var old_current_item = current_item;
                current_item = -1;

                //Use
                if (old_current_item == 0)
                item_effects_script.use_rock_item(id, other);
            }



            Camera[] camera_array = game_manager.cameras.ToArray();
            GameObject target_camera = playerID == 1 ? camera_array[1].gameObject : camera_array[0].gameObject;


            //CAMERA SHAKE EFFECT--------------------------------------------------------------------
            if (current_item == 1)
            {
                item_effects_script.use_shaker_item(target_camera);
            }
            
            
            //VIDEO DISTRACTION-------------------------------------------------------------------
            if (current_item == 2)
            item_effects_script.use_distraction_item(target_camera);


            //Dont write code after this, doesnt get called 
          
            
        }
   
         
    }
}
