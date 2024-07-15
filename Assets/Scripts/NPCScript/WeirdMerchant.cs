using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WeirdMerchant : MonoBehaviour
{
    [SerializeField] TulipItemData _rainbowTulip;
    [SerializeField] TulipItemData _weirdTulip;
    [SerializeField] Dialogue _sellRainbowDialogue;
    [SerializeField] Dialogue _rejectDialogue;
    [SerializeField] Dialogue _getWeirdTulipDialogue;
    private void  OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (GMInventory.Instance.CheckItemAmount(_rainbowTulip) > 0)
                { AskSellRainbow(); }
            else if (Random.Range(0, 100) > 2 ) 
                { AskWeirdTulip(); }
            else 
                { Reject(); }
        }
    }

    private async Task AskSellRainbow()
    {
        DialogueReply reply = await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(_sellRainbowDialogue);
        if (reply == DialogueReply.Option1)
        {
            GMInventory.Instance.DecreaseItemFromInventory(_rainbowTulip, 1);
            int today = GMClock.Instance.GetGameDay();
            int price = (int) (_rainbowTulip.PriceMultiple * MyConst.PRICE_LIST[today-1] * 100);
            GMGold.Instance.EarnGold(price);
        } 
    }

    private async Task AskWeirdTulip()
    {
        DialogueReply reply = await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(_getWeirdTulipDialogue);
        if (reply == DialogueReply.Option1)
        {
            GMInventory.Instance.AddItemToInventory(_weirdTulip, 1);
        } 
    }
    private async Task Reject()
    {
        await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(_rejectDialogue);
    }
}
