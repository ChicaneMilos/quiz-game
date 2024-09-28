using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionInfo : MonoBehaviour
{
    [SerializeField] private string title;
    [SerializeField] private string question1;
    [SerializeField] private string question2;
    [SerializeField] private string question3;
    [SerializeField] private string question4;
    [SerializeField] private int correctAnswer;
    [SerializeField] private int numberOfAnswers;

    [SerializeField] private TextMeshProUGUI title_ui;
    [SerializeField] private TextMeshProUGUI question1_ui;
    [SerializeField] private TextMeshProUGUI question2_ui;
    [SerializeField] private TextMeshProUGUI question3_ui;
    [SerializeField] private TextMeshProUGUI question4_ui;


    [SerializeField] private int numberOfQuestions;

    public string getTitle() { return title; }
    public string getQuestion1() { return question1; }
    public string getQuestion2() { return question2; }
    public string getQuestion3() { return question3; }
    public string getQuestion4() { return question4; }
    public int getCorrectAnswer() { return correctAnswer; }


    public void setTitle(string content) { title = content; }
    public void setQuestion1(string content) { question1 = content; }
    public void setQuestion2(string content) { question2 = content; }
    public void setQuestion3(string content) { question3 = content; }
    public void setQuestion4(string content) { question4 = content; }
    public void setCorrectAnswer(int content) { correctAnswer = content; }
    public void setNumberOfAnswers(int content) { numberOfAnswers = content; }
    public int getNumberOfAnswers(int content) { return numberOfAnswers; }

    public void UpdateValues()
    {
        title_ui.text = title;
        question1_ui.text = question1;
        question2_ui.text = question2;
        question3_ui.text = question3;
        question4_ui.text = question4;
    }
}
