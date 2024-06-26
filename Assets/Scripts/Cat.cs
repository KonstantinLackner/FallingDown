using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cat : MonoBehaviour
{
    private bool fallingDown;
    private bool inDrill;
    public bool inClaws;
    public bool inParachute;
    public bool inRubberLines;
    public bool inRubberWalls;
    public bool inWallJump;
    public ParticleSystem clawsParticleSystem;
    private bool hitDrillWP1;
    private bool hitDrillWP2;
    private Vector3 drillWP1;
    private Vector3 drillWP2;
    private bool atWall;
    public AudioSource drillAudioSource;
    public AudioSource clawAudioSource;
    public AudioSource respawnAudioSource;
    public AudioSource starCountAudioSource;
    public AudioSource shirtAudioSource;
    public AudioSource weeeAudioSource;
    public AudioSource ooohAudioSource;
    public AudioSource ngahAudioSource;
    public AudioSource wowAudioSource;
    public Rigidbody2D myRigidbody;
    public SpriteRenderer mySpriteRenderer;
    public SpriteRenderer ShirtCoverSpriteRenderer;
    public SpriteRenderer ShirtCoverSpriteRendererOuter;
    public ItemManager itemManager;
    public List<Sprite> sprites;
    public Transform spawnPoint;

    public GameStateManager GSM;
    public AudioSource bounceAudioSource;

    public float spriteChangeSpeed1;
    public float spriteChangeSpeed2;
    
    public float maxMultiplier = 3f; // The maximum velocity multiplier at low speeds
    public float maxSpeedForMultiplier = 15f; // Speed at which the multiplier becomes 1
    private Vector2[] bounceSpeedMemory = new Vector2[3] {new Vector2(0,0), new Vector2(0,0), new Vector2(0,0)};
    public bool isLatestLineSlopey = false;
    public ItemSpawner itemSpawner;
    
    void Start()
    {
        clawsParticleSystem.Stop();
        transform.position = spawnPoint.position;
        ShirtCoverSpriteRenderer.color = new Color(1, 1, 1, 0);
        ShirtCoverSpriteRendererOuter.color = new Color(0, 0, 0, 0);
    }

    void Update()
    {
        // Sprite & sound stuff
        float yFlip = 0.15 * myRigidbody.velocity.y > 0 ? -1 : 1;
        transform.localScale = new Vector3(1, yFlip, 1);
        if (Mathf.Abs(myRigidbody.velocity.y) <= spriteChangeSpeed1)
        {
            mySpriteRenderer.sprite = sprites[0];
        }
        else if (Mathf.Abs(myRigidbody.velocity.y) >= spriteChangeSpeed1 &&
                 Mathf.Abs(myRigidbody.velocity.y) <= spriteChangeSpeed2)
        {
            mySpriteRenderer.sprite = sprites[1];
        }
        else
        {
            if (mySpriteRenderer.sprite != sprites[2] && myRigidbody.velocity.y > 17) weeeAudioSource.Play();
            mySpriteRenderer.sprite = sprites[2];
        }

        if (inDrill)
        {
            MovementInDrill();
        }

        if (inClaws)
        {
            MovementClaws();
        }

        if (inParachute)
        {
            MovementParachute();
        }
    }

    private void MovementClaws()
    {
        if (myRigidbody.velocity.y < -3 && atWall)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, -3);
        }
    }
    
    private void MovementParachute()
    {
        if (myRigidbody.velocity.y < -3)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, -3);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("CollisionWall"))
        {
            if (inRubberWalls)
            {
                myRigidbody.velocity += myRigidbody.velocity.x > 0 ? new Vector2(3.5f, 0) : new Vector2(-3.5f,0);
            }

            if (inWallJump)
            {
                bounceAudioSource.Play();
                myRigidbody.velocity = myRigidbody.velocity.x > 0 ? new Vector2(2, 10) : new Vector2(-2,10);
            }
        }

        if (collision.gameObject.CompareTag("Line"))
        {

            bounceAudioSource.Play();
            if (isLatestLineSlopey) 
            {
                ooohAudioSource.Play();
                itemSpawner.SpawnAhead(myRigidbody.position);
            }

            Vector2 oldVelocity = myRigidbody.velocity;
            Vector2 newVelocity = oldVelocity;

            Vector2 favourVertical = new Vector2(0.5f, 1);
            float currentSpeed = myRigidbody.velocity.magnitude;
            float multiplier = 1 + (maxMultiplier - 1) * (1 - Mathf.Clamp01(currentSpeed / maxSpeedForMultiplier));
            if (multiplier > 1)
            {
                newVelocity *= multiplier * favourVertical;
            }
            if (inParachute || (inClaws && atWall)) // This is to mitigate the effect of being slow on the bounce
            {
                newVelocity *= 2;
            }

            if (inRubberLines)
            {
                newVelocity *= 1.5f;
            }

            myRigidbody.velocity = newVelocity;

            bounceSpeedMemory[0] = bounceSpeedMemory[1];
            bounceSpeedMemory[1] = bounceSpeedMemory[2];
            bounceSpeedMemory[2] = newVelocity;
            if (bounceSpeedMemory[0].y < 7 && bounceSpeedMemory[1].y < 7 && newVelocity.y < 7)
            {
                ngahAudioSource.Play();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Gust"))
        {
            Vector2 newVelocity = other.gameObject.GetComponent<Gust>().GetGustDirection();
            myRigidbody.velocity += newVelocity * 0.5f;
            if (newVelocity.y >= -0.1f)
            {
                myRigidbody.velocity += new Vector2(0, 0.1f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Shirt"))
        {
            shirtAudioSource.Play();
            ShirtCoverSpriteRenderer.color = col.gameObject.GetComponent<SpriteRenderer>().color;
            ShirtCoverSpriteRendererOuter.color = Color.black;
            StartCoroutine(ResolveShirt());
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("Star"))
        {
            GSM.CollectStar();
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("RedStar"))
        {
            GSM.CollectRedStar();
            Destroy(col.gameObject);
        }

        if (col.gameObject.CompareTag("Item"))
        {
            if (inClaws)
            {
                clawsParticleSystem.Stop();
                clawAudioSource.Stop();
                atWall = false;
            }
            wowAudioSource.Play();
            Item itemToPickUp = col.gameObject.GetComponent<Item>();
            if (itemManager.PickupItem(itemToPickUp))
            {
                GSM.startQuip(itemToPickUp);
            }

            Destroy(col.gameObject);
        }

        if (col.gameObject.CompareTag("Drill"))
        {
            drillAudioSource.Play();
            myRigidbody.simulated = false;
            myRigidbody.bodyType = RigidbodyType2D.Kinematic;
            myRigidbody.velocity = Vector2.zero;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            inDrill = true;
            drillWP1 = col.GetComponent<Drill>().WP1.transform.position;
            drillWP2 = col.GetComponent<Drill>().WP2.transform.position;
            Destroy(col.gameObject);
        }

        if (inClaws && col.gameObject.CompareTag("Wall"))
        {
            clawsParticleSystem.Play();
            clawAudioSource.Play();
            atWall = true;
            myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);
        }
        
        if (col.gameObject.CompareTag("Cloud"))
        {
            if (myRigidbody.velocity.y > 0)
            {
                myRigidbody.velocity += new Vector2(0, 10);
            }
            else
            {
                myRigidbody.velocity = new Vector2(-myRigidbody.velocity.x, 10);
            }
        }
        
        if (col.gameObject.CompareTag("KillBox"))
        {
            if (itemManager.CanRespawnWithConverter())
            {
                StartCoroutine(Respawn());
            }
            else
            {
                GSM.GameOver();
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator ResolveShirt()
    {
        yield return new WaitForSeconds(3);
        float currentTime = Time.deltaTime;
        Color startColour = ShirtCoverSpriteRenderer.color;
        while (currentTime < 3)
        {
            float alpha = Mathf.Lerp(1f, 0f, currentTime / 3);
            ShirtCoverSpriteRenderer.color = new Color(startColour.r, startColour.g, startColour.b, alpha);
            ShirtCoverSpriteRendererOuter.color = new Color(0, 0, 0, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }

        ShirtCoverSpriteRenderer.color = new Color(1, 1, 1, 0);
        ShirtCoverSpriteRendererOuter.color = new Color(0, 0, 0, 0);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (inClaws && col.gameObject.CompareTag("Wall"))
        {
            clawsParticleSystem.Stop();
            clawAudioSource.Stop();
            atWall = false;
        }
    }

    void MovementInDrill()
    {
        if (transform.position == drillWP1)
        {
            hitDrillWP1 = true;
        }

        if (transform.position == drillWP2)
        {
            hitDrillWP2 = true;
        }
        
        float step = 5 * Time.deltaTime;
        if (!hitDrillWP1)
        {
            transform.position = Vector3.MoveTowards(transform.position, drillWP1, step);
        }
        else if (!hitDrillWP2)
        {
            transform.position = Vector3.MoveTowards(transform.position, drillWP2, step);
        }
        else
        {
            myRigidbody.simulated = true;
            myRigidbody.bodyType = RigidbodyType2D.Dynamic;
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
            inDrill = false;
            hitDrillWP1 = false;
            hitDrillWP2 = false;
            Vector2 velocity = (transform.position - drillWP1).normalized * 10;
            myRigidbody.velocity = velocity;
        }
    }

    private IEnumerator Respawn()
    {
        myRigidbody.velocity = Vector2.zero;
        myRigidbody.simulated = false;
        myRigidbody.bodyType = RigidbodyType2D.Kinematic;
        for (int i = itemManager.currentPriceStarLifeConverter; i > 0; i--)
        {
            yield return new WaitForSeconds(0.3f + (i * 0.02f));
            starCountAudioSource.Play();
            GSM.removeStar();
        }
        itemManager.IncreaseStarsToLifeConverterCost();
        respawnAudioSource.Play();
        clawsParticleSystem.Play();
        gameObject.SetActive(true);
        transform.position = spawnPoint.position;
        yield return new WaitForSeconds(1);
        clawsParticleSystem.Stop();
        myRigidbody.simulated = true;
        myRigidbody.bodyType = RigidbodyType2D.Dynamic;
    }
}