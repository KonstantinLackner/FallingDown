using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BuildingGenerator : MonoBehaviour
{
    public Camera camera;
    private float startLeft = -12.93f;
    private float startRight = 10.75f;
    private float currentHeight = -10;
    private List<GameObject> currentBuildings = new List<GameObject>();
    public GameObject h1Prefab;
    public List<GameObject> availableBuildings;

    private void Start()
    {
        GenerateBuildings(true);
    }

    void Update()
    {
        if (camera.transform.position.y > currentHeight - 50)
        {
            RemoveOldBuildings();
            GenerateBuildings(false);
        }
    }

    private void GenerateBuildings(bool firstGeneration)
    {
        GameObject left1;
        GameObject left2;
        GameObject right1;
        GameObject right2;
        Vector3 left = new Vector3(startLeft, currentHeight, 0);
        Vector3 right = new Vector3(startRight, currentHeight, 0);
        
        if (firstGeneration)
        {
            left1 = GameObject.Instantiate(h1Prefab, left, Quaternion.identity);
            right1 = GameObject.Instantiate(h1Prefab, right, Quaternion.identity);
            left2 = GameObject.Instantiate(h1Prefab, left + new Vector3(0,30,0), Quaternion.identity);
            right2 = GameObject.Instantiate(h1Prefab, right+ new Vector3(0,30,0), Quaternion.identity);
        }
        else
        {
            left1 = GameObject.Instantiate(GetRandomBuilding(), left, Quaternion.identity);
            right1 = GameObject.Instantiate(GetRandomBuilding(), right, Quaternion.identity);
            left2 = GameObject.Instantiate(GetRandomBuilding(), left + new Vector3(0, 30, 0),
                Quaternion.identity);
            right2 = GameObject.Instantiate(GetRandomBuilding(), right + new Vector3(0, 30, 0),
                Quaternion.identity);
        }

        left1.transform.localScale = new Vector3(-left1.transform.localScale.x, 1, 1);
        left2.transform.localScale = new Vector3(-left2.transform.localScale.x, 1, 1);

        left1.GetComponent<Building>().houseSpriteRenderer.color = new Color(Random.Range(0.0f, 1.0f),
            Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        left2.GetComponent<Building>().houseSpriteRenderer.color = new Color(Random.Range(0.0f, 1.0f),
            Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        right1.GetComponent<Building>().houseSpriteRenderer.color = new Color(Random.Range(0.0f, 1.0f),
            Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        right2.GetComponent<Building>().houseSpriteRenderer.color = new Color(Random.Range(0.0f, 1.0f),
            Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            
        currentBuildings.Add(left1);
        currentBuildings.Add(left2);
        currentBuildings.Add(right1);
        currentBuildings.Add(right2);

        currentHeight += 60;
    }
    
    private GameObject GetRandomBuilding()
    {
        float randomNumber = Random.Range(0.0f, 1.0f);

        if (randomNumber < 0.33)
        {
            return availableBuildings[0];
        }

        int index = Random.Range(1, availableBuildings.Count); // Random index among the remaining items
        return availableBuildings[index];
    }

    private void RemoveOldBuildings()
    {
        if (currentBuildings.Count >= 4)
        {
            int removeCount = currentBuildings.Count - 4;

            for (int i = 0; i < removeCount; i++)
            {
                Destroy(currentBuildings[i]);
            }

            currentBuildings.RemoveRange(0, removeCount);
        }
    }
}
