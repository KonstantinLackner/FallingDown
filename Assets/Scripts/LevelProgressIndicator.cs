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
    public GameObject story1Button;
    public TMP_Text level2SecretText;
    public TMP_Text level2HighscoreText;
    public GameObject level2PlayButton;
    public GameObject story2Button;
    public TMP_Text level3SecretText;
    public TMP_Text level3HighscoreText;
    public GameObject level3PlayButton;
    public GameObject storyEndButton;
    public TMP_Text bottomText;
    public GameObject lvl1Picture;
    public GameObject lvl2Picture;
    public GameObject lvl3Picture;

    void Start()
    {
        levelProgressManager = GameObject.Find("LevelProgressManager").GetComponent<LevelProgressManager>();

        level1SecretText.text = getUnlockedSecrets(1) + "/3 secrets";
        level2SecretText.text = getUnlockedSecrets(2) + "/3 secrets";
        level3SecretText.text = getUnlockedSecrets(3) + "/3 secrets";

        level1HighscoreText.text = "highscore: " + levelProgressManager.level1Highscore;
        if (levelProgressManager.level1NewHighscore) level1HighscoreText.color = new Color(0.83f, 0.31f, 0.6f, 1);
        level2HighscoreText.text = "highscore: " + levelProgressManager.level2Highscore;
        if (levelProgressManager.level2NewHighscore) level2HighscoreText.color = new Color(0.83f, 0.31f, 0.6f, 1);
        level3HighscoreText.text = "highscore: " + levelProgressManager.level3Highscore;
        if (levelProgressManager.level3NewHighscore) level3HighscoreText.color = new Color(0.83f, 0.31f, 0.6f, 1);

        level1PlayButton.GetComponent<Button>().interactable = levelProgressManager.level1Unlocked;
        level2PlayButton.GetComponent<Button>().interactable = levelProgressManager.level2Unlocked;
        level3PlayButton.GetComponent<Button>().interactable = levelProgressManager.level3Unlocked;

        story1Button.GetComponent<Button>().interactable = levelProgressManager.level2Unlocked;
        story2Button.GetComponent<Button>().interactable = levelProgressManager.level3Unlocked;
        storyEndButton.GetComponent<Button>().interactable = levelProgressManager.endUnlocked;

        if (levelProgressManager.level2Unlocked)
        {
            lvl2Picture.GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (levelProgressManager.level3Unlocked)
        {
            lvl3Picture.GetComponent<SpriteRenderer>().color = Color.white;
        }

        bottomText.text = "latest score: " + levelProgressManager.latestScore.Item1 + " meters + 10 * " + levelProgressManager.latestScore.Item2 + " stars = " + (levelProgressManager.latestScore.Item1 + levelProgressManager.latestScore.Item2 * 10);
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
