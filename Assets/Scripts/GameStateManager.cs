using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class GameStateManager : MonoBehaviour
{
    private bool gameIsRunning = true;
    private float currentTimeScale = 1; // I added this as the gravity boots could just slow down the time scale
    public GameObject quipWindow;
    public TMP_Text quipWindowText;
    public Image quipWindowSprite;
    
    [System.Serializable]
    public class Item
    {
        public Sprite ItemSprite { get; set; }
        public string ItemText { get; set; }

        public Item(Sprite sprite, string text)
        {
            ItemSprite = sprite;
            ItemText = text;
        }
    }
    
    private Dictionary<string, Item> items = new Dictionary<string, Item>();

    void Start()
    {
        items.Add("gravityBoots", new Item(null, "These gravity boots will take some of that weight off your paws! ... and they look fancy."));
        items.Add("parachute", new Item(null, "That's a parachute that slows down your fall ... 'Sie müssen nur den Nippel durch die Lasche zieh'n'"));
        items.Add("claws", new Item(null, "Those claws look mighty sharp, try clawing that those walls, maybe."));
        items.Add("starToLifeConverter", new Item(null, "This converts stars to extra lives! The price is subject to inflation though... Don't overuse it."));
        
        quipWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameIsRunning && Input.GetKeyDown(KeyCode.Alpha1))
        {
            resumeGame();
        }
    }

    void pauseGame()
    {
        gameIsRunning = false;
        Time.timeScale = 0;
    }

    void resumeGame()
    {
        quipWindow.SetActive(false);
        gameIsRunning = true;
        Time.timeScale = currentTimeScale;
    }

    public void startQuip(string itemName)
    {
        quipWindow.SetActive(true);
        if (items.TryGetValue(itemName, out Item item))
        {
            quipWindowText.text = item.ItemText;
            quipWindowSprite.sprite = item.ItemSprite;
        }
        pauseGame();
    }
}
