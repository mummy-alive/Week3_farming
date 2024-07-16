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
	public float wiggleDuration = 10f;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.5f;
	public float decreaseFactor = 1.0f;
	public float x_speed = 10f;
    public float y_speed = 3f;
    [SerializeField] ScreenColorFilter _colorFilter;
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
        WiggleCamera();
    }

    void Update()
    {
        if (wiggleTimeLeft > 0)
        {
            time += Time.deltaTime;
            float x = Mathf.Sin(time * x_speed) * shakeAmount; // Adjust the multiplier for speed
            float y = Mathf.Sin(time * y_speed) * shakeAmount; // Adjust the multiplier for speed

            camTransform.localPosition = originalPos + new Vector3(x, y, 0f);

            wiggleTimeLeft -= Time.deltaTime * decreaseFactor;
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
        _colorFilter.StartFadeOut();
    }
}
