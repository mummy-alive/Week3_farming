using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIInventoryPanel : MonoBehaviour
{
    [SerializeField] private UIInventorySlot _slotPrefab;
    [SerializeField] private RectTransform _inventoryPanel;
    public List<UIInventorySlot> UIInventorySlotList = new List<UIInventorySlot>();
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
            UIInventorySlotList.Add(inventorySlot);
        }
    }
    private void UIInventoryUpdate(List<InventorySlot> inventorySlots)
    {
        foreach (UIInventorySlot uiSlot in UIInventorySlotList)
        {
            uiSlot.Clean();
        }
        for (int i = 0; i < GMInventory.Instance._itemSlotList.Count; i++)
        {
            UIInventorySlotList[i].SetItem(inventorySlots[i]);
        }
    }
}