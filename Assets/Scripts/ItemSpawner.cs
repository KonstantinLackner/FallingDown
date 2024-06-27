using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    public ItemManager itemManager;
    public GameStateManager GSM;
    private float timer = 8f;
    public int secondsToSpawnNewItem = 0;
    public float chanceForStars;
    public List<GameObject> items = new List<GameObject>();
    private ProgressTracker progressTracker;
    public GameObject redStar;

    private void Update()
    {
        if (timer < secondsToSpawnNewItem)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            SpawnNew();
        }
    }

    private GameObject GetRandomItem()
    {
        float randomNumber = Random.Range(0.0f, 1.0f);
        
        if (randomNumber < chanceForStars)
        {
            return items[0]; // 70% chance to pick the first item
        }

        int index = Random.Range(1, items.Count); // Random index among the remaining items

        while (itemManager.ItemExistsInQueueByName(items[index].name))
        {
            randomNumber = Random.Range(0.0f, 1.0f);
        
            if (randomNumber < chanceForStars)
            {
                return items[0]; // 70% chance to pick the first item
            }

            index = Random.Range(1, items.Count);
        }
        
        return items[index];
    }

    private void SpawnNew()
    {
        float[] xPositions = new float[] {-3.5f, -3, -2.5f, -2, -1.5f, -1, -0.5f, 0, 0.5f, 1, 1.5f};
        Vector3 spawnPosition =
            new Vector3(xPositions[Random.Range(0, xPositions.Length)], transform.position.y + Random.Range(-0.5f, 7.5f), 0);
        if (!isWallOrItemHere(spawnPosition))
        {
            GameObject newItem = GetRandomItem();
            Instantiate(newItem, spawnPosition, Quaternion.identity);
        }
    }

    public void SpawnAhead(Vector3 position)
    {
        StartCoroutine(MoveObjectAlongPath(position, new Vector3(position.x > 0 ? Random.Range(-0.5f,-1) : Random.Range(0.5f,1), Random.Range(5,6.5f), 0)));

    }

    private IEnumerator MoveObjectAlongPath(Vector3 position, Vector3 trajectory)
    {
        GameObject currentStar = Instantiate(redStar, position+trajectory/2, Quaternion.identity);
        int granularity = 16;
        for (int i = 0; i < granularity; i++)
        {
            currentStar.transform.position = currentStar.transform.position + trajectory /2/ granularity;
            yield return new WaitForSeconds(0.02f);
        }
        yield return null;
    }

    private bool isWallOrItemHere(Vector3 position)
    {
        // item
        List<Collider2D> results = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.NoFilter();
        Physics2D.OverlapBox(position, new Vector2(5, 5), 0, filter, results);

        foreach (Collider2D c in results)
        {
            if (c.gameObject.CompareTag("Item") || c.gameObject.CompareTag("Star") )
            {
                return true;
            }
        }

        // wall
        Physics2D.OverlapBox(position, new Vector2(1, 1), 0, filter, results);

        if (results.Count == 0)
        {
            return false;
        }
        else
        {
            foreach (Collider2D c in results)
            {
                if (c.gameObject.CompareTag("CollisionWall"))
                {
                    return true;
                }
            }
            return false;
        }
    }
}