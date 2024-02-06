using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGenerator : MonoBehaviour
{
    public Camera camera;
    private Vector3 startLeft = new Vector3(-12.93f, 10, 0);
    private Vector3 startRight = new Vector3(10.75f, 8, 0);
    private List<GameObject> currentBuildings = new List<GameObject>();
    public GameObject housePrefab;

    private void Start()
    {
        GenerateBuildings();
    }

    void Update()
    {
        if (camera.transform.position.y > startLeft.y + 3)
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
            
            startLeft = startLeft + new Vector3(0, 30, 0);
            startRight = startRight + new Vector3(0, 30, 0);
            GenerateBuildings();
        }
    }

    private void GenerateBuildings()
    {
        GameObject left1 = GameObject.Instantiate(housePrefab, startLeft, Quaternion.identity);
        left1.transform.localScale = new Vector3(-1, 1, 1);
        GameObject right1 = GameObject.Instantiate(housePrefab, startRight, Quaternion.identity);
            
        GameObject left2 = GameObject.Instantiate(housePrefab, startLeft + new Vector3(0,30,0), Quaternion.identity);
        left2.transform.localScale = new Vector3(-1, 1, 1);
        GameObject right2 = GameObject.Instantiate(housePrefab, startRight+ new Vector3(0,30,0), Quaternion.identity);
            
        currentBuildings.Add(left1);
        currentBuildings.Add(left2);
        currentBuildings.Add(right1);
        currentBuildings.Add(right2);
    }
}
