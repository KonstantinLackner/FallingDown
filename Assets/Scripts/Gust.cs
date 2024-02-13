using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gust : MonoBehaviour
{
    public Transform from;
    public Transform to;

    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer is > 3f and < 4)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        } else if (timer > 4f)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            timer = 0;
        }
    }

    public Vector2 GetGustDirection()
    {
        return Vector3.Normalize(to.position - from.position);
    }
}
