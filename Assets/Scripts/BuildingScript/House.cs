using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class House : MonoBehaviour
{
    public static event Action SleepUntilNextDay;
    [SerializeField] private Dialogue dialogue;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("collided with house");
        if (collision.collider.CompareTag("Player"))
        {
            DialogueReply reply = UIDialogue.Instance.StartDialogue(dialogue);
            if (reply == DialogueReply.None) SleepUntilNextDay?.Invoke();
        }
    }
}