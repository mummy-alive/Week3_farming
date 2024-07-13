using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Data/Plant")]
public class PlantData : ScriptableObject 
{   
    [SerializeField]
    internal static object PlantObject;
    private static string PlantObjectName;
    public List<Sprite> growProcess;
    private static TulipItemData tulipItemData;
}