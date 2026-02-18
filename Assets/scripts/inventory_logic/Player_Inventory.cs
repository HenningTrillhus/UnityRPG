using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    public ItemData woodItem; // drag your Wood asset here in Inspector
    public ItemData stoneItem; // drag your Stone asset here in Inspector
    public ItemData Fiber; // drag your Wood asset here in Inspector
    public ItemData IronOre; // drag your Stone asset here in Inspector
    public ItemData CopperOre; // drag your Stone asset here in Inspector

    public Inventory inventory; // this is the logic object
    public InventoryV2 inventoryV2; // this is the new logic object


    

    void Awake()
    {
        inventory = new Inventory(10); // 10 slots to start
        inventoryV2 = new InventoryV2(); // initialize the new inventory
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
            inventoryV2.AddItemV2(woodItem,25); // adds 1 item to the new inventory
        }
        if (Input.GetKeyDown(KeyCode.H))
        { 
            inventoryV2.RemoveItemV2(woodItem, 5); // removes 1 item from the new inventory
        }
        if (Input.GetKeyDown(KeyCode.F))
        { 
            inventoryV2.AddItemV2(Fiber, 5); // removes 1 item from the new inventory
        }
        if (Input.GetKeyDown(KeyCode.I))
        { 
            inventoryV2.AddItemV2(IronOre, 5); // removes 1 item from the new inventory
        }
        if (Input.GetKeyDown(KeyCode.C))
        { 
            inventoryV2.AddItemV2(CopperOre, 5); // removes 1 item from the new inventory
        }
        if (Input.GetKeyDown(KeyCode.J))
        { 
            inventoryV2.logInv();
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
