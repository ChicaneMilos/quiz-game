using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditDialog : MonoBehaviour
{
    public static EditDialog instance;
    [SerializeField] private TMP_InputField title;
    [SerializeField] private TMP_InputField question1;
    [SerializeField] private TMP_InputField question2;
    [SerializeField] private TMP_InputField question3;
    [SerializeField] private TMP_InputField question4;

    [SerializeField] private Button correctButton1;
    [SerializeField] private Button correctButton2;
    [SerializeField] private Button correctButton3;
    [SerializeField] private Button correctButton4;
    private QuestionInfo questionInfo;
    [SerializeField] private GameObject EditDialogGO;
    [SerializeField] private int correctAnswer;

    private void Start()
    {
        instance = this;
    }
    public void EditQuestion(GameObject question)
    {
        EditDialogGO.SetActive(true);
        questionInfo = question.GetComponent<QuestionInfo>();

        if (questionInfo != null)
        {
            title.text = questionInfo.getTitle();
            question1.text = questionInfo.getQuestion1();
            question2.text = questionInfo.getQuestion2();
            question3.text = questionInfo.getQuestion3();
            question4.text = questionInfo.getQuestion4();
        }

        ResetCorrectButtons();
        correctButton1.GetComponent<Image>().color = new Color(0f / 255f, 255f / 255f, 60f / 255f, 255f / 255f);
        correctAnswer = 1;
    }

    public void CloseDialog()
    {
        EditDialogGO.SetActive(false);
    }

    public void ConfirmDialog()
    {
        if (questionInfo != null)
        {
            questionInfo.setTitle(title.text);
            questionInfo.setCorrectAnswer(correctAnswer);

            int numberOfAnswers = 0;
            if (question1.text != "")
            {
                numberOfAnswers++;
                questionInfo.setQuestion1(question1.text);
            }

            if (question2.text != "")
            {
                numberOfAnswers++;
                questionInfo.setQuestion2(question2.text);
            }

            if (question3.text != "")
            {
                numberOfAnswers++;
                questionInfo.setQuestion3(question3.text);
            }

            if (question4.text != "")
            {
                numberOfAnswers++;
                questionInfo.setQuestion4(question4.text);
            }

            questionInfo.setNumberOfAnswers(numberOfAnswers);

            title.text = "";
            question1.text = "";
            question2.text = "";
            question3.text = "";
            question4.text = "";
        }
        questionInfo.UpdateValues();
        EditDialogGO.SetActive(false);
    }

    public void SetCorrectAnswer1()
    {
        ResetCorrectButtons();
        correctAnswer = 1;
        correctButton1.GetComponent<Image>().color = new Color(0f / 255f, 255f / 255f, 60f / 255f, 255f / 255f);
    }

    public void SetCorrectAnswer2()
    {
        ResetCorrectButtons();
        correctAnswer = 2;
        correctButton2.GetComponent<Image>().color = new Color(0f / 255f, 255f / 255f, 60f / 255f, 255f / 255f);
    }

    public void SetCorrectAnswer3()
    {
        ResetCorrectButtons();
        correctAnswer = 3;
        correctButton3.GetComponent<Image>().color = new Color(0f / 255f, 255f / 255f, 60f / 255f, 255f / 255f);
    }

    public void SetCorrectAnswer4()
    {
        ResetCorrectButtons();
        correctAnswer = 4;
        correctButton4.GetComponent<Image>().color = new Color(0f / 255f, 255f / 255f, 60f / 255f, 255f / 255f);
    }

    void ResetCorrectButtons()
    {
        correctButton1.GetComponent<Image>().color = Color.white;
        correctButton2.GetComponent<Image>().color = Color.white;
        correctButton3.GetComponent<Image>().color = Color.white;
        correctButton4.GetComponent<Image>().color = Color.white;
    }

}
