using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class UIDialogue : MonoBehaviour
{
    private Queue<string> _sentences;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private GameObject _dialoguePanel;
    public void StartDialogue(Dialogue dialogue)
    {
        _sentences.Clear();
        foreach (string sentence in dialogue.Sentences)
        {
            _sentences.Enqueue(sentence);
        }
        _dialoguePanel.SetActive(true);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (_sentences.Count == 0) 
        {
            EndDialogue();
            return;
        }
        string nextSentence = _sentences.Dequeue();
        _dialogueText.text = nextSentence;
    }

    private void EndDialogue()
    {
        _dialoguePanel.SetActive(false);
    }
}
