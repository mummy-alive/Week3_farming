using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemAlert : MonoBehaviour
{
    [SerializeField] private GameObject _background;
    [SerializeField] private TextMeshProUGUI _itemNameText;
    [SerializeField] private Image _itemIcon;
    // Start is called before the first frame update
    public async Task ShowItemAlert(ItemData itemData)
    {
        if(itemData == null) return;
        _background.SetActive(true);
        _itemNameText.text = itemData.InGameName;
        _itemIcon.sprite = itemData.Icon;
        await Task.Delay(2000);
        _background.SetActive(false);
    }

    // Make UIItemAlert a singleton object
    private static UIItemAlert _instance;
    public static UIItemAlert Instance
    {
        get {
            if(!_instance)
            {
                _instance = FindObjectOfType(typeof(UIItemAlert)) as UIItemAlert;
                if (_instance == null)
                    Debug.Log("no Singleton goldManager");
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
