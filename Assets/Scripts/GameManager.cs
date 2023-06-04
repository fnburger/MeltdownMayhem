using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

    public List<Camera> cameras = new List<Camera>();

    private int maxPlayerCount;
    [Header("Delay before the game starts after player 2 has joined")]
    [Tooltip("Should be the same length in seconds as the countdown animation.")]
    public int countdownTime = 3; // in seconds
    private bool gameIsLive = false;
    AudioSource gameStartSound;
    private bool istTesting;
    public GameObject CountdownDisplayObject;
    private int initialStateHash;

    apse sound_effects_script;




    private void Awake()
    {
        playerInputManager = FindObjectOfType<PlayerInputManager>();
        maxPlayerCount = playerInputManager.maxPlayerCount;
        initialStateHash = CountdownDisplayObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).fullPathHash;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameStartSound = GetComponent<AudioSource>();
        sound_effects_script = GameObject.FindGameObjectWithTag("SFX").GetComponent<apse>();
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
            // TODO: Pause Game because a player left :ccccccc
            PauseGame();
        }
    }

    public void AddPlayer(PlayerInput player)
    {
        deactivateLevelCam();
        if (players.Count == maxPlayerCount) return;
        if (player == null) Debug.Log("joined player is null :(((( --> GameManager");

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
            playerParent.GetComponentInChildren<MainPlayerScript>().playerID = 1;
        } 
        else
        {
            Debug.Log("Player 2 joined!");
            playerParent.GetComponentInChildren<MainPlayerScript>().playerID = 2;
        }
        // move the player to the designated start position
        MovePlayerToStart(player);
    }

    public void MovePlayerToStart(PlayerInput player)
    {
        Transform playerParent = player.transform.parent;
        DisablePlayerController(player);
        playerParent.transform.position = startingPoints[players.Count - 1].transform.position;
        playerParent.transform.rotation = startingPoints[players.Count - 1].transform.rotation;
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

    public void MovePlayerToPosition(PlayerInput player, Transform tr)
    {
        DisablePlayerController(player);
        StartCoroutine(Teleport(player, tr));
    }

    public IEnumerator Teleport(PlayerInput player, Transform tr)
    {
        yield return new WaitForSeconds(2);
        player.gameObject.transform.position = tr.position;
        player.gameObject.transform.rotation = tr.rotation;
        EnablePlayerController(player);
    }

    public void StartGame()
    {
        gameIsLive = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.Log("Starting game in " + countdownTime + " seconds...");

        // show countdown, its length in seconds should be equal to countdownTime
        CountdownDisplayObject.GetComponent<SpriteRenderer>().enabled = true;
        CountdownDisplayObject.GetComponent<Animator>().Play(initialStateHash, 0, 0f);

        sound_effects_script.play_sfx_countdown();

        // enable all player controllers after the countdown has passed
        Invoke("EnablePlayerController", countdownTime);
        // play start game sound after countdown has finished
         Invoke("PlayGameStartSound", countdownTime);
        // hide the countdown after it finishes
        Invoke("stopCountdown", countdownTime+1);
    }

    public void stopCountdown()
    {
        CountdownDisplayObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    // for devs only
    public void StartTesting()
    {
        istTesting = true;
        gameIsLive = true;
        Debug.Log("Starting test...");
        EnablePlayerController();
        PlayGameStartSound();
    }

    public void PauseGame()
    {
        gameIsLive = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // TODO
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        gameIsLive = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // TODO
        Time.timeScale = 1;
    }

    public void PlayGameStartSound()
    {
        if (gameStartSound != null) gameStartSound.Play(0);
    }
   
    public void EndGame(PlayerInput winner) 
    {
        // winner completed the last lap
        
        Transform playerParent = winner.transform.parent;
        int winnerID = playerParent.GetComponentInChildren<MainPlayerScript>().playerID;
        string playerPrefsKey = "Player";
        string winnerName;
        switch(winnerID)
        {
            case 1:
                playerPrefsKey = "player1_name";
                break;
            case 2:
                playerPrefsKey = "player2_name";
                break;
        }
        if (!PlayerPrefs.HasKey(playerPrefsKey) || PlayerPrefs.GetString(playerPrefsKey) == "" || PlayerPrefs.GetString(playerPrefsKey) == " ")
        {
            //PlayerPrefs.SetString(playerPrefsKey, "Player"+winnerID);
            winnerName = "Player " + winnerID;
        } else
        {
            winnerName = PlayerPrefs.GetString(playerPrefsKey);
        }
        Debug.Log(winnerName + " won!");

        // TODO: show some sort of endscreen before we load the menu level
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("Menu");
    }

    public void ResetGameState()
    {
        SceneManager.LoadScene("Level1");
    }
}
