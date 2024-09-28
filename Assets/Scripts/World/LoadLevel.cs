using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class LoadLevel : MonoBehaviour
{
    public static LoadLevel instance;
    [SerializeField] private Transform platformParent;
    [SerializeField] private int spawnCoordinate = 15;
    [SerializeField] private GameObject DATA;
    [SerializeField] private GameObject platform2;
    [SerializeField] int platformNumber;
    [SerializeField] private Vector3 platformPos;
    [SerializeField] private Vector3 platformRot;
    List<QuestionData> questionDataList;
    [SerializeField] private int timeLimit;
    [SerializeField] private TextMeshProUGUI timeLimit_UGUI;
    public int totalQuestions = 0;

    [System.Serializable]
    public class QuestionData
    {
        public string questionTitle;
        public string question1;
        public string question2;
        public string question3;
        public string question4;
        public int correctAnswer;
    }

    [System.Serializable]
    public class QuizSettings
    {
        public int time_limit;
    }

    [System.Serializable]
    public class QuizDataWrapper
    {
        public string quizName;
        public string quizDescription;
        public List<QuestionData> questions;
        public QuizSettings settings;
    }

    public void Awake()
    {
        instance = this;

        DATA = GameObject.Find("QuizData(Clone)");
        string jsonString = DATA.GetComponent<QuizDataInfo>().getJSON();

        var wrapper = Load(jsonString);
        questionDataList = wrapper.questions;
        timeLimit = wrapper.settings.time_limit; 
        platformNumber = 0;
        platformPos = new Vector3(0, 0, 0);
        platformRot = new Vector3(0, 0, 0);
        LoadQuestion();
        StartTimeCounter();
    }

    void LoadQuestion()
    {
        if (questionDataList == null || questionDataList.Count == 0 || platformNumber >= questionDataList.Count)
        {
            Debug.LogError("Invalid platformNumber or empty questionDataList.");
            return;
        }

        totalQuestions++;

        var question = questionDataList[platformNumber];

        List<string> answers = new List<string>();

        if (!string.IsNullOrEmpty(question.question1))
        {
            answers.Add(question.question1);
        }

        if (!string.IsNullOrEmpty(question.question2))
        {
            answers.Add(question.question2);
        }

        if (!string.IsNullOrEmpty(question.question3))
        {
            answers.Add(question.question3);
        }

        if (!string.IsNullOrEmpty(question.question4))
        {
            answers.Add(question.question4);
        }

        bool lastPlatform = platformNumber + 1 == questionDataList.Count;

        SpawnPlatform(answers.Count, answers, question.questionTitle, question.correctAnswer, lastPlatform);

        Debug.Log("Title: " + question.questionTitle);
        Debug.Log("Question 1: " + question.question1);
        Debug.Log("Question 2: " + question.question2);
        Debug.Log("Question 3: " + question.question3);
        Debug.Log("Question 4: " + question.question4);
        Debug.Log("Correct Answer: " + question.correctAnswer);
    }

    public void SpawnNextPlatform(GameObject mainPlatform, string doorNumber)
    {
        platformNumber++;
        var questionHandler = mainPlatform.GetComponent<QuestionHandler>();
        Vector3 currentRotation = mainPlatform.transform.eulerAngles;
        switch (doorNumber)
        {
            case "1":
                platformPos = new Vector3(questionHandler.doorTransition1.transform.position.x, 0f, questionHandler.doorTransition1.transform.position.z);
                platformRot = new Vector3(0, currentRotation.y - 45, 0);
                LoadQuestion();
                break;
            case "2":
                platformPos = new Vector3(questionHandler.doorTransition2.transform.position.x, 0, questionHandler.doorTransition2.transform.position.z);
                platformRot = new Vector3(0, currentRotation.y + 45, 0);
                LoadQuestion();
                break;
            case "3":
                platformPos = new Vector3(questionHandler.doorTransition3.transform.position.x, 0, questionHandler.doorTransition3.transform.position.z);
                platformRot = new Vector3(0, currentRotation.y + 135, 0);
                LoadQuestion();
                break;
            case "4":
                platformPos = new Vector3(questionHandler.doorTransition4.transform.position.x, 0, questionHandler.doorTransition4.transform.position.z);
                platformRot = new Vector3(0, currentRotation.y - 135, 0);
                LoadQuestion();
                break;
        }
    }

    void SpawnPlatform(int numberOfAnswers, List<string> answers, string title, int correctAnswer, bool lastPlatform)
    {
        if (numberOfAnswers < 1 || numberOfAnswers > 4) return;

        GameObject platform = Instantiate(platform2);
        platform.transform.parent = platformParent;
        platform.transform.position = platformPos;
        platform.transform.rotation = Quaternion.Euler(platformRot.x, platformRot.y, platformRot.z);
        QuestionHandler pl = platform.GetComponent<QuestionHandler>();
        pl.title.text = title;
        pl.lastPlatform = lastPlatform;
        pl.answers.text = "";

        pl.door1.GetComponent<BoxCollider>().isTrigger = false;
        pl.door2.GetComponent<BoxCollider>().isTrigger = false;
        pl.door3.GetComponent<BoxCollider>().isTrigger = false;
        pl.door4.GetComponent<BoxCollider>().isTrigger = false;

        if (platformNumber == 0)
        {
            pl.platformTransition.GetComponent<BoxCollider>().isTrigger = false;
        }
        else
        {
            pl.platformTransition.GetComponent<BoxCollider>().isTrigger = true;
        }

        switch (answers.Count)
        {
            case 1:
                pl.answers.text += $"A) {answers[0]} \n";
                pl.door1.GetComponent<BoxCollider>().isTrigger = true;
                break;
            case 2:
                pl.answers.text += $"A) {answers[0]} \n";
                pl.answers.text += $"B) {answers[1]} \n";
                pl.door1.GetComponent<BoxCollider>().isTrigger = true;
                pl.door2.GetComponent<BoxCollider>().isTrigger = true;
                break;
            case 3:
                pl.answers.text += $"A) {answers[0]} \n";
                pl.answers.text += $"B) {answers[1]} \n";
                pl.answers.text += $"C) {answers[2]} \n";
                pl.door1.GetComponent<BoxCollider>().isTrigger = true;
                pl.door2.GetComponent<BoxCollider>().isTrigger = true;
                pl.door3.GetComponent<BoxCollider>().isTrigger = true;
                break;
            case 4:
                pl.answers.text += $"A) {answers[0]} \n";
                pl.answers.text += $"B) {answers[1]} \n";
                pl.answers.text += $"C) {answers[2]} \n";
                pl.answers.text += $"D) {answers[3]} \n";
                pl.door1.GetComponent<BoxCollider>().isTrigger = true;
                pl.door2.GetComponent<BoxCollider>().isTrigger = true;
                pl.door3.GetComponent<BoxCollider>().isTrigger = true;
                pl.door4.GetComponent<BoxCollider>().isTrigger = true;
                break;
        }

        switch (correctAnswer)
        {
            case 1:
                pl.door1.GetComponent<DoorState>().setAnswerState(true);
                break;
            case 2:
                pl.door2.GetComponent<DoorState>().setAnswerState(true);
                break;
            case 3:
                pl.door3.GetComponent<DoorState>().setAnswerState(true);
                break;
            case 4:
                pl.door4.GetComponent<DoorState>().setAnswerState(true);
                break;
        }
    }

    public QuizDataWrapper Load(string jsonString)
    {
        QuizDataWrapper wrapper = JsonUtility.FromJson<QuizDataWrapper>(jsonString);

        if (wrapper != null && wrapper.questions != null)
        {
            return wrapper;
        }

        return new QuizDataWrapper
        {
            questions = new List<QuestionData>(),
            settings = new QuizSettings { time_limit = 0 }
        };
    }

    void StartTimeCounter()
    {
        if (timeLimit == 0)
        {
            timeLimit_UGUI.gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(IEnumTimeCounter());
        }
        
    }

    IEnumerator IEnumTimeCounter()
    {
        while (timeLimit > 0)
        {
            timeLimit_UGUI.text = timeLimit.ToString();
            yield return new WaitForSeconds(1f);
            timeLimit -= 1;
        }
        ScoreController.instance.endGame();

    }

    public void ClearPlatforms()
    {
        if (platformParent.transform.childCount >= 3)
        {
            Transform firstChild = platformParent.GetChild(0);
            Destroy(firstChild.gameObject);
        }

    }
}
