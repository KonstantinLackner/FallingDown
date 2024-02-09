using UnityEngine;

public class Item : MonoBehaviour
{
    public string ItemName;
    public Sprite ItemSprite;
    public string ItemText;
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
