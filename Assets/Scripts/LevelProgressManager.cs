using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgressManager : MonoBehaviour
{
    public static LevelProgressManager Instance { get; private set; }
    public Dictionary<string, bool> level1Secrets = new Dictionary<string, bool> 
        { {"HyperJump", false}, {"TshirtLover", false}, {"Walljumper", false} };
    public int level1Highscore = 0;
    public bool level1Unlocked = true;
    public bool level1MemoryUnlocked = true;
    public Dictionary<string, bool> level2Secrets = new Dictionary<string, bool>
        { {"tbd1", false}, {"tbd2", false}, {"tbd3", false} };
    public int level2Highscore = 0;
    public bool level2Unlocked = false;
    public bool level2MemoryUnlocked = true;
    public Dictionary<string, bool> level3Secrets = new Dictionary<string, bool>
        { {"tbd1", false}, {"tbd2", false}, {"tbd3", false} };
    public int level3Highscore = 0;
    public bool level3Unlocked = false;
    public bool level3MemoryUnlocked = false;
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

    public void EnterScore(int level, int height, int stars)
    {
        int score = height + 10 * stars;
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
    }
}