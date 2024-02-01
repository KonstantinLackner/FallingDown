using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class StarSpawner : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    private GameObject currentStar;
    private Vector3 previousStarPosition = new Vector3(0, 0, 0);
    private bool isExpertLevel;

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        isExpertLevel = sceneName == "ExpertLevel";
    }

    public void SpawnNew()
    {
        DestroyStar();
        float[] xPositions = isExpertLevel
            ? new float[] { -3.5f, -3, -2.5f, -2, -1.5f, -1, -0.5f, 0, 0.5f, 1, 1.5f, 2, 2.5f, 3, 3.5f }
            : new float[] { -3.5f, -3, -2.5f, -2, -1.5f, -1, -0.5f, 0, 0.5f, 1, 1.5f };
        Vector3 spawnPosition =
            new Vector3(xPositions[Random.Range(0, xPositions.Length)], Random.Range(-1.5f, 3.5f), 0);
        Debug.Log("spawn position: " + spawnPosition);
        currentStar = Instantiate(GetRandomItem(), spawnPosition, Quaternion.identity);
    }

    public void DestroyStar()
    {
        if (currentStar)
        {
            previousStarPosition = currentStar.transform.localPosition;
            Destroy(currentStar);
        }
    }


    public GameObject GetRandomItem()
    {
        float randomNumber = Random.Range(0.0f, 1.0f);

        if (randomNumber < 0.7f)
        {
            return items[0]; // 70% chance to pick the first item
        }

        int index = Random.Range(1, items.Count); // Random index among the remaining items
        return items[index];
    }
}