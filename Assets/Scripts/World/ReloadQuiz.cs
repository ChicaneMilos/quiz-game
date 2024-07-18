using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadQuiz : MonoBehaviour
{
    public void reloadQuiz()
    {
        SceneManager.LoadScene(1);
    }
}
