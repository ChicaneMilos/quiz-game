using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorState : MonoBehaviour
{
    [SerializeField] private bool isCorrectAnswer = false;

    public bool getAnswerState() { return isCorrectAnswer; }
    public void setAnswerState(bool state) { isCorrectAnswer = state; }
}
