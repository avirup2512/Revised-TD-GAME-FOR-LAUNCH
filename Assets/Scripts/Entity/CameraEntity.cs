using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraEntity : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;
    public RectTransform rect;

    Vector3 originalPos;

    void Awake()
    {
        if (rect == null)
        {
            rect = gameObject.GetComponent<RectTransform>();
        }
    }

    void Start()
    {
        originalPos = rect.transform.position;
    }

    public void setShakeDuration(float f)
    {
        shakeDuration = f;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            rect.transform.position = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            rect.transform.position = originalPos;
        }
    }
}