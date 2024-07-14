using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.VersionControl;
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
    public DialogueReply StartDialogue(Dialogue dialogue)
    {
        OpenDialogueUI?.Invoke();
        _dialoguePanel.SetActive(true);
        print(dialogue.Sentence);
        _dialogueText.text = dialogue.Sentence;
        if (dialogue.HasReplyOption)
        {
            _replyPanel.SetActive(true);
            return DialogueReply.Option1;
        }
        else
        {
            _replyPanel.SetActive(false);
            _dialoguePanelAsButton.onClick.AddListener(EndDialogue);
            return DialogueReply.None;
        }
    }
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
    private static UIDialogue _instance;
    // 인스턴스에 접근하기 위한 프로퍼티
    public static UIDialogue Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(UIDialogue)) as UIDialogue;
                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}











Message Fjfundd Vicwioe






