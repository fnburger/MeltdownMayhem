using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;


public class ItemEffects : MonoBehaviour
{
    //Stores all item use effect functions.


    public GameObject boulder_item;
    public Transform player;                 //Player who uses the item
    public Transform target;                 //Player who gets the item in the face

    static float rock_speed = 15.0f;           //Change this to make used rock item faster or slower
    static float rock_offset_from_ground = 1.0f;

    GameManager game_manager;
    apse sound_effects_script;
    public int shake_frames = 290;

    //ThirdPersonController third_person_controller;



    void Start()
    {
        game_manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        sound_effects_script = GameObject.FindGameObjectWithTag("SFX").GetComponent<apse>();
    }

    //Rock that is targeting the enemy. Slows down temporarily when hit.
    public void use_rock_item(Transform user, Transform target, int target_id)
    {
        Debug.Log("Haha I used a rock item OMEGALUL");

        var rock = Instantiate(boulder_item, new Vector3(user.position.x, user.position.y+ rock_offset_from_ground, user.position.z), Quaternion.identity);

        var heading = target.position - user.position;
        var distance = heading.magnitude;                                  //Can be used but is unused
        var direction = heading / distance;                                 //Target enemy
        var velocity = rock_speed * direction;                             //Set actual speed


        var body = rock.GetComponent<Rigidbody>();
        body.velocity = velocity;                                                    //Fly away to victory
        body.AddTorque(new Vector3(160.0f,140.0f,180.0f), ForceMode.Force);         //Haha funny rotation

        rock.GetComponent<RockVars>().target = target_id;
    }



    Vector3 initial_target_viewpoint_position;
    GameObject shaking_target_viewpoint;
    Transform target_p;
    int n = 0;
  

    //Item that shakes the camera of the other player.
    public void use_shaker_item(Transform target_player)
    {
        Debug.Log("Used shaker item OMEGASHAKE");

        //Trying things with Cinemachine
        var component = target_player.transform.parent.GetComponentInChildren<ThirdPersonController>();
        GameObject target_viewpoint = component.CinemachineCameraTarget;
        initial_target_viewpoint_position = target_viewpoint.transform.position;                        //Remember this guy to reset it afterwards
        shaking_target_viewpoint = target_viewpoint;
        target_p = target_player;

        //Sfx
        sound_effects_script.play_sfx_death();
        sound_effects_script.play_sfx_scream();

        //Effect
        StartCoroutine(CameraShake());
    }

    IEnumerator CameraShake()
    {
        //Shake that thing 
        while (n < shake_frames)
        {
            shaking_target_viewpoint.transform.position = new Vector3(
            initial_target_viewpoint_position.x + Random.Range(-4,4),
            initial_target_viewpoint_position.y + Random.Range(-2, 2),
            initial_target_viewpoint_position.z + Random.Range(-3, 3));

            n++;

            Debug.Log(n);
        
            yield return null;
        }

        n = 0;

        //Reset
        shaking_target_viewpoint.transform.position = new Vector3(
            initial_target_viewpoint_position.x - Mathf.Abs(initial_target_viewpoint_position.x - target_p.transform.position.x),
            initial_target_viewpoint_position.y, //- Mathf.Abs(initial_target_viewpoint_position.y - target_p.transform.position.y),
            initial_target_viewpoint_position.z //- Mathf.Abs(initial_target_viewpoint_position.z - target_p.transform.position.z)
            );

        Debug.Log("Changed x: " + Mathf.Abs(initial_target_viewpoint_position.x - target_p.transform.position.x));
        Debug.Log("Changed y: " + Mathf.Abs(initial_target_viewpoint_position.x - target_p.transform.position.y));
        Debug.Log("Changed z: " + Mathf.Abs(initial_target_viewpoint_position.x - target_p.transform.position.z));

        Debug.Log("Cam should be normal again");
    }




    //Item that creates a stupid video in front of the other player and
    //makes it hard to see.
    public void use_distraction_item(GameObject target)
    {
        Debug.Log("Used video item");
    }
}
