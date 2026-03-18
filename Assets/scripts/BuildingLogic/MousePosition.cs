using UnityEngine;
using System.Collections.Generic;


public class MousePosition : MonoBehaviour
{
    public Transform HowerBox;
    public Transform Player;
    public GameObject TestTile;
    public GameObject EmptyCropTest;

    public float ReachRangeForHowerBox = 4f;

    public bool howerBoxVisible = true;
    public bool building = false;
    public string buildingBlock;

    public class POS
    {
        public float x;
        public float y;
        public bool SeedAble;
    }

    public List<POS> CordsInUse = new List<POS>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (building){
            // Get mouse position in world space (what you want for 2D games)
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f; // always set z to 0 for 2D

            float mouseX = Mathf.FloorToInt(mousePos.x) + 0.5f;
            float mouseY = Mathf.FloorToInt(mousePos.y) + 0.5f;

            
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
                    if (!IsSlotTaken(mouseX, mouseY, false))
                    {
                        Instantiate(EmptyCropTest, new Vector3(mouseX, mouseY, 0f), Quaternion.identity);
                        CordsInUse.Add(new POS {x = mouseX, y = mouseY, SeedAble = true});
                    }
                    else
                    {
                        Debug.Log("Pos in use");
                    } 
                }
                if (buildingBlock == "CorrotSeed")
                {
                    if (IsSlotTaken(mouseX, mouseY, true))
                    {
                        Debug.Log("Crop avable to seed");
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
        
    }

    bool IsSlotTaken(float checkX, float checkY, bool checkForSeedSlot)
    {
        if (!checkForSeedSlot){
            foreach (POS cord in CordsInUse)
            {
                if (cord.x == checkX && cord.y == checkY)return true;
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
