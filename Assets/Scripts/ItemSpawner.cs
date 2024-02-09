using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    public ItemManager itemManager;
    private bool isExpertLevel;
    private float timer = 8f;
    public int secondsToSpawnNewItem = 0;
    public float chanceForStars;

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        isExpertLevel = sceneName == "ExpertLevel";
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
        float[] xPositions = isExpertLevel
            ? new float[] {-3.5f, -3, -2.5f, -2, -1.5f, -1, -0.5f, 0, 0.5f, 1, 1.5f, 2, 2.5f, 3, 3.5f}
            : new float[] {-3.5f, -3, -2.5f, -2, -1.5f, -1, -0.5f, 0, 0.5f, 1, 1.5f};
        Vector3 spawnPosition =
            new Vector3(xPositions[Random.Range(0, xPositions.Length)], transform.position.y + Random.Range(- 2.5f, 4.5f), 0);
        GameObject newItem = GetRandomItem();
        Instantiate(newItem, spawnPosition, Quaternion.identity);
    }

}