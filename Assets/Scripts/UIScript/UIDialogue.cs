﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public enum DialogueReply { None, Option1, Option2 }
public class UIDialogue : MonoBehaviour, IPointerClickHandler
{
    public static event Action OpenDialogueUI;
    public static event Action CloseDialogueUI;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private Button _dialoguePanelAsButton;
    [SerializeField] private GameObject _replyPanel;
    [SerializeField] private UIDialogueReply _dialogueReply;
    [SerializeField] private GameObject _numberInputPanel;

    public async Task<DialogueReply> StartDialogueAsync(Dialogue dialogue)
    {
        OpenDialogueUI?.Invoke();
        _dialoguePanel.SetActive(true);
        print(dialogue.Sentence);
        _dialogueText.text = dialogue.Sentence;
        if (dialogue.HasReplyOption)
        {
            _replyPanel.SetActive(true);
            print("replay panel is shown");
            DialogueReply reply = await _dialogueReply.StartAndWaitReply(dialogue);
            EndDialogue();
            return reply;
        }
        else
        {
            _replyPanel.SetActive(false);
            _dialoguePanelAsButton.onClick.AddListener(EndDialogue);
            return DialogueReply.None;
        }
    }
/*    public async Task<int> StartNumberInputDialogueAsync(Dialogue dialogue)
    {
        OpenDialogueUI?.Invoke();
        _dialoguePanel.SetActive(true);
        _dialogueText.text = dialogue.Sentence;

        _replyPanel.SetActive(false);
        _numberInputPanel.SetActive(true);
        int numberInput = await _dialogueNumberInput.StartAndWaitForInput(dialogue);
        EndDialogue();
        return numberInput;
    }
*/
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        print("clicked dialog panel");
        EndDialogue();
    }
    private void EndDialogue()
    {
        _dialoguePanel.SetActive(false);
        CloseDialogueUI?.Invoke();
    }
}