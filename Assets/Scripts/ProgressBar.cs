using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    private ProgressTracker progressTracker;
    public AudioSource countAudioSource;
    public AudioSource progressBackingTrackAudioSource;
    public TMP_Text scoreText;
    public TMP_Text currentStarText;
    private float updateTick = 0;
    private float newValue;

    private void Start()
    {
        progressBackingTrackAudioSource.Play();
        progressTracker = GameObject.Find("ProgressTracker").GetComponent<ProgressTracker>();
        scoreText.text = "HIGHSCORE:\n " + progressTracker.maxHeight + " meters";
        currentStarText.text = progressTracker.starCount + "/60";

        newValue = MapValue(progressTracker.starCount, 0f, 60f, 0f, 17f);
    }

    void Update()
    {
        if (updateTick > 0.15f) 
        {
            float yValue = transform.localScale.y;
            if (transform.localScale.x <= newValue)
            {
                countAudioSource.Play();
                transform.localScale = new Vector3(transform.localScale.x + 0.15f, yValue, 1);
            }
            updateTick = 0;
        }
        else 
        {
            updateTick += Time.deltaTime;
        }
        

    }

    private float MapValue(float oldValue, float oldMin, float oldMax, float newMin, float newMax)
    {
        return ((oldValue - oldMin) / (oldMax - oldMin)) * (newMax - newMin) + newMin;
    }
}