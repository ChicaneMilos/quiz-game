using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditQuestionAction : MonoBehaviour
{
    public void EditQuestion()
    {
        EditDialog.instance.EditQuestion(this.gameObject.transform.parent.gameObject);
    }
}
