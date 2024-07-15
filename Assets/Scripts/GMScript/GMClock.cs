using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GMClock : MonoBehaviour
{
    public static event Action<int, int, int> ClockChangeEvent;
    public static event Action DayChangeEvent;
    [SerializeField] private bool _isClockActive = true;
    [SerializeField] private int _currGameDay = 1;
    [SerializeField] private int _currGameHour = 6;
    [SerializeField] private int _currGameMinute = 0;
    private int _prevGameMinute = 0;
    private float _realSecPassed = 0f;
    private void StartClock()
    {
        _isClockActive = true;
    }
    private void StopClock()
    {
        _isClockActive = false;
    }
    public int GetGameDay()
    {
        return _currGameDay;
    }
    private void StartNextDay()
    {
        _currGameHour = 6;
        _currGameMinute = 0;
        _currGameDay += 1;
        _realSecPassed = 0f;
        DayChangeEvent?.Invoke();
    }
    private void Start()
    {
        UIController.OpenInventory += StopClock;
        UIController.CloseInventory += StartClock;
        UIDialogue.OpenDialogueUI += StopClock;
        UIDialogue.CloseDialogueUI += StartClock;
        House.SleepUntilNextDay += StartNextDay;
    }
    private void Update()
    {
        if (_isClockActive)
        {
            _realSecPassed += Time.deltaTime;
            _currGameMinute = (int)(_realSecPassed * 60 / MyConst.REAL_SECOND_PER_GAME_HOUR);
            if (_currGameMinute >= 60)
            {
                _realSecPassed = 0;
                _currGameHour += 1;
            }
            if (_currGameHour >= 24)
            {
                StartNextDay();
            }
            if (_prevGameMinute != _currGameMinute)
                ClockChangeEvent?.Invoke(_currGameDay, _currGameHour, _currGameMinute);
        }
    }
    // Make GMClock a singleton object
    private static GMClock _instance;
    // 인스턴스에 접근하기 위한 프로퍼티
    public static GMClock Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GMClock)) as GMClock;
                if (_instance == null)
                    Debug.Log("no Singleton clock");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null) _instance = this;
        else if (_instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
}