using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyItemInfo : MonoBehaviour
{
    [SerializeField] private int id;

    public int GetID()
    {
        return id;
    }

    public void SetID(int id_)
    {
        id = id_;
    }
}
