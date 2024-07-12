using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor.PackageManager;
using UnityEngine;



public class GameManager : MonoBehaviour
{
    public static event Action<List<InventorySlot>> InventoryUpdateEvent;

    [SerializeField]
    private int goldAmount = 500;
    [SerializeField]
    private List<InventorySlot> itemSlotList = new List<InventorySlot>();

    public int addItemToInventory(ItemData itemData, int amount)
    {
        int remainSlot = MyConst.INVENTORY_SIZE - itemSlotList.Count;
        foreach (InventorySlot inventorySlot in itemSlotList)
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
            itemSlotList.Add(inventorySlot);
            amount -= inventorySlot.amount;
            remainSlot -= 1;
        }
        InventoryUpdateEvent?.Invoke(itemSlotList);
        return amount;
    }

    public void removeItemFromInventory(int index)
    {
        InventoryUpdateEvent?.Invoke(itemSlotList);
        itemSlotList.RemoveAt(index);
    }

    public void decreaseItemFromInventory(ItemData itemData, int amount)
    {
        foreach (InventorySlot inventorySlot in itemSlotList)
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
        InventoryUpdateEvent?.Invoke(itemSlotList);
    }
}
