using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    internal static object gameObject;
    public string Name;
    public bool stackable;
    public Sprite icon;
}
