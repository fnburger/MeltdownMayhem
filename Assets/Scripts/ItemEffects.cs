using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffects : MonoBehaviour
{
    //Stores all item use effect functions.


    public GameObject boulder_item;
    public Transform player;                 //Player who uses the item
    public Transform target;                 //Player who gets the item in the face

    static float rock_speed = 15.0f;           //Change this to make used rock item faster or slower
    static float rock_offset_from_ground = 1.0f;



    //Rock that is targeting the enemy. Slows down temporarily when hit.
    public void use_rock_item(Transform user, Transform target)
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

        rock.GetComponent<RockVars>().target = target;
    }    
    

    //Item that shakes the camera of the other player.
    public void use_shaker_item(GameObject target_view)
    {
        Debug.Log("Used shaker item");
    }  
    
    //Item that creates a stupid video in front of the other player and
    //makes it hard to see.
    public void use_distraction_item(GameObject target)
    {
        Debug.Log("Used video item");
    }
}
