using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddQuiz : MonoBehaviour
{
    public void AddNewQuiz()
    {
        LevelController.instance.CreateLevel();
    }
}