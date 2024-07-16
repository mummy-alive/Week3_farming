using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GMGameTutorial : MonoBehaviour
{
    [SerializeField] Dialogue _startDialogue;
    void Start()
    {
        ShowDialogue();
    }

    private async Task ShowDialogue()
    {
        await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(_startDialogue);
    }
}
