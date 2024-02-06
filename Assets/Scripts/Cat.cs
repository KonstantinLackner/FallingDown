using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    private bool fallingDown;
    public Rigidbody2D myRigidbody;
    public SpriteRenderer mySpriteRenderer;
    public ItemManager itemManager;
    public ItemSpawner itemSpawner;
    public List<Sprite> sprites;
    public Transform spawnPoint;

    public GameStateManager GSM;
    public AudioSource bounceAudioSource;

    public float spriteChangeSpeed1;
    public float spriteChangeSpeed2;
    
    public float maxMultiplier = 3f; // The maximum velocity multiplier at low speeds
    public float maxSpeedForMultiplier = 15f; // Speed at which the multiplier becomes 1



    void Start()
    {
        transform.position = spawnPoint.position;
    }

    void Update()
    {
        // Sprite stuff
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
            mySpriteRenderer.sprite = sprites[2];
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector2 velocity = myRigidbody.velocity;
            float xVelocity = velocity.x;
            float yVelocity = velocity.y;
            Vector2 newVelocity = new Vector2(0, 0);

            if (yVelocity > 0 && xVelocity != 0)
            {
                myRigidbody.simulated = false;
                newVelocity = new Vector2(xVelocity,
                    Mathf.Abs(yVelocity + Mathf.Max(Mathf.Min(xVelocity * 0.5f, 2), 0.5f)));
                myRigidbody.velocity = newVelocity;

                myRigidbody.simulated = true;
            }

            Debug.Log("velocity: " + velocity + "| velocity after: " + newVelocity);
        }

        if (collision.gameObject.CompareTag("Line"))
        {
            bounceAudioSource.Play();

            float currentSpeed = myRigidbody.velocity.magnitude;
            float multiplier = 1 + (maxMultiplier - 1) * (1 - Mathf.Clamp01(currentSpeed / maxSpeedForMultiplier));
            if (multiplier > 1)
            {
                myRigidbody.velocity *= multiplier;
            }
        }
    }

    

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Star"))
        {
            itemSpawner.DestroyCurrentItem();
            GSM.CollectStar();
        }

        if (col.gameObject.CompareTag("Item"))
        {
            Item itemToPickUp = col.gameObject.GetComponent<Item>();
            itemSpawner.DestroyCurrentItem();
            itemManager.PickupItem(itemToPickUp);
            GSM.startQuip(itemToPickUp);
        }

        if (col.gameObject.CompareTag("Cloud"))
        {
            myRigidbody.velocity += new Vector2(0, 10);
        }
        
        if (col.gameObject.CompareTag("KillBox"))
        {
            if (itemManager.CanRespawnWithConverter())
            {
                // TODO: RespawnMethod maybe with a timer and a nice animation or at least particle system with star sprite shooting out?
            }
            else
            {
                GSM.GameOver();
            }
        }
    }
}