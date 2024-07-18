using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayQuiz : MonoBehaviour
{
    public void Play()
    {
        int id = gameObject.GetComponent<LobbyItemInfo>().GetID();

        LevelController.instance.DeleteOtherData(id);
        SceneManager.LoadScene(1);
    }
}
