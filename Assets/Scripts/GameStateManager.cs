using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class GameStateManager : MonoBehaviour
{
    private bool gameIsRunning = true;
    private LevelProgressManager levelProgressManager;
    public float currentTimeScale = 1; // I added this as the gravity boots could just slow down the time scale
    public GameObject quipWindow;
    public TMP_Text quipWindowText;
    public Image quipWindowSprite;
    public Rigidbody2D cat;
    public TMP_Text bigText;
    public TMP_Text fullScore;
    public TMP_Text detailedScore;
    private int lastHeight;
    private int height;
    public GameObject camera;
    public int currentStars = 0;
    public AudioSource gameOverAudioSource;
    public AudioSource starCollectAudioSource;
    public AudioSource starCountAudioSource;
    public AudioSource backingTrackAudioSource;
    public SpriteRenderer UIStar;
    public SpriteRenderer SkyBox2;
    public SpriteRenderer SkyBox3;
    public TMP_Text quipWindowTitle;
    private int level = 0;

    void Start()
    {
        levelProgressManager = GameObject.Find("LevelProgressManager").GetComponent<LevelProgressManager>();
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        level = int.Parse(sceneName.Substring(sceneName.Length-1));
        backingTrackAudioSource.Play();
        pauseGame();
    }

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
        quipWindowTitle.text = item.ItemName;
        quipWindow.SetActive(true);
        quipWindowText.text = item.ItemText;
        quipWindowSprite.sprite = item.ItemSprite;
        pauseGame();
    }

    public void GameOver()
    {
        levelProgressManager.EnterScore(level, height, currentStars);
        bigText.text = "GAME OVER";
        StartCoroutine(loadNextScene());
    }

    private IEnumerator loadNextScene()
    {
        backingTrackAudioSource.Stop();
        gameOverAudioSource.Play();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("LevelSelectScene");
    }

    private void UpdateScore()
    {
        height = (int) (camera.transform.position.y / 2);
        if (height > lastHeight)
        {
            starCountAudioSource.Play();
        }
        fullScore.text = "current score: " +  levelProgressManager.CalculateScore(height, currentStars);
        detailedScore.text = height + " meters | " + currentStars;
        lastHeight = height;

        if (height is > 100 and < 200)
        {
            float opacity = Mathf.Clamp01((height - 100f) / (100));
            SkyBox2.color = new Color(1, 1, 1, opacity);
        } else if (height > 200)
        {
            float opacity = Mathf.Clamp01((height - 200f) / (100f));
            SkyBox3.color = new Color(1, 1, 1, opacity);
        }
    }

    public void CollectStar()
    {
        currentStars++;
        starCollectAudioSource.Play();
        StartCoroutine(ScoreTextFlash(new Color(0.88f, 0.88f, 0.18f, 1)));
    }

    public void CollectRedStar()
    {
        currentStars += 2;
        starCollectAudioSource.Play();
        StartCoroutine(ScoreTextFlash(Color.red));
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
    
    private IEnumerator ScoreTextFlash(Color color)
    {
        fullScore.color = color;
        detailedScore.color = color;
        yield return new WaitForSeconds(0.35f);
        fullScore.color = Color.white;
        detailedScore.color = Color.white;
    }
}