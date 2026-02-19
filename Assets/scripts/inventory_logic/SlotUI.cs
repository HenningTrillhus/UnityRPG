using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotUI : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI amountText;

    //public void UpdateSlot(Inventory_Slot slot)
    public void UpdateSlot(Inventory_Slot slot)
    {
        if (slot.IsEmpty)
        {
            icon.enabled = false;
            amountText.text = "";
        }
        else
        {
            icon.enabled = true;
            icon.sprite = slot.item.icon;
            amountText.text = slot.amount > 1 ? slot.amount.ToString() : "";
        }
    }
}
