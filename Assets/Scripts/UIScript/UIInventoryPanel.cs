using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryPanel : MonoBehaviour
{
    [SerializeField]
    private UIInventorySlot _slotPrefab;
    [SerializeField]
    private RectTransform _inventoryPanel;
    private List<UIInventorySlot> _UIInventorySlotList = new List<UIInventorySlot>();
    private void Start()
    {
        InitializeInventoryUI();
        GMInventory.InventoryUpdateEvent += UIInventoryUpdate;
    }
    public void InitializeInventoryUI()
    {
        for (int i = 0; i < MyConst.INVENTORY_SIZE; i++)
        {
            UIInventorySlot inventorySlot =
                Instantiate(_slotPrefab, Vector3.zero, Quaternion.identity);
            inventorySlot.transform.SetParent(_inventoryPanel);
            _UIInventorySlotList.Add(inventorySlot);
        }
    }

    private void UIInventoryUpdate(List<InventorySlot> inventorySlots)
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            _UIInventorySlotList[i].SetItem(inventorySlots[i]);
        } 
    }
}
