using SFB;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

using UnityEngine.UI;

public class Banners : MonoBehaviour
{
    public Renderer objectRenderer;
    public RawImage rawImage1;

    void Start()
    {
        if (objectRenderer == null)
        {
            Debug.LogError("objectRenderer is not assigned in the Inspector");
        }
    }


    public void OpenExplorer()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel("Select Texture", "", "png", false);
        if (paths.Length > 0)
        {
            Debug.Log("Selected file path: " + paths[0]);
            LoadTexture(paths[0]);
        }
        else
        {
            Debug.Log("No file selected");
        }
    }


    void LoadTexture(string path)
    {
        byte[] fileData = File.ReadAllBytes(path);
        Texture2D newTexture = new Texture2D(2, 2);
        newTexture.LoadImage(fileData);

        if (newTexture != null)
        {
            // Direktno menjanje teksture na originalnom materijalu
            objectRenderer.sharedMaterial.mainTexture = newTexture;
            rawImage1.texture = newTexture;

            Debug.Log("Texture loaded and applied to the shared material.");
        }
        else
        {
            Debug.Log("Failed to load texture.");
        }
    }


}
