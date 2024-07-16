using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject _ReducedInventoryPanel;
    [SerializeField] ItemData _example_item;

    private void Start()
    {
        Time.timeScale = 1;
        DontDestroyOnLoad(gameObject);

    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            GMInventory.Instance.AddItemToInventory(_example_item, 1);
        }
    }

}

