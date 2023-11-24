using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    private bool fallingDown;
    public Rigidbody2D myRigidbody;
    public SpriteRenderer mySpriteRenderer;
    public List<Sprite> sprites;

    // Start is called before the first frame update
    void Start()
    {
        fallingDown = true;
    }

    // Update is called once per frame
    void Update()
    {
        mySpriteRenderer.flipY = myRigidbody.velocity.y > 0;
        if (Mathf.Abs(myRigidbody.velocity.y) <= 10.5)
        {
            mySpriteRenderer.sprite = sprites[0];
        } else if (Mathf.Abs(myRigidbody.velocity.y) >= 10.5 && Mathf.Abs(myRigidbody.velocity.y) <= 15)
        {
            mySpriteRenderer.sprite = sprites[1];
        }
        else
        {
            mySpriteRenderer.sprite = sprites[2];
        }
    }
}
