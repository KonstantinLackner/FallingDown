using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f,0f,-10f);
    private float smoothTime = 0.25f;
    Vector3 velocity = Vector2.zero;
    public Transform target;
    private float highPoint = 0; // It's over, Anakin, I have the high ground!
    

    void Update()
    {
        if (target != null && target.position.y > highPoint)
        {
            highPoint = target.position.y;
        }
        Vector3 targetPosition = new Vector3(0, highPoint, -10f) + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
