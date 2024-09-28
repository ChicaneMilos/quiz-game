using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleQuiz : MonoBehaviour
{
    public void Play()
    {
        int id = gameObject.GetComponent<LobbyItemInfo>().GetID();
        LevelController.instance.DeleteOtherData(id);
        SceneManager.LoadScene(1);
    }

    public void DeleteQuiz()
    {
        int id = gameObject.GetComponent<LobbyItemInfo>().GetID();
        LevelController.instance.DeleteData(id);
        Destroy(gameObject);
    }
}
