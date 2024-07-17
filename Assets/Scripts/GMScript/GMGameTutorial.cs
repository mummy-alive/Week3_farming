using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GMGameTutorial : MonoBehaviour
{
    [SerializeField] Dialogue[] _startDialogueList;
    void Start()
    {
        ShowDialogue();
    }

    private async Task ShowDialogue()
    {
        foreach (var dialogue in _startDialogueList)
        {
            await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(dialogue);
        }
    }
}
