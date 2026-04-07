using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public int itemID;
    public string itemName;
    public string itemType;
    public string itemDescription;
    public bool itemIsTool;
    public string rarity;
    public int valueInGameCurrency;
    public Sprite icon;
    public bool stackable = true;
    public bool placeable = false;
    public bool shelfPlaceable = false;
    public int shelfSlotsTaking = 1;
    public int maxStack = 99;
}
