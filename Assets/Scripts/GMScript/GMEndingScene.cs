using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GMEndingScene : MonoBehaviour
{
    [SerializeField] private GameObject _UIReducedInventoryPanel;
    [SerializeField] private GameObject _UIGoldClock;
    [SerializeField] private GameObject _goodEndingNPC;
    [SerializeField] private GameObject _badEndingNPC;
    [SerializeField] private Dialogue[] _goodEndingDialogueList;
    [SerializeField] private Dialogue[] _badEndingDialogueList;
    private void Start()
    {
        GMClock.DayChangeEvent += StartEndingScene;
    }

    private void StartEndingScene()
    {
        if ( GMClock.Instance.GetGameDay() > 80)
        {
            _UIReducedInventoryPanel.SetActive(false);
            _UIGoldClock.SetActive(false);
            if (GMGold.Instance.CurrDebtAmount > 0) { StartBadEndingAsync(); }
            else { StartGoodEndingAsync(); }
        }
    }

    private async Task StartGoodEndingAsync()
    {
        _goodEndingNPC.SetActive(true);
        foreach (var dialogue in _goodEndingDialogueList)
        {
            await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(dialogue);
        }
        
        ScreenColorFilter.Instance.StartFadeOut();
    }

    private async Task StartBadEndingAsync()
    {
        _badEndingNPC.SetActive(true);
        foreach (var dialogue in _badEndingDialogueList)
        {
            await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(dialogue);
        }
        CameraShaker.Instance.ShakeCamera();
        ScreenColorFilter.Instance.StartFadeOut();
    }
}
