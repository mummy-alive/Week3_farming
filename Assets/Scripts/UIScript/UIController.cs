using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static event Action OpenInventory;
    public static event Action CloseInventory;
    [SerializeField] GameObject _InventoryPanel;
    [SerializeField] ItemData _example_item;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (_InventoryPanel.activeInHierarchy){
                _InventoryPanel.SetActive(false);
                CloseInventory?.Invoke();
            } else {
                _InventoryPanel.SetActive(true);
                OpenInventory?.Invoke();
            }
        }
    }

}

