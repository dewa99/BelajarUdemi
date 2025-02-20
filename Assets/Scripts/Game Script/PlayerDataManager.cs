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

    public static void OnPlayerCollectTreeEvent()
    {
        playerCollectTreeEvent?.Invoke();
    }
}