using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    public RawImage img;
    public GameObject cat;
    public float factor;

    // Update is called once per frame
    void Update()
    {
        img.uvRect = new Rect(img.uvRect.position +
                              new Vector2(0, cat.GetComponent<Rigidbody2D>().velocity.y) * factor * Time.deltaTime, img.uvRect.size);
    }
}
