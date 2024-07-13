using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class UIGold : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _clockText;
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private TextMeshProUGUI _debtText;

    private void Start()
    {
        GMClock.ClockChangeEvent += ChangeClockUI;
        GMGold.GoldAmountChangeEvent += ChangeGoldUI;
;
        GMGold.DebtAmountChangeEvent += ChangeDebtUI;
    }
    private void ChangeClockUI(int day, int hour, int minute)
    {
        string clockStr = string.Format("Day {0}, {1:D2} : {2:D2}", day, hour, minute);
        _clockText.text = clockStr;
    }

    private void ChangeGoldUI(int goldAmount)
    {
        _goldText.text = string.Format("Gold: {0}", goldAmount);
    }

    private void ChangeDebtUI(int debtAmount)
    {
        _debtText.text = string.Format("Debt: {0}", debtAmount);
    }
}
