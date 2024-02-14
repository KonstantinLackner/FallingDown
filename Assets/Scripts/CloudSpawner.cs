using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    private float timer = 0f;
    public int secondsToSpawnNewCloud;
    public GameObject cloud;

    // Update is called once per frame
    void Update()
    {
        if (timer < secondsToSpawnNewCloud)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            SpawnNew();
        }
    }
    
    
    private void SpawnNew()
    {
        float[] xPositions = new float[] {-3.5f, -3, -2.5f, -2, -1.5f, -1, -0.5f, 0, 0.5f, 1, 1.5f};
        Vector3 spawnPosition =
            new Vector3(xPositions[Random.Range(0, xPositions.Length)], transform.position.y + Random.Range(- 2.5f, 4.5f), 0);
        if (!isCloudNear(spawnPosition)) Instantiate(cloud, spawnPosition, Quaternion.identity);
    }

    private bool isCloudNear(Vector3 position)
    {
        List<Collider2D> results = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.NoFilter();
        Physics2D.OverlapBox(position, new Vector2(5f, 5f), 0, filter, results);

        if (results.Count == 0)
        {
            return false;
        }
        else
        {
            foreach (Collider2D c in results)
            {
                if (c.gameObject.CompareTag("Cloud"))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
