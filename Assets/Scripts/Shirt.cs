using UnityEngine;

public class Shirt : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Awake()
    {
      spriteRenderer.color = new Color(Random.Range(0.0f, 1.0f),
          Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }
}
