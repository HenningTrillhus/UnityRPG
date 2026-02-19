using UnityEngine;

public class InventoryUI2 : MonoBehaviour
{
    [Header("References")]
    public GameObject inventoryPanel;
    public Player_Inventory playerInventory;

    public static bool IsOpen { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IsOpen = false;
        CreateSlots();
    }

    // Update is called once per frame
    void Update()
    {
        playerInventory.inventoryV2.Test();
        // toggle inventory
        /*if (Input.GetKeyDown(KeyCode.Tab) && (!GameManager.Instance.IsGamePaused()) && (Dialog_open_ui.DialogAcctivRN != true))
        {
            //Debug.Log(Dialog_open_ui.DialogAcctivRN);
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            IsOpen = !IsOpen;
        }*/
    }

    void CreateSlots()
    {
        int slotCount = playerInventory.inventoryV2.inventoryCapacity;
        Debug.Log("this is the inventory capacity form the ui " + slotCount);
    }

    public void UpdateSlots()
    {
        Debug.Log("Updating slots in InventoryUI2");
    }
}
