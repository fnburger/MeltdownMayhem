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

    private List<Camera> cameras = new List<Camera>();

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
        // move players to starting points


        // TODO: lock player input until fgames starts
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
        if (players.Count == 2) return;
        if (player == null) Debug.Log("joined player is null :( --> GameManager");

        // ad player to our list of players
        players.Add(player);
        // move the player to the designated start position
        MovePlayerToStart(player);

        // convert layer mask (bit) to an integer
        int layerToAdd = (int)Mathf.Log(playerLayers[players.Count - 1].value, 2);

        // need to use the parent due to the structure of the prefab
        Transform playerParent = player.transform.parent;
        // set the layer
        playerParent.GetComponentInChildren<CinemachineVirtualCamera>().gameObject.layer = layerToAdd;
        // add the layer
        playerParent.GetComponentInChildren<Camera>().cullingMask |= 1 << layerToAdd;
        // set the action in the custom cinemachine Input Handler
        playerParent.GetComponentInChildren<InputHandler>().horizontal = player.actions.FindAction("Look");

        cameras.Add(playerParent.GetComponentInChildren<Camera>());

        // if this is player 1
        if (players.Count == 1)
        {
            Debug.Log("Player 1 joined!");
            // reduce player 1 camera priority so that player 2 will see his own cam
            playerParent.GetComponentInChildren<Camera>().depth -= 1;

        } 
        else
        {
            Debug.Log("Player 2 joined!");
            // setup cam distance from players
            SetupPlayerCamPositions();
        }


    }

    public void MovePlayerToStart(PlayerInput player)
    {
        
        Transform playerParent = player.transform.parent;
        playerParent.position = startingPoints[players.Count - 1].position;
    }

    public void SetupPlayerCamPositions()
    {
        
        //cameras.ForEach(transform.position += new Vector3(0f, 5f, 2.5f));
        //transform.position += new Vector3(0f, 5f, 2.5f); 
        for (int i = 0; i < 2; i++)
        {
            Transform parent = players[i].transform.parent;
            parent.GetComponentInChildren<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = 100;
            Debug.Log("set distance");
        }
        
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
