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

    public TMP_Text timeAliveText;
    public TMP_Text starsCollectedText;
    public TMP_Text scoreText;
    private float timeAlive = 0f;
    private int starsCollected = 0;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = spawnPoint.position;
        fallingDown = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (fallingDown)
        {
            mySpriteRenderer.flipY = myRigidbody.velocity.y > 0;
            if (Mathf.Abs(myRigidbody.velocity.y) <= 10.5)
            {
                mySpriteRenderer.sprite = sprites[0];
            }
            else if (Mathf.Abs(myRigidbody.velocity.y) >= 10.5 && Mathf.Abs(myRigidbody.velocity.y) <= 15)
            {
                mySpriteRenderer.sprite = sprites[1];
            }
            else
            {
                mySpriteRenderer.sprite = sprites[2];
            }

            timeAlive += Time.deltaTime;
            UpdateScore();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            ResetScore();
            Respawn();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Game over!");
            fallingDown = false;
        }
    }

    private void Respawn()
    {
        fallingDown = true;
        transform.position = spawnPoint.position;
        myRigidbody.velocity = Vector2.zero;
        myRigidbody.angularVelocity = 0f;
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
        timeAlive = 0;
        starsCollected = 0;
        UpdateScore();
    }
}
