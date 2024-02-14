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
    public TMP_Text highscoreText;
    public TMP_Text currentStarText;
    public List<GameObject> unlockableItems;
    private float updateTick = 0;
    private float oldValue;
    private float newValue;

    private void Start()
    {
        progressBackingTrackAudioSource.Play();

        progressTracker = GameObject.Find("ProgressTracker").GetComponent<ProgressTracker>();

        scoreText.text = progressTracker.latestScore.Item1 + " meters | " + progressTracker.latestScore.Item2 + " stars";
        if (progressTracker.newHighscore)
        {
            highscoreText.color = new Color(255, 0, 255, 255);
            highscoreText.text = " NEW HIGHSCORE\n " + progressTracker.highscore.Item1 + " meters" + " | " + progressTracker.highscore.Item2 + " stars";
        } 
        else 
        {
            highscoreText.text = " highscore\n" + progressTracker.highscore.Item1 + " meters" + " | " + progressTracker.highscore.Item2 + " stars";
        }

        currentStarText.text = progressTracker.starCount + "/60";

        List<GameObject> unlockedItems = GetUnlockedItems(progressTracker.starCount);
        foreach (GameObject item in unlockedItems)
        {   
            item.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            item.transform.GetChild(0).gameObject.SetActive(true);
        }

        oldValue = MapValue(progressTracker.previousStarCount, 0f, 60f, 0f, 14.5f);
        transform.localScale = new Vector3(oldValue, transform.localScale.y, 1);

        newValue = MapValue(progressTracker.starCount, 0f, 60f, 0f, 14.5f);
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

    private List<GameObject> GetUnlockedItems(int points)
    {
        return points switch
        {
            < 10 => new List<GameObject>(),
            >= 10 and < 20 => unlockableItems.GetRange(0, 2),
            >= 20 and < 30 => unlockableItems.GetRange(0, 3),
            >= 30 and < 40 => unlockableItems.GetRange(0, 4),
            >= 40 and < 50 => unlockableItems.GetRange(0, 5),
            >= 50 and < 60 => unlockableItems.GetRange(0, 6),
            _ => unlockableItems
        };
    }
}