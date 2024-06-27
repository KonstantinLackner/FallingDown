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
    public AudioSource unlockAudioSource;
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
            highscoreText.color = new Color(0.83f, 0.31f, 0.6f, 1);
            highscoreText.text = " NEW HIGHSCORE\n " + progressTracker.highscore.Item1 + " meters" + " | " + progressTracker.highscore.Item2 + " stars";
        } 
        else 
        {
            highscoreText.text = " highscore\n" + progressTracker.highscore.Item1 + " meters" + " | " + progressTracker.highscore.Item2 + " stars";
        }

        currentStarText.text = progressTracker.starCount + "/60 stars collected";

        oldValue = MapValue(progressTracker.previousStarCount);
        transform.localScale = new Vector3(oldValue, transform.localScale.y, 1);
        List<GameObject> unlockedItems = GetUnlockedItems(progressTracker.previousStarCount);
        foreach (GameObject item in unlockedItems)
        {   
            item.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            item.transform.GetChild(0).gameObject.SetActive(true);
        }

        newValue = MapValue(progressTracker.starCount);
    }

    void Update()
    {
        if (updateTick > 0.15f) 
        {
            float xValue = transform.localScale.x;
            float yValue = transform.localScale.y;
            if (xValue <= newValue)
            {
                countAudioSource.Play();
                float nextXValue = transform.localScale.x + 0.2f;
                TryUnlockingItem(xValue, nextXValue);
                transform.localScale = new Vector3(nextXValue, yValue, 1);
            }
            updateTick = 0;
        }
        else 
        {
            updateTick += Time.deltaTime;
        }
    }

    private void TryUnlockingItem(float position, float nextPosition)
    {

        GameObject itemToUnlock = null;
        if (position <= MapValue(10) && nextPosition >= MapValue(10))
        {
            itemToUnlock = unlockableItems[1];
        }        
        if (position <= MapValue(20) && nextPosition >= MapValue(20))
        {
            itemToUnlock = unlockableItems[2];
        }        
        if (position <= MapValue(30) && nextPosition >= MapValue(30))
        {
            itemToUnlock = unlockableItems[3];
        }        
        if (position <= MapValue(40) && nextPosition >= MapValue(40))
        {
            itemToUnlock = unlockableItems[4];
        }        
        if (position <= MapValue(50) && nextPosition >= MapValue(50))
        {
            itemToUnlock = unlockableItems[5];
        }        
        if (position <= MapValue(60) && nextPosition >= MapValue(60))
        {
            itemToUnlock = unlockableItems[6];
        }

        if (itemToUnlock != null)
        {
            unlockAudioSource.Play();
            itemToUnlock.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            itemToUnlock.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private float MapValue(float oldValue)
    {
        float oldMin = 0;
        float oldMax = 60f;
        float newMin = 0f;
        float newMax = 14.5f;
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