using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using static LoadLevel;

public class QuestionController : MonoBehaviour
{
    public static QuestionController instance;
    [SerializeField] private int numberOfQuestions = 0;
    [SerializeField] private GameObject questionPrefab;
    [SerializeField] private GameObject questionsGrid;
    [SerializeField] private GameObject addAnswerButton;
    [SerializeField] private TMP_InputField timeLimit_Input;
    [SerializeField] List<GameObject> questionList = new List<GameObject>();
    [SerializeField] private LevelController levelcontroller;
    [SerializeField] private GameObject DATA;

    [SerializeField] private int time_limit;

    List<QuestionData> questionDataList;

    [SerializeField] private string quizName = "Unnamed Quiz";
    [SerializeField] private string quizDescription = "A very interesting quiz!";

    [SerializeField] private TextMeshProUGUI quizNameText;
    [SerializeField] private TextMeshProUGUI quizDescriptionText;

    private void Start()
    {
        instance = this;
    }

    public void ResetData()
    {
        foreach (Transform child in questionsGrid.transform)
        {
            if (child.name != "AddItem")
            {
                Destroy(child.gameObject);
            }
        }

        numberOfQuestions = 0;
        timeLimit_Input.text = "0"; // Resetovanje time limit input polja
        questionList.Clear(); // Čišćenje postojeće liste pitanja

        SetQuizInfo("Novi kviz", "Ovo je opis kviza koji korisnik moze da unese ili da izmeni.");
    }

    public void LoadQuiz(int id)
    {
        ResetData();
        LevelController.instance.EnableCreator();
        // Postavljanje naziva i opisa kviza



        // Pretraga kroz sve child objekte u DATA transformu
        foreach (Transform child in DATA.transform)
        {
            // Provera da li je ID kviza tačan
            if (child.GetComponent<QuizDataInfo>().getID() == id)
            {
                // Učitavanje JSON podataka
                var wrapper = Load(child.GetComponent<QuizDataInfo>().getJSON());
                quizNameText.text = wrapper.quizName;
                quizName = wrapper.quizName;
                quizDescriptionText.text = wrapper.quizDescription;
                quizDescription = wrapper.quizDescription;
                questionDataList = wrapper.questions;

                // Prolazak kroz sva pitanja iz liste questionDataList
                foreach (var question in questionDataList)
                {
                    // Instanciranje novog pitanja iz prefab-a
                    GameObject newQuestion = Instantiate(questionPrefab, questionsGrid.transform);
                    questionList.Add(newQuestion);
                    numberOfQuestions++;

                    // Postavljanje pitanja na poslednju poziciju u grid-u
                    questionsGrid.transform.GetChild(questionsGrid.transform.childCount - 2).SetAsLastSibling();

                    // Onemogućavanje dugmeta za dodavanje pitanja ako je dostignut maksimum
                    if (numberOfQuestions == 12)
                    {
                        addAnswerButton.SetActive(false);
                    }

                    // Popunjavanje informacija o pitanju
                    QuestionInfo questionInfo = newQuestion.GetComponent<QuestionInfo>();

                    // Postavljanje svih atributa pitanja
                    if (!string.IsNullOrEmpty(question.questionTitle))
                    {
                        questionInfo.setTitle(question.questionTitle);
                    }

                    if (!string.IsNullOrEmpty(question.question1))
                    {
                        questionInfo.setQuestion1(question.question1);
                    }

                    if (!string.IsNullOrEmpty(question.question2))
                    {
                        questionInfo.setQuestion2(question.question2);
                    }

                    if (!string.IsNullOrEmpty(question.question3))
                    {
                        questionInfo.setQuestion3(question.question3);
                    }

                    if (!string.IsNullOrEmpty(question.question4))
                    {
                        questionInfo.setQuestion4(question.question4);
                    }

                    questionInfo.setCorrectAnswer(question.correctAnswer);

                    // Podešavanje broja odgovora (broji se koliko pitanja nije prazno)
                    int answerCount = 0;
                    if (!string.IsNullOrEmpty(question.question1)) answerCount++;
                    if (!string.IsNullOrEmpty(question.question2)) answerCount++;
                    if (!string.IsNullOrEmpty(question.question3)) answerCount++;
                    if (!string.IsNullOrEmpty(question.question4)) answerCount++;

                    questionInfo.setNumberOfAnswers(answerCount);

                    questionInfo.UpdateValues();
                }

                // Postavljanje vremenskog ograničenja iz JSON-a u odgovarajuće polje
                timeLimit_Input.text = wrapper.settings.time_limit.ToString();
                break;
            }
           
        }
    }


    public void SetQuizInfo(string quizName, string quizDescription)
    {
        this.quizName = quizName;
        quizNameText.text = quizName;
        quizDescriptionText.text = quizDescription;
        this.quizDescription= quizDescription;
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

        if (numberOfQuestions < 12)
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

        public QuizSettings(int timeLimit)
        {
            time_limit = timeLimit;
        }
    }

    [System.Serializable]
    public class QuizData
    {
        public string quizName;
        public string quizDescription;
        public List<QuestionData> questions;
        public QuizSettings settings;

        public QuizData(string name, string description, List<QuestionData> questionList, QuizSettings quizSettings)
        {
            quizName = name;
            quizDescription = description;
            questions = questionList;
            settings = quizSettings;
        }
    }

    public void SaveQuestions()
    {
        List<QuestionData> data = new List<QuestionData>();

        foreach (GameObject obj in questionList)
        {
            QuestionInfo qInfo = obj.GetComponent<QuestionInfo>();
            if (qInfo != null)
            {
                QuestionData qData = new QuestionData
                {
                    questionTitle = qInfo.getTitle(),
                    question1 = qInfo.getQuestion1(),
                    question2 = qInfo.getQuestion2(),
                    question3 = qInfo.getQuestion3(),
                    question4 = qInfo.getQuestion4(),
                    correctAnswer = qInfo.getCorrectAnswer()
                };
                data.Add(qData);
            }
        }

        int timeLimit;
        if (timeLimit_Input.text == "")
        {
            timeLimit = 0;
        }
        else
        {
            timeLimit = int.Parse(timeLimit_Input.text);
        }


        QuizSettings settings = new QuizSettings(timeLimit);

        QuizData quizData = new QuizData(quizName, quizDescription, data, settings);
        string json = JsonUtility.ToJson(quizData);
        Debug.Log(json);
        levelcontroller.SaveJSONData(json);
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
            //settings = new QuizSettings {time_limit}
        };
    }

    [System.Serializable]
    public class QuizDataWrapper
    {
        public string quizName;
        public string quizDescription;
        public List<QuestionData> questions;
        public QuizSettings settings;
    }
}
