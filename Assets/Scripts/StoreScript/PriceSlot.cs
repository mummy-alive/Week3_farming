using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PriceSlot : MonoBehaviour, IPointerClickHandler
{
    public static event Action<PriceSlot> StoreItemSelect;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _highlight;
    [SerializeField] private TextMeshProUGUI _itemNameText;
    [SerializeField] private TextMeshProUGUI _itemPriceText;
    private void Start()
    {
        PriceSlot.StoreItemSelect += SlotIsSelected;
    }
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        _highlight.enabled = true;
        StoreItemSelect?.Invoke(this);
    }

    private void SlotIsSelected(PriceSlot other)
    {
        if(other != this) _highlight.enabled = false;
    }

    public void SetItem(ItemData itemData)
    {
        _icon.sprite = itemData.Icon;
        _itemNameText.text = itemData.Name;
    }
    public void UpdatePrice(int price)
    {
        _itemPriceText.text = price.ToString();
    }
}
