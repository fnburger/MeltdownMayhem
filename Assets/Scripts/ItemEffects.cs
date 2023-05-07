using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffects : MonoBehaviour
{
    //Stores all item use effect functions.


    //Rock that is targeting the enemy. Slows down temporarily when hit.
    public void use_rock_item()
    {
        Debug.Log("Haha I used a rock item OMEGALUL");
    }    
    

    //Item that shakes the camera of the other player.
    public void use_shaker_item()
    {
        Debug.Log("Used shaker item");
    }  
    
    //Item that creates a stupid video in front of the other player and
    //makes it hard to see.
    public void use_distraction_item()
    {
        Debug.Log("Used video item");
    }
}
