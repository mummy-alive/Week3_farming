using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GMGameTutorial : MonoBehaviour
{
    [SerializeField] Dialogue[] _startDialogueList;
    [SerializeField] GameObject _tutorialPanel;
    [SerializeField] Button _tutorialEndButton;
    void Start()
    {
        ShowDialogue();
        _tutorialEndButton.onClick.AddListener(CloseTutorial);
    }

    private void CloseTutorial()
    {
        _tutorialPanel.SetActive(false);
    }

    private async Task ShowDialogue()
    {
        foreach (var dialogue in _startDialogueList)
        {
            DialogueReply reply = await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(dialogue);
            print(reply);
        }
        _tutorialPanel.SetActive(true);
    }
}
