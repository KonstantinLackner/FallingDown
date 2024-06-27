using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Enumerable = System.Linq.Enumerable;
using Image = UnityEngine.UI.Image;

public class ItemManager : MonoBehaviour
{
    private List<Item> currentItems = new List<Item>(); // The items the cat currently has equipped
    public int maxItems = 1;
    public GameStateManager GSM;
    public Cat cat;
    public int currentPriceStarLifeConverter = 10;
    public Image item1;
    public TMP_Text StarsTolifeConverterCost;
    
    public bool PickupItem(Item item)
    {
        if (ItemExistsInQueueByName(item.ItemName))
        {
            return false;
        }
        if (currentItems.Count >= maxItems)
        {
            // Drop the first item (mimicking queue behavior)
            Item droppedItem = DropItem();
        }

        currentItems.Add(item); // Add new item to the end of the list
        if (item.ItemName == "StarsToLifeConverter")
        {
            StarsTolifeConverterCost.text = currentPriceStarLifeConverter.ToString();
        }
        else
        {
            StarsTolifeConverterCost.text = "";
        }

        CheckCurrentItems();
        return true;
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
        
        if (ItemExistsInQueueByName("GravityBoots"))
        {
            GSM.currentTimeScale = 0.8f;
        }
        else
        {
            GSM.currentTimeScale = 1;
        }

        cat.inClaws = ItemExistsInQueueByName("Claws");
        cat.inParachute = ItemExistsInQueueByName("Parachute");
        cat.inRubberLines = ItemExistsInQueueByName("RubberLines");
        cat.inRubberWalls = ItemExistsInQueueByName("RubberWalls");
        cat.inWallJump = ItemExistsInQueueByName("WallJump");
        
        GSM.ApplyAllItemChanges();
    }

    public void IncreaseStarsToLifeConverterCost()
    {
        currentPriceStarLifeConverter += 5;
        StarsTolifeConverterCost.text = currentPriceStarLifeConverter.ToString();
    }
}
