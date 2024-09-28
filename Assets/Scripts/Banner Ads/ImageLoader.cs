using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ImageLoader : MonoBehaviour
{
    public RawImage displayImage;
    private Texture2D loadedTexture;

    //EDITOR READY - TO BE ADDED

    //public void LoadImage()
    //{
    //    // Otvara dijalog za odabir slike
    //    string imagePath = UnityEditor.EditorUtility.OpenFilePanel("Odaberi sliku", "", "png,jpg,jpeg");

    //    if (!string.IsNullOrEmpty(imagePath))
    //    {
    //        // Učitava sliku kao teksturu
    //        byte[] fileData = File.ReadAllBytes(imagePath);
    //        loadedTexture = new Texture2D(2, 2);
    //        loadedTexture.LoadImage(fileData); // Učitava podatke slike u teksturu

    //        // Postavlja učitanu sliku na UI element
    //        displayImage.texture = loadedTexture;
    //    }
    //}
}