using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Star : MonoBehaviour
{
    public GameObject myPrefab;
    private GameObject currentStar;
    private Vector3 previousStarPosition = new Vector3(0,0,0);
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

        float[] distances = new float[] {-1.5f, -2f, 1.5f, 2f};
        Vector3 spawnPosition = new Vector3((previousStarPosition.x + distances[Random.Range(0,distances.Length)]) % 3, Random.Range(-1.5f, 3.5f), 0);
        currentStar = Instantiate(myPrefab, spawnPosition, Quaternion.identity);
    }

    public void DestroyStar()
    {
        if (currentStar)
        {
            previousStarPosition = currentStar.transform.localPosition;
            Destroy(currentStar);
        }
    }
}
