using System.Collections.Generic;
using UnityEngine;
using Enumerable = System.Linq.Enumerable;

public class ItemManager : MonoBehaviour
{
    private Queue<Item> currentItems = new Queue<Item>(); // The items the cat currently has equipped
    public int maxItems = 3;
    public GameStateManager GSM;
    private int currentPriceStarLifeConverter = 10;
    public List<Item> items = new List<Item>(); // All items in the game (they are prefabs and collected here)
    
    public void PickupItem(Item item)
    {
        if (currentItems.Count >= maxItems)
        {
            Item droppedItem = DropItem();
            Debug.Log($"Dropped {droppedItem.ItemName}");
        }

        currentItems.Enqueue(item);
        Debug.Log($"Picked up {item.ItemName}");
    }

    public Item DropItem()
    {
        if (currentItems.Count > 0)
        {
            return currentItems.Dequeue();
        }
        return null;
    }

    public bool CanRespawnWithConverter()
    {
        if (ItemExistsInQueueByName("StarToLifeConverter") && GSM.currentStars >= currentPriceStarLifeConverter)
        {
            GSM.currentStars -= currentPriceStarLifeConverter;
            currentPriceStarLifeConverter += 5;
            return true;
        }

        return false;
    }
    
    bool ItemExistsInQueueByName(string itemNameToCheck)
    {
        return Enumerable.Any(currentItems, item => item.ItemName == itemNameToCheck);
    }
}
