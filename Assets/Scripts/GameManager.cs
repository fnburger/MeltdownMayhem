using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    public List<PlayerInput> players = new List<PlayerInput>();
    [SerializeField]
    private List<Transform> startingPoints;
    [SerializeField]
    private List<LayerMask> playerLayers;

    private PlayerInputManager playerInputManager;

    private void Awake()
    {
        playerInputManager = FindObjectOfType<PlayerInputManager>();
    }

    private void onEnable()
    {
        playerInputManager.onPlayerJoined += AddPlayer;
    }

    private void onDisable()
    {
        playerInputManager.onPlayerJoined -= AddPlayer;
    }

    // Start is called before the first frame update
    void Start()
    {
        // spawn players
        //SpawnPlayers();
        // TODO: lock player input
        // ...

        // StartGame
        //StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: do stuff that needs to be checked every frame globally
        // stuff per player etc. can be done with another script on each player object 
        
    }

    public void AddPlayer(PlayerInput player)
    {
        players.Add(player);

        // need to use the parent due to the structure of the prefab
        Transform playerParent = player.transform.parent;
        playerParent.position = startingPoints[players.Count - 1].position;

        // convert layer mask (bit) to an integer
        int layerToAdd = (int)Mathf.Log(playerLayers[players.Count - 1].value, 2);

        // set the layer
        playerParent.GetComponentInChildren<CinemachineFreeLook>().gameObject.layer = layerToAdd;
        // add the layer
        playerParent.GetComponentInChildren<Camera>().cullingMask |= 1 << layerToAdd;
        // set the action in the custom cinemachine Input Handler
        playerParent.GetComponentInChildren<InputHandler>().horizontal = player.actions.FindAction("Look");
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
