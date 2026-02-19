using UnityEngine;
using System.Collections.Generic;
using System.Linq;

//NO MONO so dont connect to any game object, public logic for new better inventory 
public class InventoryV2
{
    //public InventoryUI2 inventoryUI; //reference to the new inventory ui to update the ui when changes are made, this is really bad but i dont know how else to do it without making a new class for the player inventory that both inventories can reference, or making the inventoryV2 a monobehaviour and connect it to the player inventory game object, which i also dont want to do because then it would be more work to connect it to the player inventory game object and also make it less flexible if i want to use it for other inventories like chest or something.

    //refrensing to the UI Code 
    /*private InventoryUI2 UILogic;

    public InventoryV2(InventoryUI2 InventoryUI2Ref){
        UILogic = InventoryUI2Ref;
    }*/

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
        int stackRotation = 1; //start at stack number one if that is full go to next

        bool ItemExistsInInv = NewInventory.Any(i => i.Name == item.itemName);

        //if inventory is empty just add to list
        if (!NewInventory.Any())
        {
            Debug.Log("added new stack of : " + item.itemName + " with amount of " + amount);
            NewInventory.Add(new InventoryV2 { Name = item.itemName, Quantity = amount, Type = item.itemType, IsTool = item.itemIsTool, Rarity = item.rarity, ValueInGameCurrency = item.valueInGameCurrency, MaxStack = item.maxStack, Stackable = item.stackable, stack = 1, stackFull = false});
        }
        else
        {
            //check if item is stackable
            if (item.stackable && ItemExistsInInv)
            {
                //go trough all items in inventory that are stackable
                foreach (InventoryV2 itemInInventory in NewInventory)
                {
                    //check for mathing name, checks if stack is full, if it is then we skip it
                    if (itemInInventory.Name == item.itemName && itemInInventory.stackFull == false &&  itemInInventory.stack == stackRotation)
                    {
                        //pluss the old amount pluss the new to check if the amount bypass the max, then creat new stack
                        int quantityLeftOver = (itemInInventory.Quantity + amount) - itemInInventory.MaxStack;
                        if (quantityLeftOver > 0)
                        {
                            //if bypass set quantity to max and mark as full stac
                            //also make sure its above 0 if just max stack still needs to be able to remove from it, so only set to fullstack if it has more stacks
                            itemInInventory.Quantity = itemInInventory.MaxStack;
                            itemInInventory.stackFull = true;

                            //creat new stack now with stack pluss one
                            Debug.Log("bypass max, creat new stack of the new item with the left over amount witch is " + quantityLeftOver + " this is stack number " + (itemInInventory.stack + 1));
                            NewInventory.Add(new InventoryV2 { Name = item.itemName, Quantity = quantityLeftOver, Type = item.itemType, IsTool = item.itemIsTool, Rarity = item.rarity, ValueInGameCurrency = item.valueInGameCurrency, MaxStack = item.maxStack, Stackable = item.stackable, stack = itemInInventory.stack+1, stackFull = false});
                            FixChanges();
                            return;
                        }
                        else
                        {
                            //if not bypass just add the amount to the stack
                            itemInInventory.Quantity += amount;
                            Debug.Log("added amount to existing stack of item: " + item.itemName + " with amount of " + amount + " now has " + itemInInventory.Quantity + " this is stack number " + itemInInventory.stack);
                        }
                        return;
                    }
                    //if stack is full, skip to next stack.
                    if (itemInInventory.Name == item.itemName && itemInInventory.stackFull == true)
                    {
                        Debug.Log("stack number " + itemInInventory.stack + " is full, skip to next stack if exist");
                        stackRotation++;
                        continue;
                    }
                    /*if (itemInInventory.Name != item.itemName)
                    {
                        
                    }*/
                    
                }
            }
            if (item.stackable && !ItemExistsInInv)
            {
                //if no match creat new stack of the new item and set stack to stackRotation
                Debug.Log("no match, creat new stack of the new item of " + item.itemName + " with amount of " + amount + " this is stack number " + stackRotation);
                NewInventory.Add(new InventoryV2 { Name = item.itemName, Quantity = amount, Type = item.itemType, IsTool = item.itemIsTool, Rarity = item.rarity, ValueInGameCurrency = item.valueInGameCurrency, MaxStack = item.maxStack, Stackable = item.stackable, stack = 1, stackFull = false});
                FixChanges();
                return;
            }
            //if item is not stackable
            else
            {
                Debug.Log("item is not stackable, creat new stack of the new item");
                NewInventory.Add(new InventoryV2 { Name = item.itemName, Quantity = amount, Type = item.itemType, IsTool = item.itemIsTool, Rarity = item.rarity, ValueInGameCurrency = item.valueInGameCurrency, MaxStack = item.maxStack, Stackable = item.stackable, stack = 1, stackFull = true});
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
                        int stackNumber = itemInInventory.stack;
                        Debug.Log("Item " + itemInInventory.Name + " is now 0, removing from inventory, it was stack number " + itemInInventory.stack);
                        Debug.Log("!removing code1!");
                        NewInventory.Remove(itemInInventory);

                        //if its multiple stacks go back to last stack and set it to not fullstack so we can remove from it when that is needed. 
                        Debug.Log("checking for more stacks of this item, looking for stack number " + (stackNumber - 1));
                        if (stackNumber > 1)
                        {
                            //Go trough inventory to find the next activ stack, and set it to false fullstack even tho its full, its always just one stack of every item that is not full witch is the activ stack.
                            for (int i = 0; i < NewInventory.Count; i++)
                            //using a for loop becuse unity did not like that i removed items as i wolked trough the inventory or somthing like that.
                            {
                                //finds activ stack by finding the next stack (stacknumber -1)
                                if (NewInventory[i].Name == item.itemName && NewInventory[i].stack == (stackNumber - 1))
                                {
                                    NewInventory[i].stackFull = false;
                                    Debug.Log("activ stack is now stack number " + NewInventory[i].stack + " with amount of " + NewInventory[i].Quantity);
                                    FixChanges();
                                }
                            }
                        }
                        else{
                            Debug.Log("no more stacks of this item");
                        }
                    }

                    Debug.Log("removing " + amount + " from stack number " + itemInInventory.stack + " new amount is " + itemInInventory.Quantity);
                    return;
                }
                //if the amount is higher then quantity but has multiple stackes
                if (itemInInventory.Quantity < amount && itemInInventory.stack > 1){
                    
                    //finds the leftover and makes it a positiv number. 
                    int LeftOverAmount = ((itemInInventory.Quantity - amount) * -1);
                    //get the stack number we search for that would be the next stack to unpack.
                    int stackNumber = (itemInInventory.stack -1);

                    Debug.Log("Removing stack number " + itemInInventory.stack + " leftover amount is " + LeftOverAmount + " looking for stack number " + stackNumber);
                    //remove the empty stack
                    Debug.Log("!removing code2!");
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
                                Debug.Log("now activ stack is stack number " + itemInInventoryR2.stack + " with amount of " + itemInInventoryR2.Quantity);
                                FixChanges();
                            }
                        }
                        else{
                            Debug.Log("(!not good not good!) , looking for stack number " + stackNumber + " but found stack number " + itemInInventoryR2.stack + " with name " + itemInInventoryR2.Name);
                        }
                    }
                    return;
                }
                else{
                    //if amount higher then full amount of item
                    Debug.Log("you broke ass, you dont have enough iteams to do that bitch ass.");
                }
            }
            else{
                Debug.Log("Item not found in inventory.");
            }
        }
        
    }

    public void FixChanges()
    {
        Debug.Log("sorting inventory...");
        //sort bassed on name always sort over the inventory after adding or removing items.
        NewInventory.Sort((a,b) => a.Name.CompareTo(b.Name)); 
        logInv();
    }

    public void logInv(){
        Debug.Log("------------------------------------------");
        foreach (InventoryV2 itemInInventory in NewInventory){
            Debug.Log("Item: " + itemInInventory.Name + " Stack: " + itemInInventory.stack + " Quantity: " + itemInInventory.Quantity + " stackFull: " + itemInInventory.stackFull);
        }
        //UILogic.UpdateSlots(); // Update the UI when logging inventory
    }


    public void Test(){
        Debug.Log("Test function called");
    }
}


