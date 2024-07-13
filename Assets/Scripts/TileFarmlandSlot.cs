using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TileFarmlandSlot: MonoBehaviour
{
    public Sprite emptyLandSprite;
    public Sprite seededLandSprite;
    private bool isPlanted = false;
    private PlantData plantData;
    private SpriteRenderer spriteRenderer;
    private int daysLeft;
    private TileBase farmland;

    private void Start()
    {
        GMFarm.FarmlandPlantDecideEvent += CheckIfMe;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = emptyLandSprite;
        
    }
    private void CheckIfMe(TileFarmlandSlot tile, PlantData plant) 
    {
        if (tile != this)
            return;
    }
    void PlantSeed()
    {
        isPlanted = true;
        spriteRenderer.sprite = seededLandSprite;
    }

    void Harvest()
    {
        isPlanted = false;
        spriteRenderer.sprite = emptyLandSprite;

    }
}