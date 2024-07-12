using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    private void Update()
    {
        if (Input.GetKey(KeyCode.I))
        {
            panel.SetActive(!panel.activeInHierarchy);
        }
    }

}
