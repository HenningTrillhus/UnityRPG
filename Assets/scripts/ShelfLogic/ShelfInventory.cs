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
    private string itemName;
    private int itemID;

    public int ShelfID;

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
        ShelfID = ShelfInventoryManager.Instance.GetShelfIDByPosition(transform.position);
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
                Debug.Log("Clicked on shelf with ID: " + ShelfID);
                OnPressedLeft();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            // Only react if THIS object was hit
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                OnPressedRight();
            }
        }
    }

    private void OnPressedLeft()
    {
        var info = MousePosition.Instance.getNeededInfoForShelf();

        Debug.Log(info.ID);
        Debug.Log(info.Name);
        Debug.Log(info.NumberOfFacings);
        Debug.Log(info.ValueOfItem);
        AddItem(info.ID, info.Name, info.NumberOfFacings, info.ValueOfItem);
    }

    private void OnPressedRight()
    {
        RemoveItem();
    }

    private void AddItem(int id, string name, int numberOfFacings, int valueOfItem)
    {
        if (ItemsInShelf.Count < shelfCapacity){
            if (Player_Inventory.Inventory.CheckForItemUsingName(name) > 0)
            {
                Player_Inventory.Inventory.RemoveItemV2(name, 1);
                Debug.Log("Adding " + name + " to the shelf");
                ItemsInShelf.Add(new Item{_ItemId = id, _ItemName = name, _ItemSpacings = numberOfFacings, _ItemValue = valueOfItem});
                itemPlaceHolders[ItemsInShelf.Count - 1].GetComponent<SpriteRenderer>().sprite = _shelfInventoryDisplay.getSpriteByID(id);
                itemPlaceHolders[ItemsInShelf.Count - 1].SetActive(true);
                _shelfInventoryDisplay.UpdateHoldingList(name,id, valueOfItem);
                ShelfInventoryManager.Instance.addItemToShelf(ShelfID, name);
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
        if (ItemsInShelf.Count > 0)
        {
            Player_Inventory.Inventory.AddItemV2(ItemsInShelf[ItemsInShelf.Count - 1]._ItemName, 1);
            
            
            itemPlaceHolders[ItemsInShelf.Count -1 ].SetActive(false);
            Debug.Log("Removing " + ItemsInShelf[ItemsInShelf.Count-1]._ItemName + " from the shelf");
            ShelfInventoryManager.Instance.removeItemFromShelf(ShelfID, ItemsInShelf[ItemsInShelf.Count - 1]._ItemName);
            ItemsInShelf.RemoveAt(ItemsInShelf.Count - 1);
            
            _shelfInventoryDisplay.UpdateHoldingListRemove(name);
        }
        else
        {
            Debug.Log("Shelf empty");
        }
        
    }
}
