using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ShelfInventoryDisplay : MonoBehaviour
{
    [Header("Script Refrence")]
    public ShelfInventory _shelfInventory;

    public Transform ShelfBoxParent;
    public GameObject ShelfItemTextBar;

    private bool found;
    private int ListLenght = 0;
    
    public class Item
    {
        public string _ItemName;
        public int _ItemAmount;
    }

    public List<Item> ItemsInShelfForDisplay = new List<Item>();
    public List<GameObject> ShelfTextBars = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //transform.position = new Vector3(transform.position.x + 1f, transform.position.y+ 1f, transform.position.z);
        /*SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.size = new Vector2(sr.size.x, 1.5f); // change height without stretching*/
        
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void UpdateItemTextBars()
    {
        int x = ShelfTextBars.Count;
        foreach (GameObject textBar in ShelfTextBars)
        {
            textBar.transform.localPosition = new Vector3(0, -0.3f + (x * 0.3f), 0);
            x --;
        }
    }

    void UpdateItemDisplay()
    {
        //If number of items in shelf has changed then run.
        if (ItemsInShelfForDisplay.Count != ListLenght)
        {
            //Update var to match
            ListLenght = ItemsInShelfForDisplay.Count;

            //Creat new shelftextbar then add to list 
            GameObject _ShelfTextBar = Instantiate(ShelfItemTextBar, transform.position, Quaternion.identity, ShelfBoxParent);
            ShelfTextBars.Add(_ShelfTextBar);

            //Render parant and change heigh and move up
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.size = new Vector2(sr.size.x, (0.5f + (0.3f * ListLenght))); 
            transform.position = new Vector3(transform.position.x, transform.position.y + (0.075f), transform.position.z);
            UpdateItemTextBars();
        }
    }

    public void UpdateHoldingList(string Name)
    {
        found = false;
        if (ItemsInShelfForDisplay.Count != 0)
        {
            Debug.Log("running");
            foreach (Item item in ItemsInShelfForDisplay)
            {
                if (Name == item._ItemName)
                {
                    
                    item._ItemAmount += 1;
                    found = true;
                    Debug.Log("Found " + Name + " Change amount to " + item._ItemAmount);
                    break;
                }
            }
            if (found == false)
            {
                Debug.Log("Adding " + Name + " to list in Display");
                ItemsInShelfForDisplay.Add(new Item {_ItemName = Name, _ItemAmount = 1});
            }
            
        }
        else
        {
            Debug.Log("Adding " + Name + " to list in Display");
            ItemsInShelfForDisplay.Add(new Item {_ItemName = Name, _ItemAmount = 1});
        }
        UpdateItemDisplay();
    }
}
