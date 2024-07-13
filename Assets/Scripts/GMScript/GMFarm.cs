using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class  GMFarm: MonoBehaviour
{
    public static event Action<TileFarmlandSlot, PlantData> FarmlandPlantDecideEvent;
    [SerializeField]
    private List<TileFarmlandSlot> _farmlandSlotList = new List<TileFarmlandSlot>();
    private SeedItemData selectedSeed;

    public void SelectSeed(SeedItemData seed)
    {
        selectedSeed = seed;
    }
    public void PlantOnFarmland(TileFarmlandSlot tile) // use map_to_world to get absolute world position?
    {   
        if(selectedSeed == null)
        {
            Debug.Log("No seed selected!");
            return;
        }
        int remainSeed = FindRemainAmount(selectedSeed);
        if (remainSeed > 0)
        {
            //GMRandomBloom.RandBloom(selectedSeed);
            //tile.PlantSeed(selectedSeed);
            //FarmlandPlantDecideEvent?.Invoke(tile, plant);
        }
        else
        {
            Debug.Log("Not enough seeds left to plant!");
        }

    }

    public int FindRemainAmount(SeedItemData seed) //Return how much is left in the inventory
    {
        int cnt=5;
        return cnt;
    }

    public PlantData GetRandomPlant(SeedItemData seed) //Add randomFunction
    {
        PlantData plantData = null;
        return plantData;
    }

    public void Harvest(Vector2 tilePosition)
    {


    }
}