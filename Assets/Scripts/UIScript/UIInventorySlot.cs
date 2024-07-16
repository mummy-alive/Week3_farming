using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour, IPointerClickHandler
{
    public static event Action<UIInventorySlot> InventorySlotSelect;
    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private Image _image;
    [SerializeField] private Image _highlight;
    public InventorySlot currInventorySlot{get; private set;}

    private void Start()
    {
        _image.sprite = null;
        _image.gameObject.SetActive(false); 
        _amountText.gameObject.SetActive(false);
        UIInventorySlot.InventorySlotSelect += SlotIsSelected;
    }
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        _highlight.enabled = true;
        InventorySlotSelect?.Invoke(this);
    }

    private void SlotIsSelected(UIInventorySlot other)
    {
        if(other != this) _highlight.enabled = false;
    }

    public void SetItem(InventorySlot inventorySlot)
    {
        _image.gameObject.SetActive(true);
        _amountText.gameObject.SetActive(true);
        currInventorySlot = inventorySlot;
        _amountText.text = inventorySlot.amount.ToString();
        _image.sprite = inventorySlot.itemData.Icon;
    }

    public void Clean()
    {
        print("InventorySlot cleared");
        _image.sprite = null;
        _image.gameObject.SetActive(false); 
        _amountText.gameObject.SetActive(false);
    }

    
}

public class InventorySlot
{
    public ItemData itemData;
    public int amount;
}