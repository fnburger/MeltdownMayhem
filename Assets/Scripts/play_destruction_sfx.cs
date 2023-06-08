using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class play_destruction_sfx : MonoBehaviour
{
    public AudioSource audio_player;


    void OnDestroy()
    {
        print("Should play item sound");
        audio_player.Play();
    }

}
