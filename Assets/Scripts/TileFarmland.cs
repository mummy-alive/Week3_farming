using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileFarmland : MonoBehaviour
{
    [SerializeField]
    private TileFarmlandSlot _slotPrefab;   //prefab?
    [SerializeField]

    public Sprite emptyLandSprite;
    public Sprite seededSprite;
    //List<UIFarmlandSlot> UIFarmalndSlotList = new List<UIFarmlandSlot>();

  
    // Update is called once per frame
    private void Start()
    {
        InitializeFarmlandUI();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //object? ?? ?? ?? ??... ??? ???? ??? ????
            if (_slotPrefab != null)
            {
                PlantSeed();
            }
        }
    }
     
    public void InitializeFarmlandUI()
    {
        for(int i=0; i<36; i++)
        {
            //UIFarmlandSlot farmlandSlot = ...

            //UIFarmlandSlotList.Add(farmlandslot);
        }
    }

    void PlantSeed()
    {
           
    }
}
