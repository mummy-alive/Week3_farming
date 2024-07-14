using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor.PackageManager;
using UnityEngine;

public class GMInventory : MonoBehaviour {

    public static event Action<List<InventorySlot>> InventoryUpdateEvent;
    [SerializeField] private List<InventorySlot> _itemSlotList = new List<InventorySlot>();


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
        return amount;
    }

    public void RemoveItemFromInventory(int index)
    {
        InventoryUpdateEvent?.Invoke(_itemSlotList);
        _itemSlotList.RemoveAt(index);
    }

    public void DecreaseItemFromInventory(ItemData itemData, int amount)
    {
        foreach (InventorySlot inventorySlot in _itemSlotList)
        {
            if((inventorySlot.itemData.name == itemData.name)) 
            {
                inventorySlot.amount -= Math.Min(amount, inventorySlot.amount);
            }
        }
        if (amount > 0)
        {
            throw new ArgumentException("Parameter cannot be bigger than item amount", nameof(itemData));
        }
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