using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    [SerializeField] GameObject _InventoryPanel;
    [SerializeField] ItemData _example_item;
    [SerializeField] GameManager gameManager;
    private void Update()
    {
        if (Input.GetKey(KeyCode.I))
        {
            _InventoryPanel.SetActive(!_InventoryPanel.activeInHierarchy);
        }
        if (Input.GetKey(KeyCode.A))
        {
            gameManager.addItemToInventory(_example_item, 1);
        }
    }

}
