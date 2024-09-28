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
    [SerializeField] private TextMeshProUGUI endState_reward_Value;

    public int getScore() {  return score; }
    public void setScore(int value) { score = value; }

    public void endGame() {  
        gameEnded = true;
        player.canMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        EndGO.SetActive(true);
        endState_Title.text = "Kviz je zavrsen.";
        float percentage = (((float)score / LoadLevel.instance.totalQuestions) * 100f);
        int intPercentage = (int)percentage;
        endState_correctAnswers_Value.text = intPercentage.ToString();
        endState_totalTime_Value.text = time.ToString();

        if (percentage == 100f)
        {
            endState_reward_Value.text = "1st";
        }
        else if (percentage >= 80f)
        {
            endState_reward_Value.text = "2nd";
        }
        else if (percentage >= 60f)
        {
            endState_reward_Value.text = "3rd";
        }
        else if (percentage >= 40f)
        {
            endState_reward_Value.text = "4th";
        }
        else if (percentage >= 20f)
        {
            endState_reward_Value.text = "5th";
        }
        else
        {
            endState_reward_Value.text = ":(";
        }
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
