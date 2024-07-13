using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FarmlandSlot: MonoBehaviour
{
    public GameObject plantedPlant;
    private bool isPlanted = false;
    private PlantData plantData;
    private TulipItemData currentTulip;
    private int daysLeft;
    private Vector2 midPoint;

    private void Start()
    {
        GMFarm.FarmlandPlantDecideEvent += CheckIfMe;
    }
    private void CheckIfMe(FarmlandSlot slot, TulipItemData tulip, SeedItemData seed) 
    {
        if (slot != this)
            return;
        PlantSeed(seed, tulip);
    }
    public void PlantSeed(SeedItemData seed, TulipItemData tulip)
    {
        isPlanted = true;
        daysLeft = seed.DaysToGrow;
        currentTulip = tulip;
        Instantiate(plantedPlant, midPoint, Quaternion.identity);
    }
    public void PlantGrow() 
    {
        // 시간 지날 때 마다 그에 해당하는 성장과정 Sprite 나옴.
    }

    public void Harvest()
    {
        isPlanted = false;
        plantedPlant = GameObject.Find("Field");
        Destroy(plantedPlant);
    }
}