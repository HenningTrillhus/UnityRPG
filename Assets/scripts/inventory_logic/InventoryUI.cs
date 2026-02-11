using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("References")]
    public GameObject inventoryPanel;
    public Player_Inventory playerInventory;

    [Header("Slot Setup")]
    public Transform slotGrid;      // parent (SlotGrid)
    public GameObject slotPrefab;   // Slot prefab

    private SlotUI[] slotUIs;

    //To let the rest of the game know if the inventory is open or not
    public static bool IsOpen { get; private set; }

    void Start()
    {
        IsOpen = false;
        CreateSlots();
    }

    void Update()
    {
        // toggle inventory
        if (Input.GetKeyDown(KeyCode.Tab) && (!GameManager.Instance.IsGamePaused()) && (Dialog_open_ui.DialogAcctivRN != true))
        {
            //Debug.Log(Dialog_open_ui.DialogAcctivRN);
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            IsOpen = !IsOpen;
        }

        UpdateSlots();
    }

    void CreateSlots()
    {
        int count = playerInventory.inventory.Slots.Count;

        slotUIs = new SlotUI[count];

        for (int i = 0; i < count; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotGrid);

            slotUIs[i] = slotObj.GetComponent<SlotUI>();
        }
    }

    void UpdateSlots()
    {
        for (int i = 0; i < slotUIs.Length; i++)
        {
            slotUIs[i].UpdateSlot(playerInventory.inventory.Slots[i]);
        }
    }
}
