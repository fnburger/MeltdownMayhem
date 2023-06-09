using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LapCounterDisplay : MonoBehaviour
{
    public GameManager gm;
    public TMP_Text lapCountText;
    public int playerID;

    void Update()
    {
        if(gm.players.Count == 2)
        {
            Transform playerParent = gm.players[playerID-1].transform.parent;
            MainPlayerScript mps = playerParent.GetComponentInChildren<MainPlayerScript>();
            lapCountText.text = mps.lapNumber.ToString();
        }
    }
}
