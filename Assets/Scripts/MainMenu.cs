using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private float gameVolume;
    private float bgmVolume;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        LoadVolumeSettings();
    }

    // function for Play Button in the main menu
    public void PlayGame ()
    {
        //SceneManager.LoadScene("Level1");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // load the next scene in the queue
    }

    // function for quit button in main menu
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    private void LoadVolumeSettings()
    {
        gameVolume = PlayerPrefs.GetFloat("gameVolume");
        bgmVolume = PlayerPrefs.GetFloat("bgmVolume");
        AudioListener.volume = gameVolume;
        // TODO: use bgmVolume to set it


    }

}
