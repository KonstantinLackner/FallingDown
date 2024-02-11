using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressTracker : MonoBehaviour
{
    public static ProgressTracker Instance { get; private set; }
    public int starCount = 0;
    public int maxHeight;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make this object persistent
        }
        else
        {
            Destroy(gameObject); // Ensure there are no duplicate instances
        }
    }

    public void IncreaseStarCount(int starsCollected)
    {
        starCount += starsCollected;
    }
}
