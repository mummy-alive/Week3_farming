using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMGold : MonoBehaviour
{
    public static event Action<int> GoldAmountChangeEvent;
    public static event Action<int> DebtAmountChangeEvent;
    public int CurrGoldAmount{get; private set;}
    public int CurrDebtAmount{get; private set;}

    public void EarnGold(int amount)
    {
        CurrGoldAmount += amount;
        GoldAmountChangeEvent?.Invoke(CurrGoldAmount);
    }
    public bool CheckAndUseGold (int amount)
    {
        if (CurrGoldAmount < amount) 
            return false;
        else
        {
            CurrGoldAmount -= amount;
            GoldAmountChangeEvent?.Invoke(CurrGoldAmount);
            return true;
        }
    }
    
    public bool CheckAndPayDebt(int amount)
    {
        if (CurrDebtAmount < amount) 
            return false;
        else
        {
            CurrDebtAmount -= amount;
            DebtAmountChangeEvent?.Invoke(CurrDebtAmount);
            return true;
        } 
    }
    public void IncreaseDebt(int amount)
    {
        CurrDebtAmount += amount;
        DebtAmountChangeEvent?.Invoke(CurrDebtAmount);
    }

    private void Start()
    {
        GoldAmountChangeEvent?.Invoke(CurrGoldAmount);
        DebtAmountChangeEvent?.Invoke(CurrDebtAmount);
    }

    private void AddInterest()
    {
        Debug.Log("This is running.. add interest");
        CurrDebtAmount = (int)(1.05 * CurrDebtAmount);
        DebtAmountChangeEvent?.Invoke(CurrDebtAmount);
        Debug.Log("This is running.. add interest1111");

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
        GMClock.DayChangeEvent += AddInterest;
    }
}
