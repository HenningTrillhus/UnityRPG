using UnityEngine;
using System.Collections.Generic;
using System;


public class MousePosition : MonoBehaviour
{
    public Transform HowerBox;
    public Transform Player;
    
    public GameObject BuildMenu;
    public StoreAreaLogic Area;

    [Header("Prefabs")]
    public GameObject TestTile;
    public GameObject EmptyCropTest;
    public GameObject Shelf1Prefab;

    [Header("ShelfSprites")]
    public Sprite Shelf1Sprite;

    public CropDisplayInfo cropDisplayInfo;
    public ShelfInventory _shelfInventory;

    public float ReachRangeForHowerBox = 4f;

    public bool howerBoxVisible = true;
    public bool building = false;
    public bool placing = false;
    public string buildingBlock;
    public int placingID;
    public string placingName;
    public int placingFacingsTaking;
    public int placingValue;

    private float previousX;
    private float previousY;
    private string colorOfHoverBox;
    private SpriteRenderer spriteRenderer;
    

    public class POS
    {
        public float x;
        public float y;
        public bool SeedAble;
    }

    public class CropPos
    {
        public float x;
        public float y;
    }

    public class ShelfPos
    {
        public float x;
        public float y;
    }

    public List<POS> CordsInUse = new List<POS>();
    public List<CropPos> cropPositions = new List<CropPos>();
    public List<ShelfPos> ShelfPositions = new List<ShelfPos>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShelfPositions.Add(new ShelfPos{x = -6.5f, y = 7.5f});
    }

    public string checkHoverBoxAcceptance(float x, float y)
    {
        if (placing && !building)
        {
            foreach (ShelfPos cord in ShelfPositions)
            {
                if (cord.x == x && cord.y == y)return "Green";
            }
            return "Red";
        }
        else
        {
            return "Red";
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get mouse position in world space (what you want for 2D games)
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // always set z to 0 for 2D

        float mouseX = Mathf.FloorToInt(mousePos.x) + 0.5f;
        float mouseY = Mathf.FloorToInt(mousePos.y) + 0.5f;
        if (mouseX != previousX || mouseY != previousY)
        {
            previousX = mouseX;
            previousY = mouseY;
            colorOfHoverBox = checkHoverBoxAcceptance(previousX, previousY);
            if (colorOfHoverBox == "Green")
            {
                SpriteRenderer sr = GetComponent<SpriteRenderer>();
                GetComponent<SpriteRenderer>().color = Color.green;
                Color color = sr.color;
                color.a = 0.3f; // 0 = invisible, 1 = fully visible
                sr.color = color;
            }
            if (colorOfHoverBox == "Red")
            {
                SpriteRenderer sr = GetComponent<SpriteRenderer>();
                GetComponent<SpriteRenderer>().color = Color.red;
                Color color = sr.color;
                color.a = 0.3f; // 0 = invisible, 1 = fully visible
                sr.color = color;
            }
            
        }
        
        if (building || placing){
            if (Vector2.Distance(Player.position, mousePos) < ReachRangeForHowerBox)
            {
                HowerBox.transform.position = new Vector3(mouseX, mouseY, 0f);
                HowerBox.GetComponent<SpriteRenderer>().enabled = true;
                howerBoxVisible = true;

            }
            else{
                HowerBox.GetComponent<SpriteRenderer>().enabled = false;
                howerBoxVisible = false;
            }

            if (building)
            {
                if (Input.GetMouseButtonDown(0) && howerBoxVisible && building)
                {
                    if (buildingBlock == "EmptyCrop"){
                        if (!IsSlotTaken(mouseX, mouseY, false, false))
                        {
                            Instantiate(EmptyCropTest, new Vector3(mouseX, mouseY, 0f), Quaternion.identity);
                            CordsInUse.Add(new POS {x = mouseX, y = mouseY, SeedAble = true});
                            cropPositions.Add(new CropPos{x = mouseX, y = mouseY});
                            CropDisplayInfo.TriggerOnNewCropPlanted();
                        }
                        else
                        {
                            Debug.Log("Pos in use");
                        } 
                    }
                    if (buildingBlock == "CorrotSeed")
                    {
                        if (IsSlotTaken(mouseX, mouseY, true, false))
                        {
                            Debug.Log("Crop avable to seed");
                        }
                        else
                        {
                            Debug.Log("no crop there boy");
                        }
                    }
                    if (buildingBlock == "WaterBucket")
                    {
                        Debug.Log("good form mouse");
                        if (IsSlotTaken(mouseX, mouseY, true, false))
                        {
                            Debug.Log("Crop avable to Water");
                        }
                        else
                        {
                            Debug.Log("no crop there boy");
                        }
                    }
                    if (buildingBlock == "Shelf1")
                    {
                        if (!IsSlotTaken(mouseX, mouseY, false, true))
                        {
                            Area.addObstacle("Shelf", mouseX, mouseY);
                            ShelfPositions.Add(new ShelfPos{x = mouseX, y = mouseY});
                            //creating the prefab of the shelf and place it in the world to the cord of the mouse
                            Instantiate(Shelf1Prefab, new Vector3(mouseX, mouseY, 0f), Quaternion.identity);
                        }
                        else
                        {
                            Debug.Log("Pos in use");
                        } 
                    }
                    /*else
                    {
                        Debug.Log("No Building block");
                    }*/
                    
                }
                if (Input.GetMouseButtonDown(1))
                {
                    building = false;
                    buildingBlock = "";
                }
            }
            if (placing)
            {
                

                if (Input.GetMouseButtonDown(0))
                {
                    if (IsSlotTaken(mouseX, mouseY, false, true))
                    {
                        Debug.Log("Adding item to shelf");
                        _shelfInventory.AddItem(placingID,placingName,placingFacingsTaking, placingValue);
                    }
                    else
                    {
                        Debug.Log("no Shelf her lil bro");
                    }
                }
                if (Input.GetMouseButtonDown(1))
                {
                    if (IsSlotTaken(mouseX, mouseY, false, true))
                    {
                        Debug.Log("Removing item to shelf");
                        _shelfInventory.RemoveItem();
                    }
                    else
                    {
                        Debug.Log("no Shelf her lil bro");
                    }
                }
                if (Input.GetMouseButtonDown(1) && !IsSlotTaken(mouseX, mouseY, false, true))
                {
                    placing = false;
                    placingName = "";
                    placingFacingsTaking = 1;
                    placingID = 0;
                }                
            }
            
        }
    }

    bool IsSlotTaken(float checkX, float checkY, bool checkForSeedSlot, bool checkForShelf)
    {
        if (!checkForSeedSlot && !checkForShelf){
            foreach (POS cord in CordsInUse)
            {
                if (cord.x == checkX && cord.y == checkY)return true;
            }
            return false;
        }
        if (!checkForSeedSlot && checkForShelf)
        {
            foreach (ShelfPos Pos in ShelfPositions)
            {
                if (Pos.x == checkX && Pos.y == checkY)return true;
            }
            return false;
        }
        else 
        {
            foreach (POS cord in CordsInUse)
            {
                if (cord.x == checkX && cord.y == checkY && cord.SeedAble)return true;
            }
            return false;
        }
        
    }

    public void BuildWithShelf1()
    {
        BuildMenu.SetActive(false);
        Debug.Log("Build with shelf 1");
        building = true;
        buildingBlock = "Shelf1";
        placing = false;
        spriteRenderer = HowerBox.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Shelf1Sprite;
    }
}
