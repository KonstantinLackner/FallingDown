using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BuildingGenerator : MonoBehaviour
{
    public Camera camera;
    private Vector3 currentPoint = new Vector3(10.73f, 90, 1);
    public List<GameObject> buildings = new List<GameObject>();
    private List<GameObject> currentBuildings = new List<GameObject>();

    private void Start()
    {
        GenerateBuildings(true);
    }

    void Update()
    {
        if (Math.Abs(camera.transform.position.y - (currentPoint.y - 150)) < 60) // 120 for being one house block away
        {
            RemoveOldBuildings();
            GenerateBuildings(false);
        }
    }

    private void GenerateBuildings(bool firstGeneration)
    {
        GameObject houseRow;
        if (firstGeneration)
        {
            houseRow = GameObject.Instantiate(buildings[0], currentPoint, Quaternion.identity);
        }
        else
        {
            houseRow = GameObject.Instantiate(GetRandomBuilding(), currentPoint, Quaternion.identity);
        }

        currentBuildings.Add(houseRow);
        currentPoint += new Vector3(0, 150, 0);
    }

    private GameObject GetRandomBuilding()
    {
        int index = Random.Range(1, buildings.Count);
        return buildings[index];
    }

    private void RemoveOldBuildings()
    {
        if (currentBuildings.Count >= 3)
        {
            int removeCount = currentBuildings.Count - 3;

            for (int i = 0; i < removeCount; i++)
            {
                Destroy(currentBuildings[i]);
            }

            currentBuildings.RemoveRange(0, removeCount);
        }
    }
}