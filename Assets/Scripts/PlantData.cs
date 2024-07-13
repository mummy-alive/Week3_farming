using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Data/Plant")]
public class PlantData : ScriptableObject //??
{   
    [SerializeField]
    internal static object PlantTile;
    private static string PlantTileName;
    private static string SeedRarity;
}

public class SeedData : ScriptableObject
{
    [SerializeField]
    internal static object SeedTile;
    private static string SeedTileName;
    private static string SeedRarity;
}

public class RarityData
{
    Dictionary<string, int> _DaysToGrow = new Dictionary<string, int>();
    public RarityData()
    {
        _DaysToGrow.Add("N", 3);
        _DaysToGrow.Add("R", 3);
        _DaysToGrow.Add("SR", 4);
        _DaysToGrow.Add("SSR", 5);
    }
    public int getDaysToGrow(string rarity)
    {
        if (_DaysToGrow.TryGetValue(rarity, out int days))
            return days;
        return -1;
    }
}