using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PathToTown : MonoBehaviour
{
    [SerializeField] private Dialogue _askMoveToTown;
    private async Task OnTriggerEnter2DAsync(Collider2D other)
    {
        print("collided with house");
        if (other.CompareTag("Player"))
        {
            DialogueReply reply = await GMDataHolder.Instance.UIDialogue.StartDialogueAsync(_askMoveToTown);
            if (reply == DialogueReply.Option1) SceneManager.LoadScene("TownScene");
        }
    }
}
