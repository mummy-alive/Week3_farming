using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Data/SeedItemData", fileName = "SeedItemData")]
public class SeedItemData : ItemData    //seed 성장정보
{
    public int DaysToGrow;
    public float PriceMultiple;
    public float NProb;
    public float RProb;
    public float SRProb;
    public float EXProb;
    public float WeirdProb;
}
