using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InventoryUI2 : MonoBehaviour
{
    [Header("References")]
    public GameObject inventoryPanel;
    public Player_Inventory playerInventory;

    [Header("Slot Setup")]
    public Transform slotGrid;      // parent (SlotGrid)
    public GameObject slotPrefab;   // Slot prefab

    [Header("Slot UI")]
    public Image icon;
    public TextMeshProUGUI amountText;
    public TextMeshProUGUI NameText;
    
    [Header("JustGotItemUI")]
    public GameObject justGotItemUI;
    public Image justGotItemIcon;
    public TMP_Text justGotItemText;
    public TMP_Text justGotItemAmountText;
    public float justGotItemDisplayTime = 2f; // Time to display the "Just Got Item" UI
    public Transform canvasTransform;


    private GameObject JGIUI;

    private Coroutine hideCoroutine;


    public static bool IsOpen { get; private set; }

    public List<SlotUI> slotUIs = new List<SlotUI>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IsOpen = false;
        CreateSlots();
    }

    // Update is called once per frame
    void Update()
    {
        //playerInventory.inventoryV2.Test();
        // toggle inventory
        if (Input.GetKeyDown(KeyCode.Tab) && (!GameManager.Instance.IsGamePaused()) && (Dialog_open_ui.DialogAcctivRN != true))
        {
            //Debug.Log(Dialog_open_ui.DialogAcctivRN);
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            IsOpen = !IsOpen;
        }
    }

    void CreateSlots()
    {
        int slotCount = playerInventory.inventoryV2.inventoryCapacity;
        Debug.Log("this is the inventory capacity form the ui " + slotCount);
        for (int i = 0; i < slotCount; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotGrid);
            slotUIs.Add(slotObj.GetComponent<SlotUI>());
            /*foreach (var slotUI in slotUIs)
            {
                if (slotUI == null)
                {
                    Debug.LogError("SlotUI component not found on slot object!");
                }
                else
                {
                    Debug.Log("SlotUI component found on slot object.");
                    slotUI.amountText.text = "4";
                    Debug.Log("SlotUI component name: " + slotUI.amountText.text);
                }
            }
            //slotUIs.Add(slotObj);*/
            
            //Debug.Log(slotUIs.Count);
            UpdateSlot(slotObj); // Initialize the slot as empty
        }
    }

    public void UpdateSlots()
    {
        int lenghtOfInventory = playerInventory.inventoryV2.NewInventory.Count;
        int slotCount = playerInventory.inventoryV2.inventoryCapacity;
        Debug.Log("Updating slots in InventoryUI2 with inventory count: " + lenghtOfInventory);

        int index = 0;
        foreach (var slotUI in slotUIs)
        {
            if (slotUI == null)
            {
                Debug.LogError("SlotUI component not found in slotUIs list!");
                slotUI.amountText.text = "0";
                slotUI.NameText.text = "";
                slotUI.icon.sprite = null;
            }
            if (slotUI != null && index < lenghtOfInventory)
            {
                slotUI.NameText.text = playerInventory.inventoryV2.NewInventory[index].Name;
                string amount = (playerInventory.inventoryV2.NewInventory[index].Quantity).ToString();
                slotUI.amountText.text = amount;
                slotUI.icon.sprite = playerInventory.inventoryV2.NewInventory[index].Icon;
                Debug.Log("slot postion " + index + " has item " + playerInventory.inventoryV2.NewInventory[index].Name + " with amount of " + playerInventory.inventoryV2.NewInventory[index].Quantity);
            }
            if (index > (lenghtOfInventory-1) && slotUI != null){
                slotUI.amountText.text = "0";
                slotUI.NameText.text = "";
                slotUI.icon.sprite = null;
            }
            
            index++;
        }
    }

    public void UpdateSlot(GameObject slotObj)
    {
        SlotUI slotUI = slotObj.GetComponent<SlotUI>();
        if (slotUI == null)
        {
            Debug.LogError("SlotUI component not found on slot object!");
            return;
        }
        else{
            amountText.text = "0";
            icon.sprite = null;
            slotObj.SetActive(true);
        }

        /*if (slotUI.IsEmpty)
        {
            slotObj.SetActive(true);
            icon.enabled = false;
            amountText.text = "";
        }
        else
        {
            icon.enabled = true;
            icon.sprite = slotUI.item.icon;
            amountText.text = slotUI.amount > 1 ? slotUI.amount.ToString() : "";
        }*/
    }

    public void ShowJustGotItem(Sprite icon, string itemName , int amount)
    {
        justGotItemIcon.sprite = icon;
        justGotItemText.text = itemName;
        justGotItemAmountText.text = amount > 1 ? "x" + amount.ToString() : "";
        GameObject JGIUI = Instantiate(justGotItemUI, canvasTransform);
        JGIUI.SetActive(true);
        Destroy(JGIUI, justGotItemDisplayTime);
    }

    
}
