using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceBoardManager : MonoBehaviour
{
    [SerializeField] private PriceSlot _priceSlotPrefab;
    
    [SerializeField] private GameObject _priceBoard;
    [SerializeField] private List<ItemData> _storeItems;
    [SerializeField] private List<int> _itemPriceList;
     //first element: last update date, ith element: (i-1)번째 item의 가격
    private List<PriceSlot> _priceSlotList = new List<PriceSlot>();
    private void Start()
    {
        InitializePriceBoard();
        _itemPriceList = new List<int>(new int[13]);
        ReloadPriceBoard();
    }
    public void ReloadPriceBoard()
    {
        int today = GMClock.Instance.GetGameDay();
        for (int i=0; i<_storeItems.Count; i++)
        {
            ItemData itemData = _storeItems[i];
            if (_itemPriceList[0] != today)
            {
                if (itemData is SeedItemData)
                {
                    float priceMultiple = (itemData as SeedItemData).PriceMultiple;
                    _itemPriceList[i+1] = (int) (MyConst.PRICE_LIST[today-1]*priceMultiple*Random.Range(80, 120));
                } 
                else if  (itemData is TulipItemData)
                {
                        float priceMultiple = (itemData as TulipItemData).PriceMultiple;
                        _itemPriceList[i+1] = (int) (MyConst.PRICE_LIST[today-1]*priceMultiple*Random.Range(80, 120));
                }
            }
            
            _priceSlotList[i].UpdatePrice(_itemPriceList[i+1]);
            _priceSlotList[i].SetBtnState();
        }
        _itemPriceList[0] = today;
        
    }
    public void InitializePriceBoard()
    {
        foreach(ItemData itemData in _storeItems)
        {
            PriceSlot priceSlot =
                Instantiate(_priceSlotPrefab, Vector3.zero, Quaternion.identity);
            priceSlot.transform.SetParent(_priceBoard.transform);
            priceSlot.SetItem(itemData);
            _priceSlotList.Add(priceSlot);
        }
    }
}
