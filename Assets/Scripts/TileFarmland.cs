using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor.Experimental.Rendering;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileFarmland : MonoBehaviour
{
    public Sprite emptyLandSprite;
    public Sprite seededLandSprite;
    private bool isPlanted = false;
    private PlantData plantData;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = emptyLandSprite;
    }

    private void OnMouseDowm()
    {
        if (!isPlanted)
        {
            //PlantSeed();
        }
        else
        {
            UnityEngine.Debug.Log("Tulips already planted here.");
        }
    }
    
}
