using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public GameObject myPrefab;
    private GameObject currentStar;

    public void SpawnNew()
    {
        DestroyStar();

        Vector3 spawnPosition = new Vector3(Random.Range(-3.5f, 3.5f), Random.Range(-1.5f, 3.5f), 0);
        currentStar = Instantiate(myPrefab, spawnPosition, Quaternion.identity);
    }

    public void DestroyStar()
    {
        if (currentStar)
        {
            Destroy(currentStar);
        }
    }
}
