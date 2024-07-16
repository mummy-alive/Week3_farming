using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEditor.PackageManager;
using UnityEngine;

public class GMBank : MonoBehaviour
{
    public static GMBank Instance { get; private set; }
    public static event Action<int> FarmlandSlotStatusChange;
    public bool[] FarmlandSlotStatus = new bool [16];
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        for (int i = 0; i < 4; i++)
        {
            ChangeStatus(i);
        }
       
    }

    public bool BuyNewSlot()
    {

            int recentSlot = GetAvailableSlot();
            //Before adding this, Don't forget to mark 0~3rd slot to TRUE
            if (recentSlot <4)
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

    private void ChangeStatus(int recentSlot)
    {
        FarmlandSlotStatus[recentSlot] = true;
        FarmlandSlotStatusChange?.Invoke(recentSlot);
    }

    private int GetAvailableSlot()
    {
        for (int i = 0; i < MyConst.FARM_SLOT_NUM; i++)
            if (!FarmlandSlotStatus[i])
                return i;
        return -1;
    }
}