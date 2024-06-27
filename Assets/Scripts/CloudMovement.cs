using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float speed;

    public GameObject leftWayPoint;
    public GameObject rightWayPoint;

    private Vector2 leftWayPointPosition;
    private Vector2 rightWayPointPosition;

    private bool headingRight = true;
    private bool alive = true;

    void Start()
    {
        leftWayPointPosition = leftWayPoint.transform.position;
        rightWayPointPosition = rightWayPoint.transform.position;
    }

    void Update()
    {
        float step = speed * Time.deltaTime;

        if (alive)
        {
            if (Vector2.Distance(transform.position, leftWayPointPosition) < 0.1)
            {
                headingRight = true;
            }

            if (Vector2.Distance(transform.position, rightWayPointPosition) < 0.1)
            {
                headingRight = false;
            }

            // move sprite towards the target location
            transform.position = Vector2.MoveTowards(transform.position,
                headingRight ? rightWayPointPosition : leftWayPointPosition, step);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cat"))
        {
            StartCoroutine(dieAndResurrect());
        }
    }

    private IEnumerator dieAndResurrect()
    {
        yield return new WaitForSeconds(0.5f);
        transform.position = leftWayPointPosition;
        alive = false;
        yield return new WaitForSeconds(6);
        alive = true;
        transform.position = leftWayPointPosition + new Vector2(1, 0);
    }
}