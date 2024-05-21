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
        { {"tbd1", false}, {"tbd2", true}, {"tbd3", false} };
    public int level2Highscore = 0;
    public bool level2Unlocked = false;
    public bool level2MemoryUnlocked = true;
    public Dictionary<string, bool> level3Secrets = new Dictionary<string, bool>
        { {"tbd1", false}, {"tbd2", true}, {"tbd3", false} };
    public int level3Highscore = 0;
    public bool level3Unlocked = false;
    public bool level3MemoryUnlocked = false;

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

    private void Start()
    {
    } 

}