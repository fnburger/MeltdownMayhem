using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                     //NECESSARY TO GET IMAGE COMPONENT



//Draws the current player item to the canvas
public class draw_item : MonoBehaviour
{

    public int playerID;      //Set this to 1 in the left canvas and 2 in the right canvas
    public Sprite boulder_sprite;
    public Sprite mixer_sprite;
    public Sprite distraction_sprite;

    GameManager game_manager;
    MainPlayerScript main_player_script;                        //Main player script of the player that holds the item

    float item_background_alpha = 0.3f;




    //Gets the current item of player {playerID}
    private int player_item()
    {
        if (main_player_script != null)
        {
            if (main_player_script.current_item != -1)
            {
                //Debug.Log("Player " + playerID + " has an item!!!!!");
            }
            return main_player_script.current_item;
        }

        else Debug.Log("Player script is null");

        return -1;
    }

    //Set image alpha value from the start
    void Start()
    {
        var image = gameObject.GetComponent<Image>();

        //Alpha
        Color c = image.color;
        c.a = item_background_alpha;
        image.color = c;
    }


    //ACCESS SCRIPT THAT KNOWS WHICH ITEM THE PLAYER HOLDS
    void Update()
    {
        //Get the player object that's holding the item
        game_manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        var player_array = game_manager.players.ToArray();
        GameObject player_holding_the_item = playerID == 1 ? player_array[0].gameObject : player_array[1].gameObject;
        main_player_script = player_holding_the_item.GetComponent<MainPlayerScript>();

        var image = gameObject.GetComponent<Image>();


        //DRAW THE ITEM
        if (player_item() != -1)
        {
            //Alpha
            Color c = image.color;
            c.a = 1;
            image.color = c;

            //Set sprite
            if (player_item() == 0)
            image.sprite = boulder_sprite; 
            
            else if (player_item() == 1)
            image.sprite = mixer_sprite;       
            
            else if (player_item() == 2)
            image.sprite = distraction_sprite;

            //Debug.Log("Image sprite: " + image.sprite);
        }

        //No item
        else
        {
            //Alpha
            Color c = image.color;
            c.a = item_background_alpha;
            image.color = c;

            image.sprite = null;
        }
    }
}
