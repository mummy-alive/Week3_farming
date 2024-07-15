using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class FarmlandSlot: MonoBehaviour
{    
    public GameObject plantedPlantPrefab;
    public GameObject plantedPlantInstance;
    private bool isPlanted = false;
    private TulipItemData currentTulip;
    private int daysLeft;
    private bool isAlreadyWatered = false;
    private Vector2 midPoint;
    private bool playerIsOnSlot = false;

    Renderer rend;

    private void Start()
    {
        GMFarm.FarmlandPlantDecideEvent += CheckIfMe;
        GMClock.DayChangeEvent += FarmDateChange;
        midPoint = transform.position;
        rend = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (playerIsOnSlot && Input.GetKeyDown(KeyCode.O))
        {
            if (!isPlanted)
            {
                //Ask if player's really gonna plant em
                GMFarm.Instance.PlantOnFarmland(this, midPoint);
            }
            else
                GMFarm.Instance.HarvestOnFarmland(this);

        }
        if (playerIsOnSlot && Input.GetKeyDown(KeyCode.O))
        {
            if (!isAlreadyWatered)
                GMFarm.Instance.WaterOnFarmland(this);
        }
    }

    private void FarmDateChange()
    {
        isAlreadyWatered = false;  
        // 식물 자라는 코드 추가
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsOnSlot = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsOnSlot = false;
        }
    }

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
        plantedPlantInstance = Instantiate(plantedPlantPrefab, midPoint, Quaternion.identity);
        plantedPlantInstance.transform.SetParent(gameObject.transform);
    }
    public void WaterPlant() 
    {
        if (daysLeft <= 0)
        {
            Debug.Log("This plant is watered enough!");
            return;
        }
        if (!isAlreadyWatered)
        {
            isAlreadyWatered = true;
            daysLeft--;
            print("Watered successfully!");
            GrowProgress();
        }
        else
            Debug.Log("This plant is already watered! Try tomorrow");         
    }

    private void GrowProgress()
    {
        // 시간 지날 때 마다 그에 해당하는 성장과정 Sprite 나옴.
    }
    public TulipItemData Harvest()
    {
        //daysLeft = 0;     //for debugging harvest
        if (daysLeft <= 0)
        {
            isPlanted = false;
            if (plantedPlantPrefab != null)
            {
                Destroy(plantedPlantInstance);
                plantedPlantInstance = null;
            }
        }
        else
        {
            Debug.Log("The flower's not bloomed yet!");
        }
        return currentTulip;
    }

    private void OnDestroy()
    {
        GMFarm.FarmlandPlantDecideEvent -= CheckIfMe;
    }
}