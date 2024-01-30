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

        float[] xPositions = isExpertLevel ? new float[] {-3.5f, -3, -2.5f, -2, -1.5f, -1, -0.5f, 0, 0.5f, 1, 1.5f, 2, 2.5f, 3, 3.5f} : new float[] {-3.5f, -3, -2.5f, -2, -1.5f, -1, -0.5f, 0, 0.5f, 1, 1.5f};
        Vector3 spawnPosition = new Vector3(xPositions[Random.Range(0,xPositions.Length)], Random.Range(-1.5f, 3.5f), 0);
        Debug.Log("spawn position: " + spawnPosition);
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
