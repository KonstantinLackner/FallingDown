using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Enumerable = System.Linq.Enumerable;
using Image = UnityEngine.UI.Image;

public class ItemManager : MonoBehaviour
{
    private List<Item> currentItems = new List<Item>(); // The items the cat currently has equipped
    public int maxItems = 3;
    public GameStateManager GSM;
    public Cat cat;
    public int currentPriceStarLifeConverter = 10;
    public List<Item> items = new List<Item>(); // All items in the game (they are prefabs and collected here)
    public Image item1;
    public Image item2;
    public Image item3;
    
    public void PickupItem(Item item)
    {
        if (ItemExistsInQueueByName(item.ItemName))
        {
            return;
        }
        if (currentItems.Count >= maxItems)
        {
            // Drop the first item (mimicking queue behavior)
            Item droppedItem = DropItem();
            Debug.Log($"Dropped {droppedItem.ItemName}");
        }

        currentItems.Add(item); // Add new item to the end of the list
        Debug.Log($"Picked up {item.ItemName}");

        CheckCurrentItems();
    }

    public Item DropItem()
    {
        if (currentItems.Count > 0)
        {
            var itemToDrop = currentItems[0]; // Get the first item
            currentItems.RemoveAt(0); // Remove it from the list
            return itemToDrop; // Return the dropped item
        }
        return null;
    }

    public bool CanRespawnWithConverter()
    {
        if (ItemExistsInQueueByName("StarsToLifeConverter") && GSM.currentStars >= currentPriceStarLifeConverter)
        {
            return true;
        }

        return false;
    }
    
    public bool ItemExistsInQueueByName(string itemNameToCheck)
    {
        return Enumerable.Any(currentItems, item => item.ItemName == itemNameToCheck);
    }

    private void CheckCurrentItems()
    {
        if (currentItems.Count >= 1)
        {
            item1.sprite = currentItems[0].ItemSprite;
        }
        if (currentItems.Count >= 2)
        {
            item2.sprite = currentItems[1].ItemSprite;
        }
        if (currentItems.Count >= 3)
        {
            item3.sprite = currentItems[2].ItemSprite;
        }
        
        if (ItemExistsInQueueByName("GravityBoots"))
        {
            GSM.currentTimeScale = 0.5f;
        }

        cat.inClaws = ItemExistsInQueueByName("Claws");
        cat.inParachute = ItemExistsInQueueByName("Parachute");
        
        GSM.ApplyAllItemChanges();
    }
}
