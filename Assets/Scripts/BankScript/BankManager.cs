using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class BankManager : MonoBehaviour
{
    [SerializeField] private PriceSlot _priceSlotPrefab;
    [SerializeField]
    public static event Action<bool[]> FarmlandSlotStatus;
    private bool[] _farmlandSlotStatus = new bool[20];
    [SerializeField]
    private Dialogue[] _askDialogue;
    [SerializeField]
    private Dialogue[] _replyDialogue;
    [SerializeField]
    private Dialogue _declineDialogue;
    private bool alreadyBorrow;

    private void Start()
    {
        GMClock.DayChangeEvent += LoanInit;
        //New slot also asks for subscription on farmlandstatus.
        
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            AskClerk();
        }
    }


    private void LoanInit()
    {
        this.alreadyBorrow = false;
    }
    private async Task AskClerk()
    {
        DialogueReply reply = await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(_askDialogue[0]);
        if (reply == DialogueReply.Option1)
            AskForNewSlot();
        //print("Yup");
        else
            AskForLoan();
        //print("Nope");
    }

    private async Task AskForNewSlot()
    {
        DialogueReply reply = await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(_askDialogue[1]);
        if (reply == DialogueReply.Option1)
        {
            bool ans = GMBank.Instance.BuyNewSlot();
            if (ans)
            {
                DialogueReply endReply = await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(_replyDialogue[0]);
            }
            else
            {
                DialogueReply endReply = await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(_replyDialogue[1]);
            }
        }
        else
        {
            DialogueReply declineDialog = await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(_declineDialogue);
        }
        return;
    }

    private async Task AskForLoan()
    {
        DialogueReply reply = await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(_askDialogue[2]);
        if (reply == DialogueReply.Option1)
        {
            BorrowMoney();
        }
        else
        {
            DialogueReply declineDialog = await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(_declineDialogue);
        }
        return;
    }

    private void BorrowMoney()
    {

    }

}