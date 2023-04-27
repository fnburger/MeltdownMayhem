using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangePlayerName : MonoBehaviour
{
    public TMP_InputField textArea;
    public string playerPrefsKey;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey(playerPrefsKey))
        {
            PlayerPrefs.SetString(playerPrefsKey, textArea.text);
        }
        else
        {
            Load();
        }
    }

    public void ChangeName()
    {
        Save();
    }

    private void Load()
    {
        textArea.text = PlayerPrefs.GetString(playerPrefsKey);
    }

    private void Save()
    {
        PlayerPrefs.SetString(playerPrefsKey, textArea.text);
    }
}
