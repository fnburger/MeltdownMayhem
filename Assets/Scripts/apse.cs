using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class apse : MonoBehaviour
{
    //"Audio Play Sound Effect". Plays a specific sound at desired time when added to an object as a component.
    public AudioSource audio_source;              //Item get sfx


    //PUT ALL SFX IN THE GAME IN HERE (and create a function down below, and also assign the sound in the components)
    public AudioClip sfx_no;
    public AudioClip sfx_use_item;
    public AudioClip sfx_jump;
    public AudioClip sfx_hit;
    public AudioClip sfx_rock_destroy;



    private float volume = 1.0f;


    //PLAY ALL SFX HERE
    public void play_sfx_no()
    {
        audio_source.PlayOneShot(sfx_no, volume);
    }

    public void play_sfx_use_item()
    {
        audio_source.PlayOneShot(sfx_use_item, volume);
    }    
    
    public void play_sfx_jump()
    {
        audio_source.PlayOneShot(sfx_jump, 0.01f);
    }   
    
    public void play_sfx_hit()
    {
        audio_source.PlayOneShot(sfx_hit, volume);
    }    
    
    public void play_sfx_rock_destroy()
    {
        audio_source.PlayOneShot(sfx_rock_destroy, volume);
    }


}
