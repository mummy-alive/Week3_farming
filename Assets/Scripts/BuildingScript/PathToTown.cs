using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PathToTown : MonoBehaviour
{
    [SerializeField] private Dialogue _askMoveToTown;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AskAndGotoTownAsync();
        }
    }
    private async Task AskAndGotoTownAsync()
    {
        DialogueReply reply = await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(_askMoveToTown);
        if (reply == DialogueReply.Option1) 
        {
            GMSceneSwitcher.Instance.SwitchScene("TownScene");
            charControl.Instance.MoveCharTo(new Vector2(-7,3));
            
        }
    }
}
