using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gust : MonoBehaviour
{
    public Transform from;
    public Transform to;
    private SpriteRenderer mySpriteRenderer;
    private BoxCollider2D myBoxCollider2D;
    public AudioSource gustAudioSource;
    private float timer = 0;

    void Start()
    {
        gustAudioSource = GameObject.Find("ACAudioSource").GetComponent<AudioSource>();
        mySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        myBoxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        gustAudioSource.Play();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer is > 2f and < 3)
        {
            gustAudioSource.Stop();
            mySpriteRenderer.color = new Color(1, 1, 1, 0);
            myBoxCollider2D.enabled = false;
        } else if (timer > 3f)
        {
            gustAudioSource.Play();
            mySpriteRenderer.color = new Color(1, 1, 1, 1);
            myBoxCollider2D.enabled = true;
            timer = 0;
        }
    }

    public Vector2 GetGustDirection()
    {
        return Vector3.Normalize(to.position - from.position);
    }
}
