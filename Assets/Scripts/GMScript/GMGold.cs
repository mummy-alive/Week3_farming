using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMGold : MonoBehaviour
{
    private int _currGold;
    private int _currDebt;

    private static GMGold _instance;

    public static GMGold Instance
    {
        get {
            if(!_instance)
            {
                _instance = FindObjectOfType(typeof(GMGold)) as GMGold;
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
