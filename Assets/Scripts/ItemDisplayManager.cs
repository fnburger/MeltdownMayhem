using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplayManager : MonoBehaviour
{

    public GameObject obj;

    void Start()
    {
        HideDisplay();
    }

    public void ShowDisplay()
    {
        obj.SetActive(true);
    }

    public void HideDisplay()
    {
        obj.SetActive(false);
    }
}
