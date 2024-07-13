using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserStatus : MonoBehaviour
{

    [SerializeField] private InventorySlot selectedInventorySlot{get;}

    // Make UserStatus a singleton object
    private static UserStatus _instance;

    public static UserStatus Instance
    {
        get {
            if(!_instance)
            {
                _instance = FindObjectOfType(typeof(UserStatus)) as UserStatus;
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
