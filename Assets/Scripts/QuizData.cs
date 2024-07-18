using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizData : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private string JSON_Data;

    private void Start()
    {
        DontDestroyOnLoad(gameObject.transform.parent);
    }

    public void SetID(int id)
    {
        this.id = id;
    }

    public void setJSON(string json) { JSON_Data = json; }
    public string getJSON() { return JSON_Data; }
    public int getID() { return id; }
}
