using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEngine;

public class GMRandomBloom : MonoBehaviour
{

    public static TulipItemData RandBloom(SeedItemData seed)
    {
        float randNum = Random.Range(0f, 100f);

        if (randNum < seed.NProb)
        {
            int randIndex  = Random.Range(0, GMDataHolder.Instance.NTulipItemDatas.Count-1);
            return GMDataHolder.Instance.NTulipItemDatas[randIndex];
        }
        else if (randNum < seed.RProb + seed.NProb)
        {
            int randIndex  = Random.Range(0, GMDataHolder.Instance.RTulipItemDatas.Count-1);
            return GMDataHolder.Instance.RTulipItemDatas[randIndex];
        } 
        else if (randNum < seed.SRProb + seed.RProb + seed.NProb)
        {
            int randIndex  = Random.Range(0, GMDataHolder.Instance.SRTulipItemDatas.Count-1);
            return GMDataHolder.Instance.SRTulipItemDatas[randIndex];
        } 
        else if (randNum < seed.EXProb + seed.SRProb + seed.RProb + seed.NProb )
        {
            int randIndex  = Random.Range(0, GMDataHolder.Instance.EXTulipItemDatas.Count-1);
            return GMDataHolder.Instance.EXTulipItemDatas[randIndex];
        } 
        else 
        {
            int randIndex  = Random.Range(0, GMDataHolder.Instance.WeirdTulipItemDatas.Count-1);
            return GMDataHolder.Instance.WeirdTulipItemDatas[randIndex];
        }
    }
}