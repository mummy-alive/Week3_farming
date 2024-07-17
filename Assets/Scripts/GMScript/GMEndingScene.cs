using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMEndingScene : MonoBehaviour
{
    [SerializeField] private GameObject _UIReducedInventoryPanel;
    private void Start()
    {
        GMClock.DayChangeEvent += StartEndingScene;
    }

    private void StartEndingScene()
    {
        if ( GMClock.Instance.GetGameDay() > 80)
        {
        }
    }
}
