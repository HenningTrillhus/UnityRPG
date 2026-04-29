using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    [Header("Items")]
    public ItemData woodItem; 
    public ItemData stoneItem; 
    public ItemData Fiber; 
    public ItemData IronOre; 
    public ItemData CopperOre; 
    public ItemData AldricWoodrowsAks; 
    public ItemData Bread; 
    public ItemData Berry; 
    public ItemData RustyDagger; 
    public ItemData TestHoe;
    public ItemData CarrotSeed;
    public ItemData WaterBucket;
    public ItemData Carrot;
    public ItemData Apple;
    public ItemData Potato;
    public ItemData RedMushroom;
    public ItemData BrownMushroom;
    public ItemData Diamond;

    [Header("Script Ref")]
    public Inventory inventory; // this is the logic object
    //public InventoryV2 inventoryV2; // this is the new logic object
    public static InventoryV2 Inventory { get; private set; }
    public InventoryUI2 inventoryUI2; 


    

    void Awake()
    {
        //inventory = new Inventory(10); // 10 slots to start
        //inventoryV2 = new InventoryV2(inventoryUI2); // initialize the new inventory
        Inventory = new InventoryV2(inventoryUI2);

        //creating the total invenvotry respotetory inside the the invevntory code
        Player_Inventory.Inventory.CreatTotalItemList(woodItem);
        Player_Inventory.Inventory.CreatTotalItemList(stoneItem);
        Player_Inventory.Inventory.CreatTotalItemList(Fiber);
        Player_Inventory.Inventory.CreatTotalItemList(CopperOre);
        Player_Inventory.Inventory.CreatTotalItemList(IronOre);
        Player_Inventory.Inventory.CreatTotalItemList(AldricWoodrowsAks);
        Player_Inventory.Inventory.CreatTotalItemList(Bread);
        Player_Inventory.Inventory.CreatTotalItemList(Berry);
        Player_Inventory.Inventory.CreatTotalItemList(RustyDagger);
        Player_Inventory.Inventory.CreatTotalItemList(TestHoe);
        Player_Inventory.Inventory.CreatTotalItemList(CarrotSeed);
        Player_Inventory.Inventory.CreatTotalItemList(WaterBucket);
        Player_Inventory.Inventory.CreatTotalItemList(Carrot);
        Player_Inventory.Inventory.CreatTotalItemList(Apple);
        Player_Inventory.Inventory.CreatTotalItemList(Potato);
        Player_Inventory.Inventory.CreatTotalItemList(RedMushroom);
        Player_Inventory.Inventory.CreatTotalItemList(BrownMushroom);
        Player_Inventory.Inventory.CreatTotalItemList(Diamond);
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.AddItem(woodItem, 5); // adds 5 Wood
            //PrintInventory();
        }*/
        if (Input.GetKeyDown(KeyCode.G))
        {
            Player_Inventory.Inventory.AddItemV2("Apple",5); // adds 1 item to the new inventory
            Player_Inventory.Inventory.AddItemV2("Carrot",5); // adds 1 item to the new inventory
            Player_Inventory.Inventory.AddItemV2("Red Mushroom",5); // adds 1 item to the new inventory
            Player_Inventory.Inventory.AddItemV2("Brown Mushroom",5); // adds 1 item to the new inventory
            Player_Inventory.Inventory.AddItemV2("Bread",5); // adds 1 item to the new inventory
            Player_Inventory.Inventory.AddItemV2("Potato",5); // adds 1 item to the new inventory
            Player_Inventory.Inventory.AddItemV2("Diamond",5); // adds 1 item to the new inventory
        }
        if (Input.GetKeyDown(KeyCode.H))
        { 
            Player_Inventory.Inventory.RemoveItemV2("Wood Log", 5); // removes 1 item from the new inventory
        }
        if (Input.GetKeyDown(KeyCode.F))
        { 
            Player_Inventory.Inventory.AddItemV2("Bread",1);
            Player_Inventory.Inventory.AddItemV2("Stone Brick",1);
            Player_Inventory.Inventory.AddItemV2("Iron Ore",1);
            Player_Inventory.Inventory.AddItemV2("Copper Ore",1);
            Player_Inventory.Inventory.AddItemV2("Fiber",1);
        }
        if (Input.GetKeyDown(KeyCode.I))
        { 
            Player_Inventory.Inventory.AddItemV2("Test Hoe",1);
            Player_Inventory.Inventory.AddItemV2("Water Bucket",1);
            //Player_Inventory.Inventory.AddItemV2(IronOre, 5); // removes 1 item from the new inventory
        }
        if (Input.GetKeyDown(KeyCode.C))
        { 
            Player_Inventory.Inventory.AddItemV2("Carrot Seed", 5); // removes 1 item from the new inventory
        }
        if (Input.GetKeyDown(KeyCode.J))
        { 
            Player_Inventory.Inventory.logInv();
        }
        if (Input.GetKeyDown(KeyCode.B))
        { 
            Player_Inventory.Inventory.AddItemV2("Bread",5);
        }
    }

    void PrintInventory()
    {

        int i = 0;
        foreach (var slot in inventory.Slots)
        {
            if (slot.IsEmpty)
                Debug.Log($"Slot {i}: Empty");
            else
                Debug.Log($"Slot {i}: {slot.item.itemName} x{slot.amount}");
            i++;
        }
    }
}
