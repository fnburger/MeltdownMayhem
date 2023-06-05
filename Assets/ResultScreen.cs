using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultScreen : MonoBehaviour
{
    public TMP_Text text_winner_name;

    public void setWinnerText(string s)
    {
        text_winner_name.text = s;
    }
}
