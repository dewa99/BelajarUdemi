using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager _instance = null;
    [SerializeField] private int _wood = 0;
    

    public static event Action playerCollectTreeEvent;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        playerCollectTreeEvent += IncreaseWood;
    }

    public void SetWood(int value)
    {
        _wood = value;
    }

    public int GetWood()
    {
        return _wood;
    }

    private void IncreaseWood()
    {
        _wood++;
    }

    public static void OnPlayerCollectTreeEvent() // this will causing coupling, dont invoke and subscribe the own class event and the function no need to be static ===> rename it like "public void OnPlayerCollectTree()" dont put Event on the function name
    {
        //call the GetWood();
        playerCollectTreeEvent?.Invoke();
        //if your concern this event invoked so its trigger the UI you better create another event that trigger the UI update, dont use the same event for mechanic and UI
        //example playerScoreEvent?.Invoke()
        //the key is create manager for the UI and trigger the UIManager event;
    }
}