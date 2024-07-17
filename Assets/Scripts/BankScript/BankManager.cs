using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class BankManager : MonoBehaviour
{
    public static event Action<int> SubmitLoanWish;
    public UIDialogue dialogueManager;
    public Dialogue numberDialogue;
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
    [SerializeField]
    private Dialogue _allSlotOpenedDialogue;
    [SerializeField]
    private Dialogue _errorDialogue;
    [SerializeField]
    private Dialogue _loanGaveDialogue;
    [SerializeField]
    private TMP_InputField loanTMPInputField;

    private bool alreadyBorrow;

    private void Start()
    {
        GMClock.DayChangeEvent += LoanInit;
        BankManager.SubmitLoanWish += AddLoan;
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
        else
            AskForLoan();
    }

    private async Task AskForNewSlot()
    {
        int recentSlot = GMBank.Instance.GetAvailableSlot();

        if(recentSlot < 4 || recentSlot >= 16)
        {
            GMDataHolder.Instance.UIDialogue.StartDialogueAsync(_allSlotOpenedDialogue);
            return;
        }
        Dialogue tellSlotPrice = new Dialogue
        {
            Sentence = $"어디보자... 자네가 이 땅을 담보로 잡은 대출은 {MyConst.FARM_SLOT_PRICE[recentSlot]}휠던일세.",
            HasReplyOption = true,
            ReplyOption1 = "구매한다",
            ReplyOption2 = "돌아간다"
        };

        DialogueReply reply = await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(tellSlotPrice); //가격 나온 부부ㄴ

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
            DialogueReply reply1 = await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(_replyDialogue[2]);
            loanTMPInputField.gameObject.SetActive(true);
            loanTMPInputField.text = "";
            loanTMPInputField.onEndEdit.AddListener(OnLoanInputSubmit);
        }
        else
        {
            DialogueReply declineDialog = await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(_declineDialogue);
        }
        return;
    }
    private void OnLoanInputSubmit(string input)
    {
        if (int.TryParse(input, out int loanWish))
        {
            SubmitLoanWish?.Invoke(loanWish);
        }
        else
        {
            SubmitLoanWish?.Invoke(0); // 잘못된 입력 처리
        }
        loanTMPInputField.gameObject.SetActive(false);
        //loanInputField.gameObject.SetActive(false); // 입력 후 InputField 비활성화
    }

    private async void AddLoan(int loanWish)
    {
        if (loanWish > 0)
        {
            GMGold.Instance.EarnGold(loanWish);
            GMGold.Instance.IncreaseDebt(loanWish);
            DialogueReply declineDialog = await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(_loanGaveDialogue);
        }
        else
        {
            DialogueReply errorDialog = await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(_errorDialogue);
        }
    }
}