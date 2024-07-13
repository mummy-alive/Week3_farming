using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private Image _image;
    private ItemData _currItemData;

    public void SetItem(InventorySlot inventorySlot)
    {
        _currItemData = inventorySlot.itemData;
        _amountText.text = inventorySlot.amount.ToString();
        _image.sprite = inventorySlot.itemData.Icon;
    }
    public void Clean()
    {
        _image.sprite = null;
        _image.gameObject.SetActive(false); 
        _amountText.gameObject.SetActive(false);
    }

        public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}

public class InventorySlot
{
    public ItemData itemData;
    public int amount;
}