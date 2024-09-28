using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class ReturnToMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void MenuReturn()
    {
        GameObject DATA = GameObject.Find("DATA");
        Destroy(DATA);
        SceneManager.LoadScene(0);
    }
}
