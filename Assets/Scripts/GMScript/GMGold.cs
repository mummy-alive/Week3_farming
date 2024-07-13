using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMGold : MonoBehaviour
{
    public static event Action<int> GoldAmountChangeEvent;
    public static event Action<int> DebtAmountChangeEvent;
    [SerializeField] private int _currGoldAmount;
    [SerializeField] private int _currDebtAmount;

    public void EarnGold(int amount)
    {
        _currGoldAmount += amount;
        GoldAmountChangeEvent?.Invoke(_currGoldAmount);
    }
    public bool CheckAndUseGold (int amount)
    {
        if (_currGoldAmount < amount) 
            return false;
        else
        {
            _currGoldAmount -= amount;
            GoldAmountChangeEvent?.Invoke(_currGoldAmount);
            return true;
        }
    }
    public bool CheckAndPayDebt(int amount)
    {
        if (_currDebtAmount < amount) 
            return false;
        else
        {
            _currDebtAmount -= amount;
            DebtAmountChangeEvent?.Invoke(_currDebtAmount);
            return true;
        } 
    }
    public void IncreaseDebt(int amount)
    {
        _currDebtAmount += amount;
        DebtAmountChangeEvent?.Invoke(_currDebtAmount);
    }

    private void Start()
    {
        GoldAmountChangeEvent?.Invoke(_currGoldAmount);
        DebtAmountChangeEvent?.Invoke(_currDebtAmount);
    }

    // Make GMGold a singleton object
    private static GMGold _instance;
    public static GMGold Instance
    {
        get {
            if(!_instance)
            {
                _instance = FindObjectOfType(typeof(GMGold)) as GMGold;
                if (_instance == null)
                    Debug.Log("no Singleton goldManager");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null) _instance = this;
        else if (_instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
}
