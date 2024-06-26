using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgressManager : MonoBehaviour
{
    public static LevelProgressManager Instance { get; private set; }
    public Dictionary<string, bool> level1Secrets = new Dictionary<string, bool> 
        { {"Hyperjump", false}, {"ShirtLover", false}, {"Walljumper", false} };
    public int level1Highscore = 0;
    public bool level1Unlocked = true;
    public Dictionary<string, bool> level2Secrets = new Dictionary<string, bool>
        { {"HorizontalJump", false}, {"ClawHater", false}, {"DrillLover", false} };
    public int level2Highscore = 0;
    public bool level2Unlocked = false;
    public Dictionary<string, bool> level3Secrets = new Dictionary<string, bool>
        { {"tbd1", false}, {"tbd2", false}, {"tbd3", false} };
    public int level3Highscore = 0;
    public bool level3Unlocked = false;
    public bool endUnlocked = false;
    public int highscore = 0;
    public (int, int) latestScore = (0, 0);
    public bool level1NewHighscore = false;
    public bool level2NewHighscore = false;
    public bool level3NewHighscore = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make this object persistent
        }
        else
        {
            Destroy(gameObject); // Ensure there are no duplicate instances
        }
    }
    public void Unlock(int level, string secret)
    {
        switch (level)
        {
            case 1:
            if (level1Secrets.ContainsKey(secret)) level1Secrets[secret] = true;
            break;
            case 2:
            if (level2Secrets.ContainsKey(secret)) level2Secrets[secret] = true;
            break;
            case 3:
            if (level3Secrets.ContainsKey(secret)) level3Secrets[secret] = true;
            break;
        }
    }

    public int CalculateScore(int height, int stars)
    {
        return height + 10 * stars;
    }

    public void EnterScore(int level, int height, int stars)
    {
        int score = CalculateScore(height, stars);
        latestScore = (height, stars);

        switch (level)
        {
            case 1: 
            if (score > level1Highscore)
            {
                level1Highscore = score;
                level1NewHighscore = true;
            }
            else 
            {
                level1NewHighscore = false;
            }
            break;
            case 2:
            if (score > level2Highscore)
            {
                level2Highscore = score;
                level2NewHighscore = true;
            }
            else 
            {
                level2NewHighscore = false;
            }
            break;
            case 3:
            if (score > level3Highscore)
            {
                level3Highscore = score;
                level3NewHighscore = true;
            }
            else 
            {
                level3NewHighscore = false;
            }
            break;
            default: break;
        }

        CheckUnlocks();
    }

    private void CheckUnlocks()
    {
        level2Unlocked = level1Highscore >= 250;
        level3Unlocked = level2Highscore >= 250;
        endUnlocked = level1Highscore >= 250 && level2Highscore >= 250 && level3Highscore >= 250 && (level1Highscore+level2Highscore+level3Highscore) >= 1000;

        int level1UnlockedSecrets = 0;
        foreach(bool unlocked in level1Secrets.Values)
        {
            if (unlocked) level1UnlockedSecrets++;
        }
        
        int level2UnlockedSecrets = 0;
        foreach(bool unlocked in level2Secrets.Values)
        {
            if (unlocked) level2UnlockedSecrets++;
        }
        
        int level3UnlockedSecrets = 0;
        foreach(bool unlocked in level3Secrets.Values)
        {
            if (unlocked) level3UnlockedSecrets++;
        }
    }
}