using UnityEngine;
using System.Collections.Generic;


public class ShelfInventory : MonoBehaviour
{
    [Header("Script Refrence")]
    public ShelfInventoryDisplay _shelfInventoryDisplay;
    private InventoryV2 inventory;
    [SerializeField] private MousePosition mousePosition;

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
    [SerializeField] private GameObject[] itemPlaceHolders;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //inventory = FindObjectOfType<InventoryV2>();
        foreach (GameObject item in itemPlaceHolders)
        {
            item.SetActive(false);
        }
        Player_Inventory.Instance.Test();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            // Only react if THIS object was hit
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                OnPressed();
            }
        }
    }

    private void OnPressed()
    {
        Debug.Log($"{gameObject.name} was clicked!");
        
        var info = MousePosition.Instance.getNeededInfoForShelf();

        Debug.Log(info.ID);
        Debug.Log(info.Name);
        Debug.Log(info.NumberOfFacings);
        Debug.Log(info.ValueOfItem);
        AddItem(info.ID, info.Name, info.NumberOfFacings, info.ValueOfItem);
    }

    private void AddItem(int id, string name, int numberOfFacings, int valueOfItem)
    {
        if (currentInventoryAmount < shelfCapacity){
            if (inventory.CheckForItemUsingName(name) > 0)
            {
                inventory.RemoveItemV2(name, 1);
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
            inventory.AddItemV2(ItemsInShelf[currentInventoryAmount]._ItemName, 1);
            ItemsInShelf.RemoveAt(currentInventoryAmount);
            itemPlaceHolders[currentInventoryAmount].SetActive(false);
        }
        else
        {
            Debug.Log("Shelf empty");
        }
        
    }
}
