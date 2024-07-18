using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeleteAnswerAction : MonoBehaviour
{

    public void DeleteQuestion()
    {
        if (QuestionController.instance.ReturnNumberOfQuestions() == 2)
        {
            print("Minimal number of questions is 2.");
        }
        else
        {
            QuestionController.instance.DecrementQuestions();
            QuestionController.instance.RemoveFromAnswerList(gameObject.transform.parent.gameObject);
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
