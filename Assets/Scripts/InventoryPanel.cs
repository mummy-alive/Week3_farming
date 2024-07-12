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
        //Show();
    }
    public void InitializeInventoryUI()
    {
        for (int i = 0; i < 24; i++)
        {
            UIInventorySlot inventorySlot =
                Instantiate(_slotPrefab, Vector3.zero, Quaternion.identity);
            inventorySlot.transform.SetParent(_inventoryPanel);
            UIInventorySlotList.Add(inventorySlot);
        }
    }
}
