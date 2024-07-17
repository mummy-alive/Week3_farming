using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEditor.PackageManager;
using UnityEngine;

public class GMBank : MonoBehaviour
{
    public static event Action<int> FarmlandSlotStatusChange;
    public bool[] FarmlandSlotStatus = new bool [16];
    

    public bool BuyNewSlot()
    {

            int recentSlot = GetAvailableSlot();
            //Before adding this, Don't forget to mark 0~3rd slot to TRUE
            if (recentSlot <4 || recentSlot >= 16)
            {
                Debug.Log("You have bought all available land.");
                return false;
            }
            bool bought = GMGold.Instance.CheckAndUseGold(MyConst.FARM_SLOT_PRICE[recentSlot]);
            if (!bought)
            {
                Debug.Log("You don't have enough money.");
                return false;
            }
            ChangeStatus(recentSlot);
            return true;

    }
    public void SetInitialStatus()
    {
        for (int i = 0; i < 4; i++)
        {
            ChangeStatus(i);
        }
    }

    private void ChangeStatus(int recentSlot)
    {
        FarmlandSlotStatus[recentSlot] = true;
        FarmlandSlotStatusChange?.Invoke(recentSlot);
    }

    public int GetAvailableSlot()
    {
        for (int i = 0; i < MyConst.FARM_SLOT_NUM; i++)
            if (!FarmlandSlotStatus[i])
                return i;
        return -1;
    }

    // Make GMClock a singleton object
    private static GMBank _instance;
    // 인스턴스에 접근하기 위한 프로퍼티
    public static GMBank Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GMBank)) as GMBank;
                if (_instance == null)
                    Debug.Log("no Singleton clock");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null) _instance = this;
        else if (_instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        SetInitialStatus();
    }
}