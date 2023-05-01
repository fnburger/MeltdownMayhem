using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    [TextArea]
    public string Information_for_devs = "There must be a PlayerInputManager Object and a LevelCamera object with priority < 9 in the scene.";
    [Header("Reference to the LevelCamera")]
    [SerializeField]
    [Tooltip("You must set this reference manually in the editor.")]
    public Camera levelCam;
    [Header("List of Players")]
    [Tooltip("Gets filled automatically when players join")]
    public List<PlayerInput> players = new List<PlayerInput>();
    [Header("Player Starting Locations")]
    [Tooltip("Move these to the desired locations. Players get moved to this location after joining.")]
    [SerializeField]
    private List<Transform> startingPoints; // a list of player starting points
    [Header("Player Camera render layers")]
    [Tooltip("Must be the same as the camera culling mask settings in each player prefab respectively. Do not change if each player sees though the correct camera.")]
    [SerializeField]
    private List<LayerMask> playerLayers;
    private PlayerInputManager playerInputManager;
    private List<Camera> cameras = new List<Camera>();
    private int maxPlayerCount;
    [Header("Delay before the game starts after player 2 has joined")]
    [Tooltip("Should be the same length in seconds as the countdown animation.")]
    public int countdownTime = 3; // in seconds
    private bool gameIsLive = false;
    AudioSource gameStartSound;

    private void Awake()
    {
        playerInputManager = FindObjectOfType<PlayerInputManager>();
        maxPlayerCount = playerInputManager.maxPlayerCount;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameStartSound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        // TODO: do stuff that needs to be checked every frame globally
        // stuff per player etc. can be done with another script on each player object 

        if(!gameIsLive && players.Count == 2)
        {
            StartGame(); // start game when player 2 has joined
        }

        if(gameIsLive && players.Count != 2)
        {
            // TODO: Pause Game because a player left
            PauseGame();
        }
    }

    public void AddPlayer(PlayerInput player)
    {
        deactivateLevelCam();
        if (players.Count == maxPlayerCount) return;
        if (player == null) Debug.Log("joined player is null :( --> GameManager");

        // add player to our list of players
        players.Add(player);
        
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
        }
        // move the player to the designated start position
        MovePlayerToStart(player);
    }

    public void MovePlayerToStart(PlayerInput player)
    {
        Transform playerParent = player.transform.parent;
        DisablePlayerController(player);
        playerParent.transform.position = startingPoints[players.Count - 1].transform.position;
        //EnablePlayerController(player);
    }

    public void DisablePlayerController(PlayerInput player)
    {
        player.GetComponent<CharacterController>().enabled = false;
    }

    public void EnablePlayerController(PlayerInput player)
    {
        player.GetComponent<CharacterController>().enabled = true;
    }

    public void EnablePlayerController()
    {
        for (int i = 0; i < maxPlayerCount; i++)
        {
            EnablePlayerController(players[i]);
        }
    }

    public void DisablePlayerController()
    {
        for (int i = 0; i < maxPlayerCount; i++)
        {
            DisablePlayerController(players[i]);
        }
    }

    public void deactivateLevelCam()
    {
        levelCam.enabled = false;
    }

    public void StartGame()
    {
        gameIsLive = true;
        Debug.Log("Starting game...");

        // TODO: show countdown, its length in seconds should be equal to countdownTime


        // enable all player controllers after the countdown has passed
        Invoke("EnablePlayerController", countdownTime);
        // play start game sound after countdown has finished
        Invoke("PlayGameStartSound", countdownTime);
    }

    public void PauseGame()
    {
        gameIsLive = false;
        // TODO
    }

    public void PlayGameStartSound()
    {
        gameStartSound.Play(0);
    }
   
    
}
