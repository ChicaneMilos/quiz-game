using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckAnswer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.GetComponent<DoorState>().getAnswerState())
        {
            ScoreController.instance.IncreaseScore();

            if (gameObject.transform.parent.gameObject.GetComponent<QuestionHandlerTWO>().lastPlatform == true)
            {
                ScoreController.instance.endGame(true);
            }
        }
        else
        {
            ScoreController.instance.endGame(false);
            //SceneManager.LoadScene(1);
        }
    }
}
