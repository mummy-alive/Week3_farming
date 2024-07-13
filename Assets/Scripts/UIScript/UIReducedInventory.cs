using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIReducedInventory : MonoBehaviour
{
    [SerializeField] private UIInventorySlot _slotPrefab;
    [SerializeField] private RectTransform _inventoryPanel;
    private List<UIInventorySlot> _UIInventorySlotList = new List<UIInventorySlot>();

    private void Start()
    {
        InitializeInventoryUI();
        GMInventory.InventoryUpdateEvent += UIInventoryUpdate;
    }
    private void UIInventoryUpdate(List<InventorySlot> inventorySlots)
    {
        for (int i = 0; i < Math.Min(MyConst.INVENTORY_COLUMNS, inventorySlots.Count); i++)
        {
            _UIInventorySlotList[i].SetItem(inventorySlots[i]);
        } 
    }
    public void InitializeInventoryUI()
    {
        for (int i = 0; i < MyConst.INVENTORY_COLUMNS; i++)
        {
            UIInventorySlot inventorySlot =
                Instantiate(_slotPrefab, Vector3.zero, Quaternion.identity);
            inventorySlot.transform.SetParent(_inventoryPanel);
            _UIInventorySlotList.Add(inventorySlot);
        }
    }
}
