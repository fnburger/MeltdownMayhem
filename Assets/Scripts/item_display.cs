using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class item_display : MonoBehaviour
{
    private int item = -1;
    public Text item_text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        item_text.text = "ITEM : " + item;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            item++;
        }
    }
}
