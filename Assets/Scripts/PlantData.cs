using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Data/Plant")]
public class PlantData : ScriptableObject // 
{   
    [SerializeField]
    internal static object PlantTile;
    private static string PlantTileName;
    private static string SeedRarity;   //이거 필요한가...?
    public List<Sprite> growProcess;
}