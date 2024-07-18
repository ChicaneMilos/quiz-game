using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class QuestionController : MonoBehaviour
{
    public static QuestionController instance;
    [SerializeField] private int numberOfQuestions = 2;
    [SerializeField] private GameObject questionPrefab;
    [SerializeField] private GameObject questionsGrid;
    [SerializeField] private GameObject addAnswerButton;
    [SerializeField] List<GameObject> questionList = new List<GameObject>();
    [SerializeField] private LevelController levelcontroller;
    private void Start()
    {
        instance = this;
    }

    public void DecrementQuestions()
    {
        numberOfQuestions--;
    }

    public int ReturnNumberOfQuestions()
    {
        return numberOfQuestions;
    }

    public void RemoveFromAnswerList(GameObject obj)
    {
        questionList.Remove(obj);
        Debug.Log("Removed from answer list.");

        if(numberOfQuestions < 12)
        {
            addAnswerButton.SetActive(true);
        }
    }

    public void AddQuestion()
    {
        if (numberOfQuestions < 12)
        {
            GameObject question = Instantiate(questionPrefab, questionsGrid.transform);
            questionList.Add(question);
            EditDialog.instance.EditQuestion(question);
            numberOfQuestions++;
            questionsGrid.transform.GetChild(questionsGrid.transform.childCount - 2).SetAsLastSibling();

            if (numberOfQuestions == 12)
            {
                addAnswerButton.SetActive(false);
            }
        }

    }

    public void SaveQuestions()
    {
        Save();
    }

    [System.Serializable]
    public class QuestionData
    {
        public string title;
        public string question1;
        public string question2;
        public string question3;
        public string question4;
        public int correctAnswer;
    }

    public void Save()
    {
        List<QuestionData> data = new List<QuestionData>();

        foreach (GameObject obj in questionList)
        {
            QuestionInfo qInfo = obj.GetComponent<QuestionInfo>();
            if (qInfo != null)
            {
                QuestionData qData = new QuestionData
                {
                    title = qInfo.getTitle(),
                    question1 = qInfo.getQuestion1(),
                    question2 = qInfo.getQuestion2(),
                    question3 = qInfo.getQuestion3(),
                    question4 = qInfo.getQuestion4(),
                    correctAnswer = qInfo.getCorrectAnswer()
                };
                data.Add(qData);
            }
        }

        string json = JsonUtility.ToJson(new Wrapper(data));
        levelcontroller.SaveJSONData(json);
        //File.WriteAllText(Application.dataPath + "/questionData.json", json);
    }

    [System.Serializable]
    private class Wrapper
    {
        public List<QuestionData> list;
        public Wrapper(List<QuestionData> list)
        {
            this.list = list;
        }
    }

}
