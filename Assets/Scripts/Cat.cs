using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Cat : MonoBehaviour
{
    private bool fallingDown;
    public Rigidbody2D myRigidbody;
    public SpriteRenderer mySpriteRenderer;
    public List<Sprite> sprites;
    public Transform spawnPoint;

    public GameStateManager GSM;
    public GameObject starSpawner;
    private StarSpawner starScript;
    public TMP_Text starsCollectedText;
    public TMP_Text starsMissedText;
    public TMP_Text scoreText;
    public TMP_Text bigText;
    public GameObject menuButton;
    private float itemTimer = 8f;
    private float timeAlive = 0f;
    private int starsCollected = 0;
    private int starsMissed = 0;
    private int score = 0;
    private bool starsInitiated = false;
    public AudioSource starCollectAudioSource;
    public AudioSource gameOverAudioSource;
    public AudioSource bounceAudioSource;
    public AudioSource starDisappearAudioSource;

    public float spriteChange1;
    public float spriteChange2;

    void Start()
    {
        transform.position = spawnPoint.position;
        fallingDown = true;
        starScript = starSpawner.GetComponent<StarSpawner>();
    }

    void Update()
    {
        if (fallingDown)
        {
            float yFlip = 0.15 * myRigidbody.velocity.y > 0 ? 1 : -1;
            transform.localScale = new Vector3(1, yFlip, 1);
            if (Mathf.Abs(myRigidbody.velocity.y) <= spriteChange1)
            {
                mySpriteRenderer.sprite = sprites[0];
            }
            else if (Mathf.Abs(myRigidbody.velocity.y) >= spriteChange1 &&
                     Mathf.Abs(myRigidbody.velocity.y) <= spriteChange2)
            {
                mySpriteRenderer.sprite = sprites[1];
            }
            else
            {
                mySpriteRenderer.sprite = sprites[2];
            }

            timeAlive += Time.deltaTime;
            UpdateScore();


            if (itemTimer < 10)
            {
                itemTimer += Time.deltaTime;
            }
            else
            {
                if (!starsInitiated)
                {
                    starsInitiated = true;
                }
                else
                {
                    starDisappearAudioSource.Play();
                    starsMissed++;
                }

                itemTimer = 0;
                StartCoroutine(SpawnNewItem());
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            GameOver();
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            // StartCoroutine(BounceUp());
            Vector2 velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
            float xVelocity = velocity.x;
            float yVelocity = velocity.y;
            Vector2 newVelocity = new Vector2(0, 0);

            if (yVelocity > 0 && xVelocity != 0)
            {
                gameObject.GetComponent<Rigidbody2D>().simulated = false;
                newVelocity = new Vector2(xVelocity,
                    Mathf.Abs(yVelocity + Mathf.Max(Mathf.Min(xVelocity * 0.5f, 2), 0.5f)));
                gameObject.GetComponent<Rigidbody2D>().velocity = newVelocity;

                gameObject.GetComponent<Rigidbody2D>().simulated = true;
            }

            Debug.Log("velocity: " + velocity + "| velocity after: " + newVelocity);
        }

        if (collision.gameObject.CompareTag("Line"))
        {
            bounceAudioSource.Play();
        }
    }

    private IEnumerator SpawnNewItem()
    {
        starScript.DestroyStar();
        yield return new WaitForSeconds(2f);
        starScript.SpawnNew();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Star"))
        {
            starCollectAudioSource.Play();
            itemTimer = 0;
            StartCoroutine(SpawnNewItem());
            starsCollected++;
        }

        if (col.gameObject.CompareTag("gravityBoots"))
        {
            starCollectAudioSource.Play();
            itemTimer = 0;
            StartCoroutine(SpawnNewItem());
            GSM.startQuip("GravityBoots");
        }
    }

    private void GameOver()
    {
        Debug.Log("Game over!");
        bigText.text = "GAME OVER";
        gameOverAudioSource.Play();
        scoreText.fontSize = 16;
        fallingDown = false;
        menuButton.SetActive(true);
    }

    private void UpdateScore()
    {
        starsCollectedText.text = "Stars collected: " + starsCollected.ToString();
        starsMissedText.text = "Stars missed: " + starsMissed.ToString();
        score = starsCollected * 50 - starsMissed * 20;
        scoreText.text = "Score: " + score;
    }
}