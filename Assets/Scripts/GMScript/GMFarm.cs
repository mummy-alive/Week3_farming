using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class  GMFarm: MonoBehaviour
{
    //public SeedItemData TestSeed;
    public static GMFarm Instance { get; private set; } //public getter, private setter
    public static event Action<FarmlandSlot, TulipItemData, SeedItemData, Vector2> FarmlandPlantDecideEvent;
    
    [SerializeField]
    private List<FarmlandSlot> _farmlandSlotList = new List<FarmlandSlot>();
    private SeedItemData selectedSeed;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance !=this)
            Destroy(gameObject);
    }
    public void SelectSeed(SeedItemData seed)
    {
        UserStatus userStatus = UserStatus.Instance;
        if (userStatus = null)
        {
            Debug.Log("UserStatus is Null!");
            return;
        }
        if (userStatus.selectedInventorySlot?.itemData is SeedItemData)
        {
            selectedSeed = userStatus.selectedInventorySlot.itemData as SeedItemData;
            if (selectedSeed == null)
            {
                Debug.Log("Selected Seed has null data!");
                return;
            }

        } else
        {
            Debug.Log("Please select seed.");
            return;
        }  
    }
    public void PlantOnFarmland(FarmlandSlot slot, Vector2 position) // use map_to_world to get absolute world position?
    {
        UserStatus userStatus = UserStatus.Instance;
        if (userStatus == null)
        {
            Debug.Log("No seed selected!");
            return;
        }
        if (userStatus.selectedInventorySlot?.itemData is SeedItemData)
        {
            TulipItemData tulip = GMRandomBloom.RandBloom(selectedSeed);
            slot.PlantSeed(selectedSeed, tulip, position);
            FarmlandPlantDecideEvent?.Invoke(slot, tulip, selectedSeed, position);
        }
        else
        {
            Debug.Log("Not enough seeds left to plant!");
        }

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