using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public string itemType;
    public bool itemIsTool;
    public string rarity;
    public int valueInGameCurrency;
    public Sprite icon;
    public bool stackable = true;
    public int maxStack = 99;
}
