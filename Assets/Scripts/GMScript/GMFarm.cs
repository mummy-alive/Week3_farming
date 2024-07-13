using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class  GMFarm: MonoBehaviour
{
    public SeedItemData TestSeed;
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
        selectedSeed = TestSeed;
    }
    public void PlantOnFarmland(FarmlandSlot slot, Vector2 position) // use map_to_world to get absolute world position?
    {
        /*if (selectedSeed == null)
        {
            Debug.Log("No seed selected!");
            return;
        }*/

        /*TulipItemData tulip = GMRandomBloom.RandBloom(selectedSeed);
        slot.PlantSeed(selectedSeed, tulip, position);
        FarmlandPlantDecideEvent?.Invoke(slot, tulip, selectedSeed, position);*/
        TulipItemData tulip = GMRandomBloom.RandBloom(TestSeed);
        slot.PlantSeed(TestSeed, tulip, position);
        FarmlandPlantDecideEvent?.Invoke(slot, tulip, TestSeed, position);

        /*if (CheckIfSeed())
        {
            TulipItemData tulip = GMRandomBloom.RandBloom(selectedSeed);
            slot.PlantSeed(selectedSeed, tulip);
            FarmlandPlantDecideEvent?.Invoke(slot, tulip, selectedSeed);
        }
        else
        {
            Debug.Log("Not enough seeds left to plant!");
        }*/

    }

   /* public bool CheckIfSeed()
    {
        UserStatus userStatus = UserStatus.Instance;
        return (userStatus != null) && (userStatus.IsSelectedItemSeed());
    } 
   지금은 삭제. 나중에 인벤토리 구성 완료되면 추가 예정.*/

    public PlantData GetRandomPlant(SeedItemData seed)
    {
        PlantData plantData = null;
        return plantData;
    }

    public void Harvest(Vector2 slotPosition)
    {


    }
}