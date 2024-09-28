using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeleteQuestionAction : MonoBehaviour
{

    public void DeleteQuestion()
    {
        QuestionController.instance.DecrementQuestions();
        QuestionController.instance.RemoveFromAnswerList(gameObject.transform.parent.gameObject);
        Destroy(gameObject.transform.parent.gameObject);
    }
}
