using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enumerable = System.Linq.Enumerable;
using Item = GameStateManager.Item;

public class ItemManager : MonoBehaviour
{
    private Queue<Item> items = new Queue<Item>();
    public int maxItems = 3;
    public GameStateManager GSM;
    private int currentPriceStarLifeConverter = 10;

    public void PickupItem(Item newItem)
    {
        if (items.Count >= maxItems)
        {
            Item droppedItem = DropItem();
            Debug.Log($"Dropped {droppedItem.ItemName}");
        }

        items.Enqueue(newItem);
        Debug.Log($"Picked up {newItem.ItemName}");
    }

    public Item DropItem()
    {
        if (items.Count > 0)
        {
            return items.Dequeue();
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
        return Enumerable.Any(items, item => item.ItemName == itemNameToCheck);
    }
}
