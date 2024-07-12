using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour
{
    static int PILE_SIZE = 64;

    private TextMeshProUGUI _amountText;
    private Image image;
}

public class InventorySlot
{
    public ItemData itemData;
    public int amount;
}