using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class ItemData : ScriptableObject
{
    internal static object gameObject;
    public string Name;
    public string InGameName;
    //public bool stackable;
    public Sprite Icon;
}


[CreateAssetMenu(menuName = "Data/SeedItem", fileName = "SeedData")]
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

[CreateAssetMenu(menuName = "Data/TulipItem", fileName = "TulipData")]
public class TulipItemData : ItemData   //flower 정보
{
    public float PriceMultiple;
    public Sprite FarmIcon;
}