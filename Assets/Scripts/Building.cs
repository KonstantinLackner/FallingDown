using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Building : MonoBehaviour
{
    public SpriteRenderer houseSpriteRenderer;

    private void Awake()
    {
        houseSpriteRenderer.color = new Color(Random.Range(0.3f, 0.8f),
            Random.Range(0.3f, 0.8f), Random.Range(0.3f, 0.8f));;
    }
}
