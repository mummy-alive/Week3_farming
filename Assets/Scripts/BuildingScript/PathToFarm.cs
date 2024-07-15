using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PathToFarm : MonoBehaviour
{
    [SerializeField] private Dialogue _askMoveToFarm;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            print("on the road to farm");
            AskAndGotoFarmAsync();
        }
    }
    private async Task AskAndGotoFarmAsync()
    {
        DialogueReply reply = await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(_askMoveToFarm);
        if (reply == DialogueReply.Option1)
        {
            GMSceneSwitcher.Instance.SwitchScene("FarmScene");
            charControl.Instance.MoveCharTo(new Vector2(7,3));
        } 
    }
    
}