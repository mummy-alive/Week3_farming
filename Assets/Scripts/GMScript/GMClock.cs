using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMClock : MonoBehaviour
{
    public static event Action<int, int, int> ClockChangeEvent;
    [SerializeField] private bool _isClockActive = true;
    [SerializeField] private int _currGameDay = 1;
    [SerializeField] private int _currGameHour = 6;
    [SerializeField] private int _currGameMinute = 0;
    private int _prevGameMinute = 0;
    private float _realSecPassed = 0f;


    private void StartClock(){
        _isClockActive = true;
    }

    private void StopClock(){
        _isClockActive = false;
    }

    private void Start()
    {
        UIController.OpenInventory += StopClock;
        UIController.CloseInventory += StartClock;
    }


    private void Update(){
        if (_isClockActive){
            _realSecPassed += Time.deltaTime;
            _currGameMinute = (int)(_realSecPassed * 60 / MyConst.REAL_SECOND_PER_GAME_HOUR);
            if (_currGameMinute >= 60){
            _realSecPassed = 0;
            _currGameHour += 1;
            }
            if (_currGameHour >= 24){
                _currGameHour = 6;
                _currGameMinute = 0;
                _currGameDay += 1;
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
        get {
            if(!_instance)
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