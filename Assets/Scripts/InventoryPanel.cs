using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField]
    private UIInventorySlot _slotPrefab;
    [SerializeField]
    private RectTransform _inventoryPanel;
    List<UIInventorySlot> UIInventorySlotList = new List<UIInventorySlot>();
    private void Start()
    {
        InitializeInventoryUI();
        GameManager.InventoryUpdateEvent += UIInventoryUpdate;
    }
    public void InitializeInventoryUI()
    {
        for (int i = 0; i < MyConst.INVENTORY_SIZE; i++)
        {
            UIInventorySlot inventorySlot =
                Instantiate(_slotPrefab, Vector3.zero, Quaternion.identity);
            inventorySlot.transform.SetParent(_inventoryPanel);
            UIInventorySlotList.Add(inventorySlot);
        }
    }

    private void UIInventoryUpdate(List<InventorySlot> inventorySlots)
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            UIInventorySlotList[i].SetItem(inventorySlots[i]);
        } 
    }
}
