using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static event Action OpenInventory;
    public static event Action CloseInventory;
    [SerializeField] GameObject _InventoryPanel;
    [SerializeField] GameObject _ReducedInventoryPanel;
    [SerializeField] ItemData _example_item;
    private bool _canOpenInventory =true;

    private void Start()
    {
        Time.timeScale = 1;
        DontDestroyOnLoad(gameObject);
        UIDialogue.OpenDialogueUI += ( () => {
            _canOpenInventory = false; 
            _InventoryPanel.SetActive(false);} );
        UIDialogue.CloseDialogueUI += ( () => {
            _canOpenInventory = true;
            _ReducedInventoryPanel.SetActive(true);} );

    }
    private void Update()
    {
        if (_canOpenInventory && Input.GetKeyDown(KeyCode.I))
        {
            if (_InventoryPanel.activeInHierarchy){
                _InventoryPanel.SetActive(false);
                _ReducedInventoryPanel.SetActive(true);
                CloseInventory?.Invoke();
            } else {
                _InventoryPanel.SetActive(true);
                _ReducedInventoryPanel.SetActive(false);
                OpenInventory?.Invoke();
            }
        }
        if (Input.GetKey(KeyCode.Q))
        {
            GMInventory.Instance.AddItemToInventory(_example_item, 1);
        }
    }

}

