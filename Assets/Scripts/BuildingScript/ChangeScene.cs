using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private Dialogue _askDialogue;
    [SerializeField] private String _sceneName;
    [SerializeField] private Vector2 _initialPosition;
    [SerializeField] private float _charScale = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AskAndGoto();
        }
    }
    private async Task AskAndGoto()
    {
        DialogueReply reply = await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(_askDialogue);
        if (reply == DialogueReply.Option1) 
        {
            print("call fade out");
            ScreenColorFilter.Instance.StartFadeOut();
            
            GMSceneSwitcher.Instance.SwitchScene(_sceneName);
            charControl.Instance.MoveCharTo(_initialPosition);
            charControl.Instance.ScaleCharBy(_charScale);
            print("call fade in");
            ScreenColorFilter.Instance.StartFadeIn();
        }
    }
}
