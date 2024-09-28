using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class QuizSettings
{
    public int time_limit;
}

[System.Serializable]
public class Question
{
    public string questionTitle;
    public string question1;
    public string question2;
    public string question3;
    public string question4;
    public int correctAnswer;
}

[System.Serializable]
public class Quiz
{
    public string quizName;
    public string quizDescription;
    public List<Question> questions;
    public QuizSettings settings;
}

public class LevelController : MonoBehaviour
{
    public static LevelController instance;
    [SerializeField] private GameObject LevelSelector;
    [SerializeField] private GameObject LevelCreator;
    [SerializeField] private GameObject LevelList;
    [SerializeField] private GameObject DATA;
    [SerializeField] private GameObject quizData;
    [SerializeField] private GameObject lobbyItem;
    [SerializeField] private GameObject addQuizPref;

    [SerializeField] private int selectedLevel = 0;
    private int id_Counter = 0;

    public void SetSelectedLevel(int level)
    {
        selectedLevel = level;
    }

    public void EnableCreator()
    {
        LevelSelector.SetActive(false);
        LevelCreator.SetActive(true);
    }

    private void Start()
    {
        instance = this;
        LevelSelector.SetActive(true);
        LevelCreator.SetActive(false);
        LoadAllQuizzes();
    }

    void ResetQuizes()
    {
        id_Counter = 0;
        foreach (Transform child in LevelList.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void CreateLevel()
    {
        QuestionController.instance.ResetData();
        LevelSelector.SetActive(false);
        LevelCreator.SetActive(true);
        GameObject quiz = Instantiate(quizData, DATA.transform);
        quiz.GetComponent<QuizDataInfo>().SetID(id_Counter);
        selectedLevel = id_Counter;
        id_Counter++;
        LevelList.transform.GetChild(LevelList.transform.childCount - 2).SetAsLastSibling();
    }

    public void LoadAllQuizzes()
    {
        ResetQuizes();
        foreach (Transform child in DATA.transform)
        {
            Debug.Log("foreached");
            QuizDataInfo quizData = child.GetComponent<QuizDataInfo>();
            if (quizData != null)
            {
                lobbyItem.GetComponent<LobbyItemInfo>().SetID(id_Counter);

                string json = quizData.getJSON();
                Quiz quiz = JsonUtility.FromJson<Quiz>(json);

                lobbyItem.GetComponent<LobbyItemInfo>().SetTitle(quiz.quizName);
                lobbyItem.GetComponent<LobbyItemInfo>().SetDescription(quiz.quizDescription);

                GameObject lobbyItemPrefab = Instantiate(lobbyItem);
                lobbyItemPrefab.transform.SetParent(LevelList.transform);
                LevelList.transform.GetChild(LevelList.transform.childCount - 2).SetAsLastSibling();
            }
            id_Counter++;
        }
        GameObject addQuiz = Instantiate(addQuizPref, LevelList.transform);
    }

    public void ExitCreator()
    {
        LevelSelector.SetActive(true);
        LevelCreator.SetActive(false);
        LoadAllQuizzes();
    }


    public void DeleteOtherData(int id)
    {
        foreach (Transform dataOBJ in DATA.transform)
        {
            QuizDataInfo qd = dataOBJ.GetComponent<QuizDataInfo>();
            if (qd.getID() != id)
            {
                Destroy(dataOBJ.gameObject);
            }
        }
    }

    public void SaveJSONData(string JSON)
    {
        Debug.Log(selectedLevel);
        foreach(Transform dataOBJ in DATA.transform)
        {
            QuizDataInfo qd = dataOBJ.GetComponent<QuizDataInfo>();
            if (qd.getID() == selectedLevel)
            {
                qd.setJSON(JSON);
                break;
            }
        }
    }

    public void DeleteData(int id)
    {
        foreach (Transform dataOBJ in DATA.transform)
        {
            QuizDataInfo qd = dataOBJ.GetComponent<QuizDataInfo>();
            if (qd.getID() == id)
            {
                Destroy(dataOBJ.gameObject);
            }
        }
    }

    public void ExportJSON()
    {
        string exportJSON;
        foreach (Transform dataOBJ in DATA.transform)
        {
            QuizDataInfo qd = dataOBJ.GetComponent<QuizDataInfo>();
            if (qd.getID() == selectedLevel)
            {
                exportJSON = qd.getJSON();
                SaveJsonToDocuments(exportJSON);
                break;
            }
        }
    }

    public void SaveJsonToDocuments(string jsonData)
    {
        string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        string fileName = "quiz_data.json";
        string fullPath = Path.Combine(documentsPath, fileName);
        File.WriteAllText(fullPath, jsonData);
        Debug.Log($"File saved to {fullPath}");
    }

    public void ImportJSON()
    {
        string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        string fileName = "quiz_data.json";
        string fullPath = Path.Combine(documentsPath, fileName);

        if (File.Exists(fullPath))
        {
            string jsonData = File.ReadAllText(fullPath);
            Quiz quiz = JsonUtility.FromJson<Quiz>(jsonData);
            GameObject newQuiz = Instantiate(quizData, DATA.transform);
            QuizDataInfo quizDataComponent = newQuiz.GetComponent<QuizDataInfo>();
            quizDataComponent.SetID(id_Counter);
            quizDataComponent.setJSON(jsonData);
            id_Counter++;
            LoadAllQuizzes();
            Debug.Log("Quiz imported successfully from " + fullPath);
        }
        else
        {
            Debug.LogError("File not found at " + fullPath);
        }
    }
}
