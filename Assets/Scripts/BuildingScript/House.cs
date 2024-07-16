using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class House : MonoBehaviour
{
    public static event Action SleepUntilNextDay;
    [SerializeField] private Dialogue _askSleepDialog;
    private async void OnCollisionEnter2D(Collision2D collision)
    {
        print("collided with house");
        if (collision.collider.CompareTag("Player"))
        {
            DialogueReply reply = await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(_askSleepDialog);
            if (reply == DialogueReply.Option1) 
            {
                SleepUntilNextDay?.Invoke();
                //ScreenColorFilter.Instance.StartFadeOut();
                ScreenColorFilter.Instance.StartFadeIn();
            }
        }
    }
}