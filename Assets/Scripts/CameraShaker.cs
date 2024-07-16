using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;
	
	// How long the object should shake for.
	public float wiggleDuration = 5f;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.5f;
	public float decreaseFactor = 1.0f;
	public float x_speed = 10f;
    public float y_speed = 3f;
	Vector3 originalPos;
	private float time;
    private float wiggleTimeLeft = 0f;


    void Start()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent<Transform>();
        }
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (wiggleTimeLeft > 0)
        {
            time += Time.deltaTime;
            float x = Mathf.Sin(time * x_speed) * shakeAmount; // Adjust the multiplier for speed
            float y = Mathf.Sin(time * y_speed) * shakeAmount; // Adjust the multiplier for speed

            camTransform.localPosition = originalPos + new Vector3(x, y, 0f);

            wiggleTimeLeft -= Time.deltaTime;
        }
        else
        {
            wiggleTimeLeft = 0f;
            camTransform.localPosition = originalPos;
            time = 0f; // Reset time to avoid accumulating the value
        }
    }

    public void WiggleCamera()
    {
        time = 0f;
        wiggleTimeLeft = wiggleDuration;
    }

    // Make CameraShaker a singleton object
    private static CameraShaker _instance;
    public static CameraShaker Instance
    {
        get {
            if(!_instance)
            {
                _instance = FindObjectOfType(typeof(CameraShaker)) as CameraShaker;
                if (_instance == null)
                    Debug.Log("no Singleton goldManager");
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
