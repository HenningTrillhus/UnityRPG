using UnityEngine;
using System.Collections.Generic;


public class ShelfInventory : MonoBehaviour
{
    [Header("Script Refrence")]
    public ShelfInventoryDisplay _shelfInventoryDisplay;
    public Player_Inventory playerInventory;

    [Header("Shelf Item Sprites")]
    public Sprite CarrotSprite;
    public Sprite AppleSprite;

    public int shelfCapacity = 20;
    private int currentInventoryAmount = 0;
    private string itemName;
    private int itemID;

    public class Item
    {
        public int _ItemId;
        public string _ItemName;
        public int _ItemSpacings;
        public int _ItemValue;
    }

    public List<Item> ItemsInShelf = new List<Item>();

    [Header("ItemPlaceHolders")]
    [SerializeField] public GameObject[] itemPlaceHolders;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (GameObject item in itemPlaceHolders)
        {
            item.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(int id, string name, int numberOfFacings, int valueOfItem)
    {
        if (currentInventoryAmount < shelfCapacity){
            if (playerInventory.inventoryV2.CheckForItemUsingName(name) > 0)
            {
                playerInventory.inventoryV2.RemoveItemV2(name, 1);
                Debug.Log("Adding " + name + " to the shelf");
                ItemsInShelf.Add(new Item{_ItemId = id, _ItemName = name, _ItemSpacings = numberOfFacings, _ItemValue = valueOfItem});
                itemPlaceHolders[currentInventoryAmount].GetComponent<SpriteRenderer>().sprite = _shelfInventoryDisplay.getSpriteByID(id);
                itemPlaceHolders[currentInventoryAmount].SetActive(true);
                currentInventoryAmount++;
                _shelfInventoryDisplay.UpdateHoldingList(name,id, valueOfItem);
            }
            else
            {
                Debug.Log("Dont have enogh of the " + name);
            }
        }
        else
        {
            Debug.Log("Shelf full");
        }
        
        
    }
    public void RemoveItem()
    {
        if (currentInventoryAmount > 0)
        {
            currentInventoryAmount--;
            playerInventory.inventoryV2.AddItemV2(ItemsInShelf[currentInventoryAmount]._ItemName, 1);
            ItemsInShelf.RemoveAt(currentInventoryAmount);
            itemPlaceHolders[currentInventoryAmount].SetActive(false);
        }
        else
        {
            Debug.Log("Shelf empty");
        }
        
    }
}
