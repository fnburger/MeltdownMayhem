using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // spawn players
        SpawnPlayers();
        // TODO: lock player input
        // ...

        // StartGame
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: do stuff that needs to be checked every frame globally
        // stuff per player etc. can be done with another script on each player object 
        
    }

    public void SpawnPlayers()
    {
        // TODO: spawn players at start point
        // ...

        Debug.Log("spawning players...");
    }

    public void StartGame()
    {
        // TODO: show countdown, then allow player input
        Debug.Log("Starting game...");

        // spawning items
        SpawnItems();
    }

    public void SpawnItems()
    {
        // TODO: spawn items randomly on the map
    }
}
