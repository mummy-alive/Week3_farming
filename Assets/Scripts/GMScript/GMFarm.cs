using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;

public class  GMFarm: MonoBehaviour
{
    public static GMFarm Instance { get; private set; } //public getter, private setter
    public static event Action<FarmlandSlot, TulipItemData, SeedItemData, Vector2> FarmlandPlantDecideEvent;
    
    [SerializeField]
    private List<FarmlandSlot> _farmlandSlotList = new List<FarmlandSlot>();
    [SerializeField]
    private Dialogue _askDialogue;
    [SerializeField]
    private Dialogue _alarmChooseSeed;
    private bool[] _farmlandSlotStatus = new bool[17];
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance !=this)
            Destroy(gameObject);
        GMBank.FarmlandSlotStatusChange += FarmEnabledCheck;

    }

    public void FarmEnabledCheck(int enabledSlot)
    {
        _farmlandSlotList[enabledSlot].EnableSlot();
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
    public async void PlantOnFarmland(FarmlandSlot slot, Vector2 position)
    {
        UserStatus userStatus = UserStatus.Instance;
        if (userStatus.selectedInventorySlot?.itemData is not SeedItemData)
        {
            GMDataHolder.Instance.UIDialogue.StartDialogueAsync(_alarmChooseSeed);
            return;
        }
        SeedItemData selectedSeed = SelectSeed();
        bool shouldPlant = await AskAndPlant();
        if (shouldPlant)
        {
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

    }

    private async Task<bool> AskAndPlant()
    {
        DialogueReply reply = await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(_askDialogue);
        return reply == DialogueReply.Option1;
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
           (userStatus.selectedInventorySlot.itemData.Name == "Watering can"))
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
        if (userStatus.selectedInventorySlot?.itemData is ToolItemData &&
           (userStatus.selectedInventorySlot.itemData.Name == "Trowel"))
        {
            TulipItemData tulip = slot.Harvest();
            if (tulip == null)
                Debug.Log("Tulip is not bloomed yet!");
            else
            {
                GMInventory.Instance.AddItemToInventory(tulip, 1);
            }
        }
    }
}