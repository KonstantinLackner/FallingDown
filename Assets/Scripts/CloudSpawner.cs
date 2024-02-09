using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    private float timer = 0f;
    public int secondsToSpawnNewCloud;
    public GameStateManager GSM;
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
        float[] xPositions = GSM.isExpertMode
            ? new float[] {-3.5f, -3, -2.5f, -2, -1.5f, -1, -0.5f, 0, 0.5f, 1, 1.5f, 2, 2.5f, 3, 3.5f}
            : new float[] {-3.5f, -3, -2.5f, -2, -1.5f, -1, -0.5f, 0, 0.5f, 1, 1.5f};
        Vector3 spawnPosition =
            new Vector3(xPositions[Random.Range(0, xPositions.Length)], transform.position.y + Random.Range(- 2.5f, 4.5f), 0);
        Instantiate(cloud, spawnPosition, Quaternion.identity);
    }
}
