using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;
    [SerializeField] private GameObject LevelSelector;
    [SerializeField] private GameObject LevelCreator;
    [SerializeField] private GameObject LevelList;
    [SerializeField] private GameObject DATA;
    [SerializeField] private GameObject quizData;
    [SerializeField] private GameObject lobbyItem;

    [SerializeField] private int selectedLevel = 0;
    private int id_Counter = 0;

    // Start is called before the first frame update
    private void Start()
    {
        instance = this;
        LevelSelector.SetActive(true);
        LevelCreator.SetActive(false);
    }

    public void CreateLevel()
    {
        LevelSelector.SetActive(false);
        LevelCreator.SetActive(true);
        GameObject quiz = Instantiate(quizData, DATA.transform);
        GameObject item = Instantiate(lobbyItem, LevelList.transform);
        quiz.GetComponent<QuizData>().SetID(id_Counter);
        selectedLevel = id_Counter;
        item.GetComponent<LobbyItemInfo>().SetID(id_Counter);
        id_Counter++;
        LevelList.transform.GetChild(LevelList.transform.childCount - 2).SetAsLastSibling();
    }

    public void ExitCreator()
    {
        LevelSelector.SetActive(true);
        LevelCreator.SetActive(false);
    }

    public void SaveJSONData(string JSON)
    {
        foreach(Transform dataOBJ in DATA.transform)
        {
            QuizData qd = dataOBJ.GetComponent<QuizData>();
            if (qd.getID() == selectedLevel)
            {
                qd.setJSON(JSON);
                break;
            }
        }
    }

    //Used to cleanup data for play
    public void DeleteOtherData(int id)
    {
        foreach (Transform dataOBJ in DATA.transform)
        {
            QuizData qd = dataOBJ.GetComponent<QuizData>();
            if (qd.getID() != id)
            {
                Destroy(dataOBJ.gameObject);
            }
        }
    }

}
