using UnityEngine;

public class Star : MonoBehaviour
{
    public Camera camera;
    
    private void Awake()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    private void Update()
    {
        if (transform.position.y < camera.transform.position.y - 10)
        {
            Destroy(gameObject);
        }
    }
}
