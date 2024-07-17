using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Data/TulipItemData", fileName = "TulipItemData")]
public class TulipItemData : ItemData   //flower 정보
{
    public float PriceMultiple;
    public GameObject FarmPrefab;
}
