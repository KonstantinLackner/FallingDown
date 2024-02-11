using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AuraPulse : MonoBehaviour
{
    public float amplitude = 0.5f; // The maximum change in scale
    public float frequency = 1f; // How fast it pulsates

    private Vector3 originalScale; // To remember the original scale

    void Start()
    {
        // Remember the original local scale of the GameObject
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Calculate the new scale factor based on the sine of time
        float scaleFactor = 1 + Mathf.Sin(Time.time * frequency) * amplitude;

        // Apply the new scale, multiplying the original scale by the scaleFactor
        transform.localScale = originalScale * scaleFactor;
    }
}
