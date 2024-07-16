using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/*

public class UIDialogueNumberInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button submitButton;
    private int userInput = 0;
    private bool inputReceived = false;

    private void Start()
    {
        submitButton.onClick.AddListener(OnSubmit);
    }

    public async Task<int> StartAndWaitForInput(Dialogue dialogue)
    {
        inputField.text = "";
        inputReceived = false;

        return await WaitForInputAsync();
    }

    private void OnSubmit()
    {
        if (int.TryParse(inputField.text, out userInput))
        {
            inputReceived = true;
        }
        else
        {
            Debug.LogError("Invalid input. Please enter a valid number.");
        }
    }

    private Task<int> WaitForInputAsync()
    {
        var tcs = new TaskCompletionSource<int>();
        StartCoroutine(CheckInputCondition(tcs));
        return tcs.Task;
    }

    private IEnumerator CheckInputCondition(TaskCompletionSource<int> tcs)
    {
        while (!inputReceived)
        {
            yield return null;
        }
        tcs.SetResult(userInput);
    }
}
*/