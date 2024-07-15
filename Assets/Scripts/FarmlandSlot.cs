using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class FarmlandSlot: MonoBehaviour
{    
    public GameObject plantedPlantPrefab ;
    private bool isPlanted = false;
    private TulipItemData currentTulip;
    private int daysLeft;
    private Vector2 midPoint;
    private bool playerIsOnSlot = false;

    Renderer rend;

    private void Start()
    {
        GMFarm.FarmlandPlantDecideEvent += CheckIfMe;
        midPoint = transform.position;
        rend = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (playerIsOnSlot && Input.GetKeyDown(KeyCode.O))
        {
            //Ask if player's really gonna plant em
            GMFarm.Instance.PlantOnFarmland(this, midPoint);
        }
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
        GameObject plant = Instantiate(plantedPlantPrefab, midPoint, Quaternion.identity);
        plant.transform.SetParent(gameObject.transform);
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