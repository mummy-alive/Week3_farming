using System;
using System.Collections.Generic;
using UnityEngine;

public class GMInventory : MonoBehaviour {

    public static event Action<List<InventorySlot>> InventoryUpdateEvent;
    [SerializeField] public List<InventorySlot> _itemSlotList = new List<InventorySlot>();

    [SerializeField] private List<ItemData> _DefaultItemList;



    private void Start()
    {
        foreach (ItemData item in _DefaultItemList)
            GMInventory.Instance.AddItemToInventory(item, 1);
        InventoryUpdateEvent?.Invoke(_itemSlotList);
    }
    public int AddItemToInventory(ItemData itemData, int amount)
    {
        int remainSlot = MyConst.INVENTORY_SIZE - _itemSlotList.Count;
        foreach (InventorySlot inventorySlot in _itemSlotList)
        {

            if((inventorySlot.itemData.name == itemData.name)) 
            {
                inventorySlot.amount += Math.Min(amount, (MyConst.PILE_SIZE - inventorySlot.amount));
                amount -= Math.Min(amount, (MyConst.PILE_SIZE - inventorySlot.amount));
            }
        }
        while((remainSlot > 0) && (amount > 0) ){
            InventorySlot inventorySlot = new InventorySlot
            {
                amount = amount % MyConst.PILE_SIZE,
                itemData = itemData
            };
            _itemSlotList.Add(inventorySlot);
            amount -= inventorySlot.amount;
            remainSlot -= 1;
        }
        InventoryUpdateEvent?.Invoke(_itemSlotList);
        UIItemAlert.Instance.ShowItemAlert(itemData);
        return amount;
    }

    public void RemoveItemFromInventory(int index)
    {
        _itemSlotList.RemoveAt(index);
        InventoryUpdateEvent?.Invoke(_itemSlotList);
    }

    public void DecreaseItemFromInventory(ItemData itemData, int amount)
    {
        foreach (InventorySlot inventorySlot in _itemSlotList)
        {
            if((inventorySlot.itemData.name == itemData.name)) 
            {
                int par = Math.Min(amount, inventorySlot.amount);
                inventorySlot.amount -= par;
                amount -= par;
            }
        }
        if (amount > 0)
        {
            throw new ArgumentException("Parameter cannot be bigger than item amount", nameof(itemData));
        }
        _itemSlotList.RemoveAll(slot => slot.amount < 1);
        InventoryUpdateEvent?.Invoke(_itemSlotList);
    }

    public int CheckItemAmount(ItemData itemData)
    {
        int amount = 0;
        foreach (InventorySlot inventorySlot in _itemSlotList)
        {
            if(inventorySlot.itemData.name == itemData.name) 
            {
                amount += inventorySlot.amount;
            }
        }
        return amount;
    }

    public void sortInventory()
    {
        Dictionary<ItemData, int> mergedDict = new Dictionary<ItemData, int>();
        foreach (InventorySlot inventorySlot in _itemSlotList)
        {
            if (mergedDict.ContainsKey(inventorySlot.itemData))
            {
                mergedDict[inventorySlot.itemData] += inventorySlot.amount;
            }
            else
            {
                mergedDict[inventorySlot.itemData] = inventorySlot.amount;
            }
        }
        _itemSlotList = new List<InventorySlot>();
        foreach (var kvp in mergedDict)
        {
            ItemData key = kvp.Key;
            int value = kvp.Value;

            // Break down values greater than 64 into multiple tuples
            while (value > 64)
            {
                InventorySlot slot = new InventorySlot();
                slot.itemData = key;
                slot.amount = 64;
                _itemSlotList.Add(slot);
                value -= 64;
            }

            if (value > 0)
            {
                InventorySlot slot = new InventorySlot();
                slot.itemData = key;
                slot.amount = value;
                _itemSlotList.Add(slot);
            }
        }
        InventoryUpdateEvent?.Invoke(_itemSlotList);
    }


    // Make GMInventory a singleton object
    private static GMInventory _instance;

    public static GMInventory Instance
    {
        get {
            if(!_instance)
            {
                _instance = FindObjectOfType(typeof(GMInventory)) as GMInventory;
                if (_instance == null)
                    Debug.Log("no Singleton inventory");
            }
            return _instance;
        }
    }
    private void Awake()
    {

        if (_instance == null) _instance = this;
        else if (_instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        
    }
}