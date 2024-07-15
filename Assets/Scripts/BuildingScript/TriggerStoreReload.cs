using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStoreReload : MonoBehaviour
{
    [SerializeField] PriceBoardManager _priceBoardManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _priceBoardManager.ReloadPriceBoard();
        }
    }
}
