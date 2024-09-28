using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EditQuiz : MonoBehaviour
{
    [SerializeField] private GameObject editQuizGO;
    [SerializeField] TMP_InputField title;
    [SerializeField] TMP_InputField description;

    [SerializeField] private TMP_Text quizTitle;
    [SerializeField] private TMP_Text quizDescription;

    private void Start()
    {
        QuestionController.instance.SetQuizInfo("Novi kviz", "Ovo je opis kviza koji korisnik moze da unese ili da izmeni.");
    }

    public void Save()
    {
        QuestionController.instance.SetQuizInfo(title.text, description.text);
        CloseEditQuiz();
    }

    public void LoadEditQuiz()
    {
        title.text = quizTitle.text;
        description.text = quizDescription.text;
    }

    public void OpenEditQuiz()
    {
        LoadEditQuiz();
        editQuizGO.SetActive(true);
    }

    public void CloseEditQuiz()
    {
        editQuizGO.SetActive(false);
    }
}
