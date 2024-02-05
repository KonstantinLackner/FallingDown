using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class GameStateManager : MonoBehaviour
{
    private bool gameIsRunning = true;
    private float currentTimeScale = 1; // I added this as the gravity boots could just slow down the time scale
    public GameObject quipWindow;
    public TMP_Text quipWindowText;
    public Image quipWindowSprite;
    public Rigidbody2D cat;
    public TMP_Text starsCollectedText;
    public TMP_Text starsMissedText;
    public TMP_Text scoreText;
    public TMP_Text bigText;
    public GameObject menuButton;
    public int currentStars = 0;
    public int starsMissed = 0;
    private int score = 0;
    public AudioSource gameOverAudioSource;
    public AudioSource starCollectAudioSource;
    
    [System.Serializable]
    public class Item
    {
        public string ItemName;
        public Sprite ItemSprite;
        public string ItemText;
    }
    
    public List<Item> items = new List<Item>();

    void Start()
    {
        quipWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameIsRunning && Input.GetKeyDown(KeyCode.Alpha1))
        {
            resumeGame();
        }

        UpdateScore();
    }

    void pauseGame()
    {
        gameIsRunning = false;
        Time.timeScale = 0;
    }

    void resumeGame()
    {
        quipWindow.SetActive(false);
        gameIsRunning = true;
        Time.timeScale = currentTimeScale;
        cat.velocity = new Vector2(0, 7);
    }

    public void startQuip(string itemName)
    {
        quipWindow.SetActive(true);
        foreach (var item in items)
        {
            if (item.ItemName.Equals(itemName))
            {
                quipWindowText.text = item.ItemText;
                quipWindowSprite.sprite = item.ItemSprite;
                pauseGame();
                return;
            }
        }

        // Handle the case where the item is not found
        Debug.Log("Item not found: " + itemName);
    }
    
    public void GameOver()
    {
        bigText.text = "GAME OVER";
        gameOverAudioSource.Play();
        scoreText.fontSize = 16;
        menuButton.SetActive(true);
    }
    
    private void UpdateScore()
    {
        starsCollectedText.text = "Stars collected: " + currentStars.ToString();
        starsMissedText.text = "Stars missed: " + starsMissed.ToString();
        score = currentStars * 50 - starsMissed * 20;
        scoreText.text = "Score: " + score;
    }

    public void CollectStar()
    {
        currentStars++;
        starCollectAudioSource.Play();
    }
}
