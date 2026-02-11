// Marks this class so Unity can serialize it (show it in the Inspector and save it).
[System.Serializable]
public class Inventory_Slot
{
    // The item type stored in this slot (null means the slot has no item).
    public ItemData item;

    // How many of the item are stored in this slot.
    public int amount;

    // Convenience check to see if the slot has no item assigned.
    public bool IsEmpty => item == null;

    // Resets the slot back to an empty state.
    public void Clear()
    {
        // Remove the item reference so the slot is treated as empty.
        item = null;

        // Reset the stack count to zero.
        amount = 0;
    }
}
