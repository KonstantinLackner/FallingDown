using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class GameStateManager : MonoBehaviour
{
    private bool gameIsRunning = true;
    public float currentTimeScale = 1; // I added this as the gravity boots could just slow down the time scale
    public GameObject quipWindow;
    public TMP_Text quipWindowText;
    public Image quipWindowSprite;
    public Rigidbody2D cat;
    public TMP_Text bigText;
    public TMP_Text heightScore;
    private int lastHeight;
    public GameObject menuButton;
    public GameObject camera;
    public int currentStars = 0;
    public AudioSource gameOverAudioSource;
    public AudioSource starCollectAudioSource;
    public AudioSource starCountAudioSource;
    public SpriteRenderer UIStar;
    public bool isExpertMode = false;
    public SpriteRenderer SkyBox2;
    public SpriteRenderer SkyBox3;

    void Start()
    {
        quipWindow.SetActive(false);
        
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        isExpertMode = sceneName == "ExpertLevel";
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameIsRunning && Input.GetKeyDown(KeyCode.Space))
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
        cat.velocity += new Vector2(0, 3);
    }

    public void startQuip(Item item)
    {
        quipWindow.SetActive(true);
        quipWindowText.text = item.ItemText;
        quipWindowSprite.sprite = item.ItemSprite;
        pauseGame();
    }

    public void GameOver()
    {
        bigText.text = "GAME OVER";
        gameOverAudioSource.Play();
        menuButton.SetActive(true);
    }

    private void UpdateScore()
    {
        int height = (int) (camera.transform.position.y / 2);
        if (height > lastHeight)
        {
            starCountAudioSource.Play();
        }
        heightScore.text = height + " meters \u00d7 " + currentStars;
        lastHeight = height;

        if (height is > 300 and < 500)
        {
            float opacity = Mathf.Clamp01((height - 300f) / (200f));
            SkyBox2.color = new Color(1, 1, 1, opacity);
        } else if (height > 500)
        {
            float opacity = Mathf.Clamp01((height - 500f) / (300f));
            SkyBox3.color = new Color(1, 1, 1, opacity);
        }
    }

    public void CollectStar()
    {
        currentStars++;
        starCollectAudioSource.Play();
    }

    public void ApplyAllItemChanges()
    {
        Time.timeScale = currentTimeScale;
    }
    
    
    public void removeStar()
    {
        if (currentStars > 0)
        {
            currentStars--;
            StartCoroutine(removeStarRedFlash());
        }
    }

    private IEnumerator removeStarRedFlash()
    {
        UIStar.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        UIStar.color = Color.white;
    }
}