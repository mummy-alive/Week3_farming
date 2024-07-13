using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class FarmlandSlot: MonoBehaviour
{    public GameObject plantedPlantPrefab ;
    private bool isPlanted = false;
    private TulipItemData currentTulip;
    private int daysLeft;
    private Vector2 midPoint;

    private void Start()
    {
        GMFarm.FarmlandPlantDecideEvent += CheckIfMe;
        midPoint = transform.position;
    }
    private void OnMouseDown()
    {
         Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         print("This is working");
         GMFarm.Instance.PlantOnFarmland(this, clickPosition);
    }
    /*private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) 
        {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            GMFarm.Instance.PlantOnFarmland(this, clickPosition);
        }
    }*/
    private void CheckIfMe(FarmlandSlot slot, TulipItemData tulip, SeedItemData seed, Vector2 position) 
    {
        if (slot != this)
            return;
        PlantSeed(seed, tulip, position);
    }
    public void PlantSeed(SeedItemData seed, TulipItemData tulip, Vector2 Position)
    {
        if(isPlanted) return;

        isPlanted = true;
        daysLeft = seed.DaysToGrow;
        currentTulip = tulip;
        Instantiate(plantedPlantPrefab, midPoint, Quaternion.identity);
    }
    public void PlantGrow() 
    {
        // 시간 지날 때 마다 그에 해당하는 성장과정 Sprite 나옴.
    }

    public void Harvest()
    {
        isPlanted = false;
        plantedPlantPrefab = GameObject.Find("Field");
        Destroy(plantedPlantPrefab);
    }

    private void OnDestroy()
    {
        GMFarm.FarmlandPlantDecideEvent -= CheckIfMe;
    }
}