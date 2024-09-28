using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuizLoader : MonoBehaviour
{
    [SerializeField] private GameObject DATA;
    [SerializeField] private GameObject lobbyItem;
    [SerializeField] private GameObject levelList;
    public static QuizLoader instance;

    private void Start()
    {
        instance = this;
    }

    public void LoadAllQuizzes(Transform parent, int id)
    {
        foreach (Transform child in DATA.transform)
        {
            QuizDataInfo quizData = child.GetComponent<QuizDataInfo>();
            if (quizData != null)
            {
                lobbyItem.GetComponent<LobbyItemInfo>().SetID(id);

                string json = quizData.getJSON();
                Quiz quiz = JsonUtility.FromJson<Quiz>(json);

                lobbyItem.GetComponent<LobbyItemInfo>().SetTitle(quiz.quizName);
                lobbyItem.GetComponent<LobbyItemInfo>().SetDescription(quiz.quizDescription);

                GameObject lobbyItemPrefab = Instantiate(lobbyItem);
                lobbyItemPrefab.transform.SetParent(parent);

                levelList.transform.GetChild(levelList.transform.childCount - 2).SetAsLastSibling();
            }
        }
    }
}
