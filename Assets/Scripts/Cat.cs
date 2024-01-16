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
    private bool starsInitiated = false;
    public TMP_Text timeAliveText;
    public TMP_Text starsCollectedText;
    public TMP_Text scoreText;
    public TMP_Text bigText;
    private float starTimer = 0f;
    private float timeAlive = 0f;
    private int starsCollected = 0;
    private int score = 0;
    public int turboLines = 0;
    private bool turboLineActive = false;
    public LineDrawer lineDrawer;
    public PhysicsMaterial2D bounceMaterial;
    public PhysicsMaterial2D turboBounceMaterial;
    public LineRenderer lineRenderer;

    public float spriteChange1;
    public float spriteChange2;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = spawnPoint.position;
        fallingDown = true;
        starScript = starSpawner.GetComponent<Star>();
    }

    // Update is called once per frame
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


            if (starTimer < 5)
            {
                starTimer += Time.deltaTime;
            }
            else
            {
                starTimer = 0;
                starScript.SpawnNew();
                starsInitiated = true;
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            ResetScore();
            Respawn();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && turboLines > 0 && !turboLineActive)
        {
            turboLines--;
            turboLineActive = true;
            lineDrawer.bounceMaterial = turboBounceMaterial;
            lineDrawer.GetComponent<EdgeCollider2D>().sharedMaterial = turboBounceMaterial;
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            GameOver();
        }
        if (collision.gameObject.CompareTag("Line") && turboLineActive)
        {
            lineDrawer.bounceMaterial = bounceMaterial;
            lineDrawer.GetComponent<EdgeCollider2D>().sharedMaterial = bounceMaterial;
            turboLineActive = false;
            lineRenderer.startColor = Color.white;
            lineRenderer.endColor = Color.white;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Trigger entered!");
        if (col.gameObject.CompareTag("Star"))
        {
            Debug.Log("Star touched!");
            starTimer = 0;
            starScript.SpawnNew();
            starsCollected++;
            turboLines += 2;
        }
    }

    private void Respawn()
    {
        fallingDown = true;
        transform.position = spawnPoint.position;
        myRigidbody.velocity = Vector2.zero;
        myRigidbody.angularVelocity = 0f;
    }

    private void GameOver()
    {
        Debug.Log("Game over!");
        bigText.text = "GAME OVER!";
        scoreText.fontSize = 16;
        fallingDown = false;
    }

    private void UpdateScore()
    {
        double timeAliveRounded = System.Math.Round(timeAlive, 1);
        timeAliveText.text = "Time alive: " + timeAliveRounded + "s";
        starsCollectedText.text = "Stars collected: " + starsCollected.ToString();
        score = ((int)timeAliveRounded * 10) + starsCollected * 50;
        scoreText.text = "Score: " + score;
    }

    private void ResetScore()
    {
        bigText.text = "";
        scoreText.fontSize = 8;

        starTimer = 0;
        starScript.DestroyStar();

        timeAlive = 0;
        starsCollected = 0;
        UpdateScore();
    }
}
