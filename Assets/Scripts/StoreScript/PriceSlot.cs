using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

enum StoreItemType { UserCanBuy, UserCanSell };
public class PriceSlot : MonoBehaviour
{
    public static event Action StoreItemSelect;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _highlight;
    [SerializeField] private TextMeshProUGUI _itemNameText;
    [SerializeField] private TextMeshProUGUI _itemPriceText;
    [SerializeField] private Button _itemSlotBtn;
    private ItemData _itemData;
    public int TodayPrice;
    private StoreItemType _itemType;
    private void Start()
    {
        PriceSlot.StoreItemSelect += SetBtnState;
        _itemSlotBtn.onClick.AddListener(MakeTransaction);
    }
    private void MakeTransaction()
    {
        //_highlight.enabled = true;
        print("selected store item");
        
        if (_itemType == StoreItemType.UserCanBuy)
        {
            if (GMGold.Instance.CheckAndUseGold(TodayPrice))
                GMInventory.Instance.AddItemToInventory(_itemData, 1);
            else
            {

            }
                print("don't have enough gold");
        } else if (_itemType == StoreItemType.UserCanSell)
        {
            if (GMInventory.Instance.CheckItemAmount(_itemData) > 0)
            {
                GMInventory.Instance.DecreaseItemFromInventory(_itemData, 1);
                GMGold.Instance.EarnGold(TodayPrice);
            }
        }
        StoreItemSelect?.Invoke();
    }

    public void SetBtnState()
    {
        if(_itemType == StoreItemType.UserCanBuy)
        {
            if (GMGold.Instance.CurrGoldAmount < TodayPrice)
                _itemSlotBtn.interactable = false;
            else _itemSlotBtn.interactable = true;
        } else if (_itemType == StoreItemType.UserCanSell)
        {
            if (GMInventory.Instance.CheckItemAmount(_itemData) > 0)
                _itemSlotBtn.interactable = true;
            else _itemSlotBtn.interactable = false;
        }
        
    }

    public void SetItem(ItemData itemData)
    {
        _itemData = itemData;
        _icon.sprite = itemData.Icon;
        _itemNameText.text = itemData.InGameName;
        if (itemData is SeedItemData) _itemType = StoreItemType.UserCanBuy;
        else if (itemData is TulipItemData) _itemType = StoreItemType.UserCanSell;

    }
    public void UpdatePrice(int price)
    {
        TodayPrice = price;
        _itemPriceText.text = price.ToString();
    }
}
