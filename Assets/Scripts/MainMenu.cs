using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   
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

}
