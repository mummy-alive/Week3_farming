using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMClock : MonoBehaviour
{
    [SerializeField]
    private bool _isClockActive = true;
    [SerializeField]
    private int _currGameDay = 1;
    [SerializeField]
    private int _currGameHour = 6;
    [SerializeField]
    private int _currGameMinute = 0;
    private int _totalGameMinutePassed = 0;
    private float _realSecPassedToday = 0f;


    public void StartClock(){
        _isClockActive = true;
    }

    public void StopClock(){
        _isClockActive = false;
    }

    private void Start()
    {
        UIController.OpenInventory += StopClock;
        UIController.CloseInventory += StartClock;
    }

    private void Update(){
        if (_isClockActive){
            _realSecPassedToday += Time.deltaTime;
            _currGameMinute = (int)(_realSecPassedToday * 60 / MyConst.REAL_SECOND_PER_GAME_HOUR);
            if (_currGameMinute >= 60){
            _realSecPassedToday = 0;
            _currGameHour += 1;
            }
            if (_currGameHour >= 24){
                _currGameHour = 6;
                _currGameMinute = 0;
                _currGameDay += 1;
            }
        }
        
    }

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
