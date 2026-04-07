using UnityEngine;
using System.Collections.Generic;
using System;


public class MousePosition : MonoBehaviour
{
    public Transform HowerBox;
    public Transform Player;
    public GameObject TestTile;
    public GameObject EmptyCropTest;

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

    // Update is called once per frame
    void Update()
    {
        
        if (building || placing){
            // Get mouse position in world space (what you want for 2D games)
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f; // always set z to 0 for 2D

            float mouseX = Mathf.FloorToInt(mousePos.x) + 0.5f;
            float mouseY = Mathf.FloorToInt(mousePos.y) + 0.5f;

            if (building)
            {
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
                        _shelfInventory.AddItem(placingID,placingName,placingFacingsTaking);
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
}
