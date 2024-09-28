using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditSelectedQuiz : MonoBehaviour
{
    public void EditQuiz()
    {
        QuestionController.instance.LoadQuiz(this.gameObject.transform.parent.GetComponent<LobbyItemInfo>().GetID());
        LevelController.instance.SetSelectedLevel(this.gameObject.transform.parent.GetComponent<LobbyItemInfo>().GetID());
    }
}
