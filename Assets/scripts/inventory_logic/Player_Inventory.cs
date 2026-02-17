using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    public ItemData woodItem; // drag your Wood asset here in Inspector
    public ItemData stoneItem; // drag your Stone asset here in Inspector

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
            //inventory.AddItem(stoneItem, 3); // adds 3 Stone
            inventoryV2.AddItemV2(woodItem,60); // adds 1 item to the new inventory
            //inventoryV2.RemoveItemV2(woodItem, 1); // removes 1 item from the new inventory
            //PrintInventory();
        }
        if (Input.GetKeyDown(KeyCode.H))
        { 
            //inventory.AddItem(stoneItem, 3); // adds 3 Stone
            //inventoryV2.AddItemV2(woodItem,60); // adds 1 item to the new inventory
            inventoryV2.RemoveItemV2(woodItem, 5); // removes 1 item from the new inventory
            //PrintInventory();
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
