using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class ShelfInventoryDisplay : MonoBehaviour
{
    [Header("Script Refrence")]
    public ShelfInventory _shelfInventory;

    [Header("GameObjects Refrence")]
    public Transform ShelfBoxParent;
    public GameObject ShelfItemTextBar;
    public GameObject SumTextBox;

    [Header("Sprite Refrence")]
    [SerializeField]public Sprite CarrotSprite;
    [SerializeField]public Sprite AppleSprite;
    [SerializeField]public Sprite PotatoSprite;
    [SerializeField]public Sprite BreadSprite;
    [SerializeField]public Sprite RedMushroomSprite;
    [SerializeField]public Sprite BrownMushroomSprite;
    [SerializeField]public Sprite DiamondSprite;

    private bool found;
    private int ListLenght = 0;

    private float TotalSumLockedYCord;
    
    public class Item
    {
        public int _ItemID;
        public string _ItemName;
        public int _ItemAmount;
        public int _ItemValue;
    }

    public List<Item> ItemsInShelfForDisplay = new List<Item>();
    public List<GameObject> ShelfTextBars = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float TotalSumLockedYCord = SumTextBox.transform.position.y;
        //transform.position = new Vector3(transform.position.x + 1f, transform.position.y+ 1f, transform.position.z);
        /*SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.size = new Vector2(sr.size.x, 1.5f); // change height without stretching*/
        
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void UpdateSpirteAndTextInBars()
    {
        //foreach (GameObject textBar in ShelfTextBars);
    }

    public Sprite getSpriteByID(int id)
    {
        if (id == 7)
        {
            return BreadSprite;
        }
        if (id == 12)
        {
            return CarrotSprite;
        }
        if (id == 13)
        {
            return AppleSprite;
        }
        if (id == 14)
        {
            return PotatoSprite;
        }
        if (id == 15)
        {
            return RedMushroomSprite;
        }
        if (id == 16)
        {
            return BrownMushroomSprite;
        }
        if (id == 17)
        {
            return DiamondSprite;
        }
        else
        {
            Debug.Log("fuck balls, dont have the spirt for this id. id sendt in: " + id);
            return null;
        }
    }

    void UpdateItemTextBars()
    {
        int i = 0;
        int x = ShelfTextBars.Count;
        foreach (GameObject textBar in ShelfTextBars)
        {
            textBar.transform.localPosition = new Vector3(0, -0.3f + (x * 0.3f), 0);
            x --;
            TMP_Text amountText = textBar.transform.Find("ShelfAmountOfItem").GetComponent<TMP_Text>();
            TMP_Text sumText = textBar.transform.Find("ShelfItemSum").GetComponent<TMP_Text>();


            SpriteRenderer sr = textBar.transform.Find("SpritePlaceHolder").GetComponent<SpriteRenderer>();
            sr.sprite = getSpriteByID(ItemsInShelfForDisplay[i]._ItemID);

            amountText.text = (ItemsInShelfForDisplay[i]._ItemAmount).ToString();
            sumText.text = ((ItemsInShelfForDisplay[i]._ItemAmount) * (ItemsInShelfForDisplay[i]._ItemValue)).ToString();
            i++;
        }
        SumTextBox.transform.localPosition = new Vector3(
        SumTextBox.transform.localPosition.x,
        SumTextBox.transform.localPosition.y - 0.15f,
        SumTextBox.transform.localPosition.z
        );
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
            sr.size = new Vector2(sr.size.x, (sr.size.y + 0.3f)); 
            transform.position = new Vector3(transform.position.x, transform.position.y + (0.3f), transform.position.z);
            
        }
        UpdateItemTextBars();
    }

    public void UpdateHoldingList(string Name, int ID, int Value)
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
                ItemsInShelfForDisplay.Add(new Item {_ItemName = Name, _ItemID = ID, _ItemAmount = 1, _ItemValue = Value});
            }
            
        }
        else
        {
            Debug.Log("Adding " + Name + " to list in Display");
            ItemsInShelfForDisplay.Add(new Item {_ItemName = Name, _ItemID = ID, _ItemAmount = 1, _ItemValue = Value});
        }
        UpdateItemDisplay();
    }
}
