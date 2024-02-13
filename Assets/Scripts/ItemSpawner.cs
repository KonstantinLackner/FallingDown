using System;
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

    private void Start()
    {
        progressTracker = GameObject.Find("ProgressTracker").GetComponent<ProgressTracker>();
        items = progressTracker.GetAvailableItems();
    }

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
            Debug.Log("Spawning new Item: " + newItem + " at position: " + spawnPosition);
            Instantiate(newItem, spawnPosition, Quaternion.identity);
        }
    }

    private bool isWallOrItemHere(Vector3 position)
    {

        List<Collider2D> results = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.NoFilter();
        Physics2D.OverlapBox(position, new Vector2(0.75f, 0.75f), 0, filter, results);

        if (results.Count == 0)
        {
            return false;
        }
        else
        {
            foreach (Collider2D c in results)
            {
                if (c.gameObject.CompareTag("CollisionWall") || c.gameObject.CompareTag("Item"))
                {
                    // Debug.Log("Wall/Item is here! at " + position);
                    return true;
                }
            }
            return false;
        }
    }

}