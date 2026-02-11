using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    public ItemData woodItem; // drag your Wood asset here in Inspector
    public ItemData stoneItem; // drag your Stone asset here in Inspector

    public Inventory inventory; // this is the logic object

    void Awake()
    {
        inventory = new Inventory(10); // 10 slots to start
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
            inventory.AddItem(stoneItem, 3); // adds 3 Stone
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
