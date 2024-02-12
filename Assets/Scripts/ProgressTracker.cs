using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressTracker : MonoBehaviour
{
    public static ProgressTracker Instance { get; private set; }
    public List<GameObject> unlockableItems = new List<GameObject>();
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

    public List<GameObject> GetAvailableItems()
    {
        return starCount switch
        {
            < 10 => unlockableItems.GetRange(0, 2),
            >= 10 and < 20 => unlockableItems.GetRange(0, 3),
            >= 20 and < 30 => unlockableItems.GetRange(0, 4),
            >= 30 and < 40 => unlockableItems.GetRange(0, 5),
            >= 40 and < 50 => unlockableItems.GetRange(0, 6),
            >= 50 and < 60 => unlockableItems.GetRange(0, 7),
            _ => unlockableItems
        };
    }
}