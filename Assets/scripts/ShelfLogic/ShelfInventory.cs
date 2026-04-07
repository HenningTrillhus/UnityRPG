using UnityEngine;
using System.Collections.Generic;


public class ShelfInventory : MonoBehaviour
{
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
    public Sprite getSpriteByID(int id)
    {
        if (id == 12)
        {
            return CarrotSprite;
        }
        if (id == 13)
        {
            return AppleSprite;
        }
        else
        {
            Debug.Log("fuck balls, dont have the spirt for this id. id sendt in: " + id);
            return null;
        }
    }

    public void AddItem(int id, string name, int numberOfFacings)
    {
        if (currentInventoryAmount < shelfCapacity){
            if (playerInventory.inventoryV2.CheckForItemUsingName(name) > 0)
            {
                playerInventory.inventoryV2.RemoveItemV2(name, 1);
                Debug.Log("Adding " + name + " to the shelf");
                ItemsInShelf.Add(new Item{_ItemId = id, _ItemName = name, _ItemSpacings = numberOfFacings});
                itemPlaceHolders[currentInventoryAmount].GetComponent<SpriteRenderer>().sprite = getSpriteByID(id);
                itemPlaceHolders[currentInventoryAmount].SetActive(true);
                currentInventoryAmount++;
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
