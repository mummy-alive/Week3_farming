using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogueReply : MonoBehaviour
{
    [SerializeField] private Button _option1PanelAsBtn;
    [SerializeField] private Button _option2PanelAsBtn;
    private DialogueReply reply = DialogueReply.None;

    private void Start()
    {
        _option1PanelAsBtn.onClick.AddListener(() => { reply = DialogueReply.Option1; });
        _option2PanelAsBtn.onClick.AddListener(() => { reply = DialogueReply.Option2; });
    }

    public DialogueReply StartAndWaitReply(string option1, string option2)
    {
        for (; ; )
        {
            if (reply != DialogueReply.None)
            {
                DialogueReply thisReply = reply;
                reply = DialogueReply.None;
                return thisReply;
            }
        }
    }
}



