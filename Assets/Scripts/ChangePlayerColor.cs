using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangePlayerColor : MonoBehaviour
{
    public int flavor;
    public Image img;
    public IceColorSetter ics;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Flavor"))
        {
            PlayerPrefs.SetInt("Flavor", 0);
        }
        else
        {
            Load();
        }
    }

    public void ChangeFlavor()
    {
        flavor = (flavor + 1) % 3;
        ics.customColor = flavor;
        img.color = ics.GetColor();
        Save();
    }

    private void Load()
    {
        flavor = PlayerPrefs.GetInt("Flavor");
        ics.customColor = flavor;
        img.color = ics.GetColor();
    }

    private void Save()
    {
        PlayerPrefs.SetInt("Flavor", flavor);
    }
}
