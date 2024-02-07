using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public SpriteRenderer spriteRenderer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cat"))
        {
            particleSystem.Play();
            spriteRenderer.color = new Color(1, 1, 1, 0);
            StartCoroutine(destroyCloud());
        }
    }

    IEnumerator destroyCloud()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

}
