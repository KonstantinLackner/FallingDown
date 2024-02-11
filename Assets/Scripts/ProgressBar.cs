using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public ProgressTracker progressTracker;
    public AudioSource countAudioSource;
    public TMP_Text scoreText;

    private void Start()
    {
        progressTracker = GameObject.Find("ProgressTracker").GetComponent<ProgressTracker>();
        scoreText.text = "HIGHSCORE:\n " + progressTracker.starCount + " meters";
        UpdateProgress();
    }

    void UpdateProgress()
    {
        float newValue = MapValue(progressTracker.starCount, 0f, 60f, 0f, 14.5f);
        StartCoroutine(GrowBar(newValue));
    }

    private float MapValue(float oldValue, float oldMin, float oldMax, float newMin, float newMax)
    {
        return ((oldValue - oldMin) / (oldMax - oldMin)) * (newMax - newMin) + newMin;
    }

    private IEnumerator GrowBar(float sizeToGrowTo)
    {
        float yValue = transform.localScale.y;
        while (transform.localScale.x <= sizeToGrowTo)
        {
            transform.localScale = new Vector3(transform.localScale.x + 0.1f, yValue, 1);
            yield return new WaitForSeconds(0.05f);
            countAudioSource.Play();
        }
    }
}