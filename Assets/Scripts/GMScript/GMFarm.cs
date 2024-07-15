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

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance !=this)
            Destroy(gameObject);
    }
    public SeedItemData SelectSeed()
    {
        UserStatus userStatus = UserStatus.Instance;
        if (userStatus == null)
        {
            Debug.Log("UserStatus is Null!");
            return null;
        }

        if (userStatus.selectedInventorySlot?.itemData is SeedItemData)
        {
            SeedItemData seed = userStatus.selectedInventorySlot.itemData as SeedItemData;
            if (seed == null)
            {
                Debug.Log("Selected Seed has null data!");
                return null;
            }
            return seed;
        }
        else
        {
            Debug.Log("Please select seed.");
            return null;
        }  
    }
    public void PlantOnFarmland(FarmlandSlot slot, Vector2 position)
    {
        SeedItemData selectedSeed = SelectSeed();
        UserStatus userStatus = UserStatus.Instance;
        if (userStatus == null)
        {
            Debug.Log("No seed selected!");
            return;
        }
        if (selectedSeed != null) //Decide which tulip & plant seed on slot
        {
            TulipItemData tulip = GMRandomBloom.RandBloom(selectedSeed);
            slot.PlantSeed(selectedSeed, tulip, position);
            FarmlandPlantDecideEvent?.Invoke(slot, tulip, selectedSeed, position);
            GMInventory.Instance.DecreaseItemFromInventory(selectedSeed, 1);
        }
        else
        {
            Debug.Log("Not enough seeds left to plant or is not seed");
        }

    }

    public void WaterOnFarmland(FarmlandSlot slot)
    {
        UserStatus userStatus = UserStatus.Instance;
        if(userStatus == null)
        {
            Debug.Log("No farm selected!");
            return;
        }
        if (userStatus.selectedInventorySlot?.itemData is ToolItemData && 
           userStatus.selectedInventorySlot.itemData.Name == "Watering can")
        {   
            slot.WaterPlant();
        }
        else
        {
            Debug.Log("You need water can to water the plant.");
            return;
        }
    }


    public void HarvestOnFarmland(FarmlandSlot slot)
    {
        UserStatus userStatus = UserStatus.Instance;
        if (userStatus == null)
        {
            Debug.Log("Nothing selected");
            return; 
        }
        TulipItemData tulip = slot.Harvest();
        if (tulip == null)
            Debug.Log("Tulip is not bloomed yet!");
        else
            GMInventory.Instance.AddItemToInventory(tulip, 1);
    }
}