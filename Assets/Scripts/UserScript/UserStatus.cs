using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UserStatus : MonoBehaviour
{

    [SerializeField] ItemData _weirdTulip;
    [SerializeField] Dialogue _askEatWeirdTulip;
    public InventorySlot selectedInventorySlot{get; private set;}

    private void Start()
    {
        UIInventorySlot.InventorySlotSelect +=SetSelectedInventorySlot;
    }

    private void SetSelectedInventorySlot(UIInventorySlot selectedSlot)
    {
        selectedInventorySlot = selectedSlot.currInventorySlot;
        if (selectedSlot.currInventorySlot.itemData == _weirdTulip)
        {
            AskEatWeird();
        }
    }
    private async Task AskEatWeird()
    {
        DialogueReply reply = await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(_askEatWeirdTulip);
        if (reply == DialogueReply.Option1) 
        {
            GMInventory.Instance.DecreaseItemFromInventory(_weirdTulip, 1);
            ScreenColorFilter.Instance.StartColorChange(5f);
        }
    }


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
