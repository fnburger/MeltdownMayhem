using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleTextAnim : MonoBehaviour
{
    public TMP_Text textComponent;

    // Update is called once per frame
    void Update()
    {
        textComponent.ForceMeshUpdate();
        var textInfo = textComponent.textInfo;

        for (int i = 0; i < textInfo.characterCount; ++i)
        {
            var charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible)
            {
                continue;
            }
            
            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;
            
            // each char has 4 vertices that make a box around it
            for (int j = 0; j < 4; ++j)
            {
                var orig = verts[charInfo.vertexIndex + j];
                // actual animation. modifies the draft data (textInfo.meshInfo[i].vertices)
                verts[charInfo.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.time * 2f + orig.x * 0.01f) * 10f, 0);
            }
        }

        // now we need to modify the working data (textInfo.meshInfo[i].mesh.vertices
        for (int i = 0; i < textInfo.meshInfo.Length; ++i)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            textComponent.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}
