using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class LoadLevel : MonoBehaviour
{
    [SerializeField] private int spawnCoordinate = 15;
    [SerializeField] private GameObject DATA;

    [SerializeField] private GameObject platform2;

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

    [System.Serializable]
    public class Wrapper<T>
    {
        public List<T> list;
    }

    public void Awake()
    {
        spawnCoordinate = 15;
        DATA = GameObject.Find("QuizData(Clone)");
        string jsonString = DATA.GetComponent<QuizData>().getJSON();

        List<QuestionData> questionDataList = Load(jsonString);

        int i = 0;
        foreach (var question in questionDataList)
        {
            i++;
            List<string> answers = new List<string>();

            if (question.question1 != "")
            {
                answers.Add(question.question1);
            }

            if (question.question2 != "")
            {
                answers.Add(question.question2);
            }

            if (question.question3 != "")
            {
                answers.Add(question.question3);
            }

            if (question.question4 != "")
            {
                answers.Add(question.question4);
            }

            bool lastPlatform = false;
            if (i == questionDataList.Count)
            {
                lastPlatform = true;
            }

            SpawnPlatform(answers.Count, answers, question.title, question.correctAnswer, lastPlatform);

            Debug.Log("Title: " + question.title);
            Debug.Log("Question 1: " + question.question1);
            Debug.Log("Question 2: " + question.question2);
            Debug.Log("Question 3: " + question.question3);
            Debug.Log("Question 4: " + question.question4);
            Debug.Log("Correct Answer: " + question.correctAnswer);
        }
    }

    void SpawnPlatform(int numberOfAnswers, List<string> answers, string title, int correctAnswer, bool lastPlatform)
    {
        switch (numberOfAnswers)
        {
            case 2:
                GameObject platform = Instantiate(platform2);
                platform.transform.position = new Vector3(0, 0, spawnCoordinate);
                spawnCoordinate += 20;
                QuestionHandlerTWO pl = platform.GetComponent<QuestionHandlerTWO>();
                pl.title.text = title;
                pl.lastPlatform = lastPlatform;
                //answers.Shuffle();
                pl.answer1.text = answers[0];
                pl.answer2.text = answers[1];

                switch (correctAnswer)
                {
                    case 1:
                        pl.door1.GetComponent<DoorState>().setAnswerState(true);
                        break;
                    case 2:
                        pl.door2.GetComponent<DoorState>().setAnswerState(true);
                        break;
                }
                break;
        }
    }
    public List<QuestionData> Load(string jsonString)
    {
        Wrapper<QuestionData> wrapper = JsonUtility.FromJson<Wrapper<QuestionData>>(jsonString);
        return wrapper.list;
    }
}

    public static class ListExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }


