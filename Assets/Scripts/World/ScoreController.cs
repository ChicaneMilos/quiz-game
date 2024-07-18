using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public static ScoreController instance;
    [SerializeField] private int score = 0;
    [SerializeField] private int time = 0;
    [SerializeField] bool gameEnded = false;
    [SerializeField] private FPSController player;

    [SerializeField] private GameObject EndGO;
    [SerializeField] private TextMeshProUGUI endState_Title;
    [SerializeField] private TextMeshProUGUI endState_correctAnswers_Value;
    [SerializeField] private TextMeshProUGUI endState_totalTime_Value;

    public int getScore() {  return score; }
    public void setScore(int value) { score = value; }

    public void endGame(bool finishedQuiz) {  
        gameEnded = true;
        player.canMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        EndGO.SetActive(true);
        if (finishedQuiz)
            endState_Title.text = "Uspesno ste presli kviz!";
        else
            endState_Title.text = "Odgovor je netacan!";
        endState_correctAnswers_Value.text = score.ToString();
        endState_totalTime_Value.text = time.ToString() + "s";
    }

    public void IncreaseScore()
    {
        score++;
    }

    public int getTime() { return time; }

    private void Awake()
    {
        instance = this;
        StartCoroutine(CountTime());
        EndGO.SetActive(false);
    }

    IEnumerator CountTime()
    {
        while (!gameEnded)
        {
            yield return new WaitForSeconds(1f);
            time++;
        }
    }
}
