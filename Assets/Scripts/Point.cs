using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    private bool locked = false;
    public KeyCode myNumber;
    public SpriteRenderer myRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        myRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        locked = Input.GetKey(myNumber);
        myRenderer.enabled = locked;
        
        // If the point hasn't been selected yet, move it to the current y position of the mouse
        if (!locked)
        {
            transform.position = new Vector3(transform.position.x,
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        }
    }
}
