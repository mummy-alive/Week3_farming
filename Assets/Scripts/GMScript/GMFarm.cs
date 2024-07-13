using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class  GMFarm: MonoBehaviour
{
    public static event Action<FarmlandSlot, TulipItemData, SeedItemData> FarmlandPlantDecideEvent;
    [SerializeField]
    private List<FarmlandSlot> _farmlandSlotList = new List<FarmlandSlot>();
    private SeedItemData selectedSeed;

    public void SelectSeed(SeedItemData seed)
    {
        selectedSeed = seed;
    }
    public void PlantOnFarmland(FarmlandSlot slot) // use map_to_world to get absolute world position?
    {   
        if(selectedSeed == null)
        {
            Debug.Log("No seed selected!");
            return;
        }
        bool isSeed = CheckIfSeed();
        if (isSeed)
        {
            TulipItemData tulip = GMRandomBloom.RandBloom(selectedSeed);
            slot.PlantSeed(selectedSeed, tulip);
            FarmlandPlantDecideEvent?.Invoke(slot, tulip, selectedSeed);
        }
        else
        {
            Debug.Log("Not enough seeds left to plant!");
        }

    }

    public bool CheckIfSeed()
    {
        return true;
    }

    public PlantData GetRandomPlant(SeedItemData seed)
    {
        PlantData plantData = null;
        return plantData;
    }

    public void Harvest(Vector2 slotPosition)
    {


    }
}