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
    public Sprite icon;
}


[CreateAssetMenu(menuName = "Data/SeedItem", fileName = "SeedData")]
public class SeedItemData : ItemData
{
    public int DaysToGrow;
}

[CreateAssetMenu(menuName = "Data/TulipItem", fileName = "TulipData")]
public class TulipItemData : ItemData
{

}