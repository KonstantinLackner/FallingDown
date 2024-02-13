using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gust : MonoBehaviour
{
    public Transform from;
    public Transform to;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 GetGustDirection()
    {
        return Vector3.Normalize(to.position - from.position);
    }
}
