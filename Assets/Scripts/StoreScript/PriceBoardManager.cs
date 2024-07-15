using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceBoardManager : MonoBehaviour
{
    [SerializeField] private PriceSlot _priceSlotPrefab;
    
    [SerializeField] private GameObject _priceBoard;
    [SerializeField] private List<ItemData> _storeItems;
    private List<PriceSlot> _priceSlotList = new List<PriceSlot>();
    private void Start()
    {
        InitializePriceBoard();
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
