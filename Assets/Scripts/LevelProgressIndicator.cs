using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressIndicator : MonoBehaviour
{
    private LevelProgressManager levelProgressManager;
    public TMP_Text level1SecretText;
    public TMP_Text level1HighscoreText;
    public GameObject level1PlayButton;
    public GameObject level1MemoryButton;
    public TMP_Text level2SecretText;
    public TMP_Text level2HighscoreText;
    public GameObject level2PlayButton;
    public GameObject level2MemoryButton;
    public TMP_Text level3SecretText;
    public TMP_Text level3HighscoreText;
    public GameObject level3PlayButton;
    public GameObject level3MemoryButton;

    void Start()
    {
        levelProgressManager = GameObject.Find("LevelProgressManager").GetComponent<LevelProgressManager>();

        level1SecretText.text = getUnlockedSecrets(1) + "/3 secrets";
        level2SecretText.text = getUnlockedSecrets(2) + "/3 secrets";
        level3SecretText.text = getUnlockedSecrets(3) + "/3 secrets";

        level1HighscoreText.text = "highscore: " + levelProgressManager.level1Highscore;
        level2HighscoreText.text = "highscore: " + levelProgressManager.level2Highscore;
        level3HighscoreText.text = "highscore: " + levelProgressManager.level3Highscore;

        level1PlayButton.GetComponent<Button>().interactable = levelProgressManager.level1Unlocked;
        level2PlayButton.GetComponent<Button>().interactable = levelProgressManager.level2Unlocked;
        level3PlayButton.GetComponent<Button>().interactable = levelProgressManager.level3Unlocked;

        level1MemoryButton.GetComponent<Button>().interactable = levelProgressManager.level1MemoryUnlocked;
        level2MemoryButton.GetComponent<Button>().interactable = levelProgressManager.level2MemoryUnlocked;
        level3MemoryButton.GetComponent<Button>().interactable = levelProgressManager.level3MemoryUnlocked;
    }

    private int getUnlockedSecrets(int level)
    {
        int unlockedSecrets = 0;
        switch (level)
        {
            case 1: 
            foreach(bool unlocked in levelProgressManager.level1Secrets.Values)
            {
                if (unlocked) unlockedSecrets++;
            }
            break;
            case 2: 
            foreach(bool unlocked in levelProgressManager.level2Secrets.Values)
            {
                if (unlocked) unlockedSecrets++;
            }
            break;
            case 3:
            foreach(bool unlocked in levelProgressManager.level3Secrets.Values)
            {
                if (unlocked) unlockedSecrets++;
            }
            break;
            default:
            break;

        }
        return unlockedSecrets;
    }
}
