using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Dialogue", fileName = "Dialogue")]
public class Dialogue: ScriptableObject
{
    public string name;
    public string[] Sentences;
}