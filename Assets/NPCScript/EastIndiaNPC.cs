using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

enum BoatType { BIGBOAT, SMALLBOAT, NONE }
public class EastIndiaNPC : MonoBehaviour
{
    private BoatType _boatType = BoatType.NONE;
    private bool _isSailing = false;
    private bool _boatArrived = false;
    private int _daysLeft = 90;
    [SerializeField] private SpriteRenderer _NPCImage;
    [SerializeField] private SpriteRenderer _boatImage;
    [SerializeField] private Collider2D _NPCCollider;
    [SerializeField] private BoatInvestment _smallBoatInvestment;
    [SerializeField] private BoatInvestment _bigBoatInvestment;
    [SerializeField] private Dialogue _noMoneyDialogue;
    [SerializeField] private Dialogue _waitBoatDialogue;
    [SerializeField] private Dialogue _boatReturnSuccess;
    [SerializeField] private Dialogue _boatReturnFail;
    [SerializeField] private ItemData _EXSeed;
    private void Start()
    {
        SetBoatForToday();
        GMClock.DayChangeEvent += SetBoatForToday;
    }
    private void SetBoatForToday()
    {
        if (_daysLeft == 0 && _isSailing) 
        {
            _daysLeft = 90;
            _isSailing = false;
            _boatArrived = true;
            _NPCImage.enabled = true;
            _NPCCollider.enabled = true;
            _boatImage.enabled = true;
            BoatResult();
        }
        if ( (!_isSailing) && (GMClock.Instance.GetGameDay() > 1))
        {
            int randNum = Random.Range(0, 10);
            if (randNum < 5 ) _boatType = BoatType.NONE;
            else if (randNum < 7) _boatType = BoatType.SMALLBOAT;
            else _boatType = BoatType.BIGBOAT;
        } else if ( _isSailing) 
        {
            _daysLeft -= 1;
            _NPCImage.enabled = true;
            _NPCCollider.enabled = true;
            _boatImage.enabled = false;
        }
        switch(_boatType)
        {
        case BoatType.BIGBOAT:
            _NPCImage.enabled = true;
            _NPCCollider.enabled = true;
            _boatImage.enabled = true;
            _boatImage.sprite = _bigBoatInvestment.BoatSprite;
            break;
        case BoatType.SMALLBOAT:
            _NPCImage.enabled = true;
            _NPCCollider.enabled = true;
            _boatImage.enabled = true;
            _boatImage.sprite = _smallBoatInvestment.BoatSprite;
            break;
        case BoatType.NONE:
            _NPCImage.enabled = false;
            _NPCCollider.enabled = false;
            _boatImage.enabled = false;
            break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isSailing) WaitForBoat();
        else if (_boatArrived) BoatResult();
        else
        {
            switch(_boatType)
            {
            case BoatType.BIGBOAT:
                AskBigBoatInvestment();
                break;
            case BoatType.SMALLBOAT:
                AskSmallBoatInvestment();
                break;
            }
        }
        
    }
    private async Task AskBigBoatInvestment()
    {
        int today = GMClock.Instance.GetGameDay();
        int price = (int) (_bigBoatInvestment.Price * MyConst.PRICE_LIST[today-1] * 100);
        if (GMGold.Instance.CurrGoldAmount < price)
        { await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(_noMoneyDialogue);}
        else
        {
            DialogueReply reply = await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(_bigBoatInvestment.AskInvestmentDialogue);
            if (reply == DialogueReply.Option1) 
            {
                _isSailing = true;
                _daysLeft = MyConst.BOAT_RETURN_DAYS;
                GMGold.Instance.CheckAndUseGold(price);
            }
        }
        
    }

    private async Task AskSmallBoatInvestment()
    {
        int today = GMClock.Instance.GetGameDay();
        int price = (int) (_smallBoatInvestment.Price * MyConst.PRICE_LIST[today-1] * 100);
        if (GMGold.Instance.CurrGoldAmount < price)
        { await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(_noMoneyDialogue);}
        else
        {
            DialogueReply reply = await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(_smallBoatInvestment.AskInvestmentDialogue);
            if (reply == DialogueReply.Option1) 
            {
                _isSailing = true;
                _daysLeft = MyConst.BOAT_RETURN_DAYS;
                GMGold.Instance.CheckAndUseGold(price);
            }
        }   
    }

    private async Task WaitForBoat()
    {
        await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(_waitBoatDialogue);
    }

    private async Task BoatResult()
    {
        BoatInvestment boatInvestment;
        if (_boatType == BoatType.BIGBOAT) { boatInvestment = _bigBoatInvestment; }
        else { boatInvestment = _smallBoatInvestment; }
        int probNum = Random.Range(0,100);
        int goldAmount = Random.Range(0, boatInvestment.MaxGold);
        int seedAmount = Random.Range(0, boatInvestment.MaxSeed);
        if ( probNum < boatInvestment.SuccessProb ) 
        {
            await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(_boatReturnSuccess);
            GMGold.Instance.EarnGold(goldAmount);
            GMInventory.Instance.AddItemToInventory(_EXSeed, seedAmount);
        }
        else
        { await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(_boatReturnFail); }
        _boatArrived = false;
    }
}
