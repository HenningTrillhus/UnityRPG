using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemForBackEndInventory
{
    public string Name;
    public int Amount;

    public ItemForBackEndInventory(string name, int amount)
    {
        Name = name;
        Amount = amount;
    }
}


[System.Serializable]
public class Inventory 
{
    public List<ItemForBackEndInventory> backEndInventory = new List<ItemForBackEndInventory>();

    public int Capacity { get; private set; }

    private List<Inventory_Slot> slots;

    public IReadOnlyList<Inventory_Slot> Slots => slots;

    // Event for UI later (optional but good design)

    public Inventory(int capacity)
    {
        Capacity = capacity;
        slots = new List<Inventory_Slot>(capacity);

        for (int i = 0; i < capacity; i++)
            slots.Add(new Inventory_Slot());
    }



// Adds an item to the inventory, stacking if possible. Returns true if successful.
    public bool AddItem(ItemData item, int amount = 1)
    {
        // stack first
        if (item.stackable)
        {
            foreach (var slot in slots)
            {
                if (!slot.IsEmpty && slot.item.itemName == item.itemName)
                {
                    slot.amount += amount;
                    
                    foreach (var itemInBackEnd in backEndInventory)
                    {
                        Debug.Log(itemInBackEnd.Name);
                        if (itemInBackEnd.Name == item.itemName)
                        {
                            itemInBackEnd.Amount += amount;
                            break;
                        }
                    }
                    return true;
                }
            }
        }

        // empty slot
        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                slot.item = item;
                slot.amount = amount;
                backEndInventory.Add(new ItemForBackEndInventory(item.itemName, amount));
                Debug.Log(backEndInventory.Count);
                return true;
            }
        }

        return false;
    }


// Removes a specified amount of an item from the inventory. Returns true if successful.
    public bool RemoveItem(ItemData item, int amount = 1)
    {
        foreach (var slot in slots)
        {
            if (!slot.IsEmpty && slot.item == item)
            {
                slot.amount -= amount;
                if (slot.amount <= 0) slot.Clear();
                foreach (var itemInBackEnd in backEndInventory)
                {
                    if (itemInBackEnd.Name == item.itemName)
                    {
                        itemInBackEnd.Amount -= amount;
                        if (itemInBackEnd.Amount <= 0) backEndInventory.Remove(itemInBackEnd);
                        break;
                    }
                }

                return true;
            }
        }

        return false;
    }


// Resizes the inventory to a new capacity, adding or removing slots as needed.
    public void Resize(int newCapacity)
    {
        if (newCapacity > Capacity)
        {
            for (int i = Capacity; i < newCapacity; i++)
                slots.Add(new Inventory_Slot());
        }
        else if (newCapacity < Capacity)
        {
            slots.RemoveRange(newCapacity, Capacity - newCapacity);
        }

        Capacity = newCapacity;
    }


    public int checkForItem(string ItemLookedFor)
    {
        /*Debug.Log("function is running, with some data like: " + backEndInventory.Count);
        foreach (var itemInBackEnd in backEndInventory) 
        {
            Debug.Log("for each is running.");
            if (itemInBackEnd.Name == ItemLookedFor)
            {
                Debug.Log("item found in inventory: " + itemInBackEnd.Name + " with amount: " + itemInBackEnd.Amount);
                return itemInBackEnd.Amount;
            }         
        }
        return 0;*/
        Debug.Log("Checking slots for item: " + ItemLookedFor);

        foreach (var slot in slots)
        {
            if (!slot.IsEmpty && slot.item.itemName == ItemLookedFor)
            {
                Debug.Log("Item found in slot: " + slot.item.itemName + " with amount: " + slot.amount);
                return slot.amount;
            }
        }

        Debug.Log("Item not found in any slot: " + ItemLookedFor);
        return 0; // not found
    }
}