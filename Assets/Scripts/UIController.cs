using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    static int INVENTORY_SIZE = 24;
    [SerializeField] GameObject panel;
    private void Update()
    {
        if (Input.GetKey(KeyCode.I))
        {
            panel.SetActive(!panel.activeInHierarchy);
        }
    }

}
