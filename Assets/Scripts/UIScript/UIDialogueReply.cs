using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogueReply : MonoBehaviour
{
    [SerializeField] private Button _option1PanelAsBtn;
    [SerializeField] private Button _option2PanelAsBtn;
    [SerializeField] private TextMeshProUGUI _option1Text;
    [SerializeField] private TextMeshProUGUI _option2Text;
    private DialogueReply reply = DialogueReply.None;

    private void Start()
    {
        _option1PanelAsBtn.onClick.AddListener(() => { reply = DialogueReply.Option1; });
        _option2PanelAsBtn.onClick.AddListener(() => { reply = DialogueReply.Option2; });
    }

    public async Task<DialogueReply> StartAndWaitReply(Dialogue dialogue)
    {
        _option1Text.text = dialogue.ReplyOption1 ?? "";
        _option2Text.text = dialogue.ReplyOption2 ?? "";
        Debug.Log("set options");

        return await WaitForReplyAsync();
    }

    private Task<DialogueReply> WaitForReplyAsync()
    {
        var tcs = new TaskCompletionSource<DialogueReply>();
        StartCoroutine(CheckReplyCondition(tcs));
        return tcs.Task;
    }

    private IEnumerator CheckReplyCondition(TaskCompletionSource<DialogueReply> tcs)
    {
        while (true)
        {
            if (reply != DialogueReply.None)
            {
                DialogueReply thisReply = reply;
                reply = DialogueReply.None;
                tcs.SetResult(thisReply);
                yield break; // Exit the coroutine after handling the reply
            }
            yield return null; // Wait until the next frame before checking again
        }
    }
}



