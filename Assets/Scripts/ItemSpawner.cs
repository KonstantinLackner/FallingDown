using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    private GameObject currentItem;
    private bool isExpertLevel;
    private float timer = 8f;
    public AudioSource starDisappearAudioSource;
    public GameStateManager GSM;

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        isExpertLevel = sceneName == "ExpertLevel";
    }

    private void Update()
    {
        if (timer < 10)
        {
            timer += Time.deltaTime;
        }
        else if (timer is >= 10 and <= 12)
        {
            if (currentItem != null)
            {
                // If the item wasn't collected by the player in time
                DestroyCurrentItem();
                starDisappearAudioSource.Play();
                GSM.starsMissed++;
            }

            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            SpawnNew();
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

    private void SpawnNew()
    {
        float[] xPositions = isExpertLevel
            ? new float[] {-3.5f, -3, -2.5f, -2, -1.5f, -1, -0.5f, 0, 0.5f, 1, 1.5f, 2, 2.5f, 3, 3.5f}
            : new float[] {-3.5f, -3, -2.5f, -2, -1.5f, -1, -0.5f, 0, 0.5f, 1, 1.5f};
        Vector3 spawnPosition =
            new Vector3(xPositions[Random.Range(0, xPositions.Length)], transform.position.y + Random.Range(- 1.5f, 3.5f), 0);
        GameObject newItem = GetRandomItem();
        Debug.Log("Spawning " + newItem + "at position: " + spawnPosition);
        currentItem = Instantiate(newItem, spawnPosition, Quaternion.identity);
    }

    public void DestroyCurrentItem()
    {
        if (currentItem != null) 
        {
            Debug.Log("Dstroying current item: " + currentItem);
            Destroy(currentItem);
            currentItem = null;
        }
        else 
        {
            Debug.Log("Instructed to destroy item, but there is none.");
        }
    }
}