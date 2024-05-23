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
    public TMP_Text heightScore;
    private int lastHeight;
    private int height;
    public GameObject menuButton;
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
    public GameObject secret1;
    public GameObject secret2;
    public GameObject secret3;

    void Start()
    {
        levelProgressManager = GameObject.Find("LevelProgressManager").GetComponent<LevelProgressManager>();
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        level = int.Parse(sceneName.Substring(sceneName.Length-1));
        UpdateUnlockedSecrets();
        backingTrackAudioSource.Play();
        pauseGame();
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

    public bool UnlockHyperjump()
    {
        if (level == 1 && !levelProgressManager.level1Secrets[secret1.name])
        {
            secret1.GetComponent<SpriteRenderer>().color = Color.white;
            levelProgressManager.level1Secrets["Hyperjump"] = true;
            return true;
        }
        return false;
    }

    public bool UnlockShirtlover()
    {
        if (level == 1 && !levelProgressManager.level1Secrets[secret2.name])
        {
            secret2.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(0.35f, 0.75f, 0.5f, 1);
            levelProgressManager.level1Secrets["ShirtLover"] = true;
            return true;
        }
        return false;
    }

    public bool UnlockWalljumper()
    {
        if (level == 1 && !levelProgressManager.level1Secrets[secret3.name])
        {
            secret3.GetComponent<SpriteRenderer>().color = Color.white;
            levelProgressManager.level1Secrets["Walljumper"] = true;
            return true;
        }
        return false;
    }

    public bool UnlockHorizontalJump()
    {
        return false;
    }

    public bool UnlockClawhater()
    {
        return false;
    }

    public bool UnlockDrillLover()
    {
        return false;
    }

    private void UpdateUnlockedSecrets()
    {
        if (level == 1)
        {
            if (levelProgressManager.level1Secrets[secret1.name])
            {
                secret1.GetComponent<SpriteRenderer>().color = Color.white;
            }
            if (levelProgressManager.level1Secrets[secret2.name])
            {
                secret2.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(0.35f, 0.75f, 0.5f, 1);
            }
            if (levelProgressManager.level1Secrets[secret3.name])
            {
                secret3.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        if (level == 2)
        {
            if (levelProgressManager.level2Secrets[secret1.name])
            {
                secret1.GetComponent<SpriteRenderer>().color = Color.white;
            }
            if (levelProgressManager.level2Secrets[secret2.name])
            {
                secret1.GetComponent<SpriteRenderer>().color = Color.white;
            }
            if (levelProgressManager.level2Secrets[secret3.name])
            {
                secret3.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
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
        heightScore.text = height + " meters | " + currentStars;
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