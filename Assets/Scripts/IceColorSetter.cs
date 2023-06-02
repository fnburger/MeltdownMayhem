using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceColorSetter : MonoBehaviour
{
    public int customColor; // 0 = strawberry ; 1 = chocolate ; 2 = blueberry
    private Texture2D texture;
    int textureW = 256;
    int textureH = 256;
    public MeshRenderer renderer = null;

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

        texture = new Texture2D(textureW, textureH);
        for (int x = 0; x < textureW; x++)
        {
            for (int y = 0; y < textureH; y++)
            {
                Color color = GetColor();
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        if(renderer != null)
        {
            AssignTexture(texture);
        }
    }

    public void AssignTexture(Texture2D t)
    {
        renderer.material.mainTexture = texture;
        renderer.material.color = new Color(1.0f, 1.0f, 1.0f);
    }

    public Color GetColor()
    {
        Color color;
        switch (customColor)
        {
            case 0:
                color = new Color(1.0f, 0.76f, 0.89f);
                break;
            case 1:
                color = new Color(0.42f, 0.21f, 0.07f);
                break;
            case 2:
                color = new Color(0.39f, 0.031f, 0.26f);
                break;
            default:
                color = new Color(1.0f, 1.0f, 1.0f);
                break;
        }
        return color;
    }

    private void Load()
    {
        customColor = PlayerPrefs.GetInt("Flavor");
    }
}
