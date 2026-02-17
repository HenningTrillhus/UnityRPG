using UnityEngine;
using System.Collections.Generic;
using System.Linq;

//NO MONO so dont connect to any game object, public logic for new better inventory 
public class InventoryV2
{

    private Player_Inventory player_inventory; //reference to the old inventory to get the capacity, this is really bad but i dont know how else to do it without making a new class for the player inventory that both inventories can reference, or making the inventoryV2 a monobehaviour and connect it to the player inventory game object, which i also dont want to do because then it would be more work to connect it to the player inventory game object and also make it less flexible if i want to use it for other inventories like chest or something.
    //(will be saved inside inventory) /will only be saved in unity item: 
    // Rule of tumb(i think...) save all data that will be uses in backend logic in inventory
    //(Name), (Type), (Tool), (Rarity), (Quantity), (ValueInGameCurrency) /max Stack, /Stackeble (!fuck this just save all!)
    public string Name;
    public string Type;
    public bool IsTool;
    public string Rarity;
    public int Quantity;
    public int ValueInGameCurrency;
    public int MaxStack;
    public bool Stackable;
    public int stack; //how many max loads of the item
    public bool stackFull;
    

    public int inventoryCapacity = 20;

    List<InventoryV2> NewInventory = new List<InventoryV2>();


    public void AddItemV2(ItemData item, int amount = 1)
    {
        //if inventory is empty just add to list
        if (!NewInventory.Any())
        {
            Debug.Log("inventory is empty, adding new item");
            NewInventory.Add(new InventoryV2 { Name = item.itemName, Quantity = amount, Type = item.itemType, IsTool = item.itemIsTool, Rarity = item.rarity, ValueInGameCurrency = item.valueInGameCurrency, MaxStack = item.maxStack, Stackable = item.stackable, stack = 1, stackFull = false});
        }
        else
        {
            //check if item is stackable
            if (item.stackable)
            {
                //go trough all items in inventory that are stackable
                foreach (InventoryV2 itemInInventory in NewInventory)
                {
                    //check for mathing name, checks if stack is full, if it is then we skip it
                    if (itemInInventory.Name == item.itemName && itemInInventory.stackFull == false)
                    {
                        //pluss the old amount pluss the new to check if the amount bypass the max, then creat new stack
                        int quantityLeftOver = (itemInInventory.Quantity + amount) - itemInInventory.MaxStack;
                        if (quantityLeftOver > 0)
                        {
                            //if bypass set quantity to max and mark as full stac
                            //also make sure its above 0 if just max stack still needs tro be able to remove from it, so only set to fullstack if it has more stacks
                            itemInInventory.Quantity = itemInInventory.MaxStack;
                            itemInInventory.stackFull = true;

                            //creat new stack now with stack pluss one
                            Debug.Log("bypass max, creat new stack of the new item with the left over amount");
                            NewInventory.Add(new InventoryV2 { Name = item.itemName, Quantity = quantityLeftOver, Type = item.itemType, IsTool = item.itemIsTool, Rarity = item.rarity, ValueInGameCurrency = item.valueInGameCurrency, MaxStack = item.maxStack, Stackable = item.stackable, stack = itemInInventory.stack+1, stackFull = false});
                            return;
                        }
                        else
                        {
                            //if not bypass just add the amount to the stack
                            itemInInventory.Quantity += amount;
                        }
                        return;
                    }
                    //if stack is full, skip to next stack.
                    if (itemInInventory.Name == item.itemName && itemInInventory.stackFull == true)
                    {
                        continue;
                    }
                    else
                    {
                        //if no match creat new stack of the new item
                        Debug.Log("no match, creat new stack of the new item");
                        NewInventory.Add(new InventoryV2 { Name = item.itemName, Quantity = amount, Type = item.itemType, IsTool = item.itemIsTool, Rarity = item.rarity, ValueInGameCurrency = item.valueInGameCurrency, MaxStack = item.maxStack, Stackable = item.stackable, stack = 1, stackFull = false});
                    }
                    
                }
                return; 

            }
            //if item is not stackable
            else
            {
                //wtf
            }
        }
        
        
    }

    public void RemoveItemV2(ItemData item, int amount = 1)
    {
        //go trough all items in inventory find name
        foreach (InventoryV2 itemInInventory in NewInventory)
        {
            //check for match in names and if stack is full
            if (itemInInventory.Name == item.itemName && itemInInventory.stackFull == false)
            {

                //if stack has enough items just remove the amount.
                if (itemInInventory.Quantity >= amount)
                {
                    itemInInventory.Quantity -= amount;

                    //if the amount leads to 0 remove the whole item form the inventory.
                    if (itemInInventory.Quantity == 0)
                    {
                        Debug.Log("removed item becuse item is now just 0 nothing more or less");
                        NewInventory.Remove(itemInInventory);
                        //clean up the slots and position in the inventory after change
                        FixChanges();
                    }
                    return;
                }
                //if the amount is higher then quantity but has multiple stackes
                if (itemInInventory.Quantity < amount && itemInInventory.stack > 1){
                    Debug.Log("removing this stack but still has leftover, moves to next");
                    //finds the left over and makes it a positiv number. 
                    int LeftOverAmount = ((itemInInventory.Quantity - amount) * -1);
                    //get the stack number we search for that would be the next stack to unpack.
                    int stackNumber = (itemInInventory.stack -1);

                    //remove the empty stack
                    NewInventory.Remove(itemInInventory);

                    //run trough again to find the next stack in the inventory.
                    foreach (InventoryV2 itemInInventoryR2 in NewInventory)
                    {
                        //check for the name and then if the stack number checks out
                        if (itemInInventoryR2.Name == item.itemName && itemInInventoryR2.stack == stackNumber){
                            //check if amount dont bypass the full stack, i will try to make this imposible
                            if (itemInInventoryR2.Quantity < LeftOverAmount)
                            {
                                Debug.Log("nahhh wtf bro");
                                //mabey now run whole funtion again.
                            }
                            else
                            {
                                
                                //remove the leftover from the new stack and set it to not fullstack any more.
                                itemInInventoryR2.Quantity -= LeftOverAmount;
                                itemInInventoryR2.stackFull = false;

                                Debug.Log("removing from the next stack now, left with " + itemInInventoryR2.Quantity + " in this stack");
                                return;
                            }
                        }
                    }
                    break;
                }
                else{
                    //if amount higher then full amount of item
                    Debug.Log("you broke ass, you dont have enough iteams to do that bitch ass.");
                }
            }
            Debug.Log("Removing left with" + itemInInventory.Quantity);
        }
        
    }

    public void FixChanges()
    {
        
    }
}


