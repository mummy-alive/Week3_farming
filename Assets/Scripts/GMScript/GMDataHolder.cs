using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMDataHolder : MonoBehaviour
{
    public List<SeedItemData> SeedItemDatas;
    public List<TulipItemData> NTulipItemDatas;
    public List<TulipItemData> RTulipItemDatas;
    public List<TulipItemData> SRTulipItemDatas;
    public List<TulipItemData> EXTulipItemDatas;
    public List<TulipItemData> WeirdTulipItemDatas;

    // Make DataHolder a singleton object
    private static GMDataHolder _instance;

    public static GMDataHolder Instance
    {
        get {
            if(!_instance)
            {
                _instance = FindObjectOfType(typeof(GMDataHolder)) as GMDataHolder;
                if (_instance == null)
                    Debug.Log("no Singleton inventory");
            }
            return _instance;
        }
    }
        private void Awake()
    {
        if (_instance == null) _instance = this;
        else if (_instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
}
