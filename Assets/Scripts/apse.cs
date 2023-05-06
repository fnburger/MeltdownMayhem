using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class apse : MonoBehaviour
{
    //"Audio Play Sound Effect". Plays a specific sound at desired time when added to an object as a component.
    public AudioSource audio_source;              //Item get sfx
    public AudioClip audio_clip;
    private float volume = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        print("Apse");
        audio_source.PlayOneShot(audio_clip, volume);
    }

    void Update()
    {
  
    }


}
