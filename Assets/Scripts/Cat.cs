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

    public GameObject starSpawner;
    private Star starScript;
    public TMP_Text starsCollectedText;
    public TMP_Text scoreText;
    public TMP_Text bigText;
    public GameObject menuButton;
    private float starTimer = 3f;
    private float timeAlive = 0f;
    private int starsCollected = 0;
    private int score = 0;
    public LineDrawer lineDrawer;
    public PhysicsMaterial2D bounceMaterial;
    public PhysicsMaterial2D turboBounceMaterial;
    public LineRenderer lineRenderer;
    public int bounceHitThreshold;

    public float spriteChange1;
    public float spriteChange2;

    void Start()
    {
        transform.position = spawnPoint.position;
        fallingDown = true;
        starScript = starSpawner.GetComponent<Star>();
    }

    void Update()
    {
        if (fallingDown)
        {
            mySpriteRenderer.flipY = myRigidbody.velocity.y > 0;
            if (Mathf.Abs(myRigidbody.velocity.y) <= spriteChange1)
            {
                mySpriteRenderer.sprite = sprites[0];
            }
            else if (Mathf.Abs(myRigidbody.velocity.y) >= spriteChange1 && Mathf.Abs(myRigidbody.velocity.y) <= spriteChange2)
            {
                mySpriteRenderer.sprite = sprites[1];
            }
            else
            {
                mySpriteRenderer.sprite = sprites[2];
            }
            timeAlive += Time.deltaTime;
            UpdateScore();


            if (starTimer < 8)
            {
                starTimer += Time.deltaTime;
            }
            else
            {
                starTimer = 0;
                StartCoroutine(SpawnNewStar());
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            GameOver();
        }
        if (collision.gameObject.CompareTag("Bird"))
        {
            StartCoroutine(BounceUp());
        }
    }

    private IEnumerator BounceUp()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);
        gameObject.GetComponent<Rigidbody2D>().simulated = true;
    }

    private IEnumerator SpawnNewStar()
    {
        starScript.DestroyStar();
        yield return new WaitForSeconds(2f);
        starScript.SpawnNew();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Trigger entered!");
        if (col.gameObject.CompareTag("Star"))
        {
            Debug.Log("Star touched!");
            starTimer = 0;
            StartCoroutine(SpawnNewStar());
            starsCollected++;
        }
    }

    private void GameOver()
    {
        Debug.Log("Game over!");
        bigText.text = "GAME OVER";
        scoreText.fontSize = 16;
        fallingDown = false;
        menuButton.SetActive(true);
    }

    private void UpdateScore()
    {
        starsCollectedText.text = "Stars collected: " + starsCollected.ToString();
        score = starsCollected * 50;
        scoreText.text = "Score: " + score;
    }
}
