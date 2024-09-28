using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyItemInfo : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private string title;
    [SerializeField] private string description;
    [SerializeField] private TextMeshProUGUI titleUGUI;
    [SerializeField] private TextMeshProUGUI descriptionUGUI;

    public int GetID()
    {
        return id;
    }

    public string GetTitle()
    {
        return title;
    }

    public string GetDescription()
    {
        return description;
    }

    public void SetID(int id_)
    {
        id = id_;
    }

    public void SetTitle(string title_)
    {
        title = title_;
        titleUGUI.text = title_;
    }

    public void SetDescription(string description_)
    { 
        description = description_;
        descriptionUGUI.text = description_;
    }
}
