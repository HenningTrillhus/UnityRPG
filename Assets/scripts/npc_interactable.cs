using UnityEngine;



public class npc_interactable : MonoBehaviour
{
    public GameObject interactionIcon;

    private bool playerNearby = false;

    public Dialog_open_ui dialogUI;
    
    void Start()
    {
        interactionIcon.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            interactionIcon.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            interactionIcon.SetActive(false);
        }
    }
    private void Update() {
        if (!playerNearby) return;

        if (dialogUI != null && Input.GetKeyDown(KeyCode.E) && !InventoryUI.IsOpen)
        {
            Time.timeScale = 0f; // FREEZES physics, animations, Update() on non-Time-independent code
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Dialog_open_ui.DialogAcctivRN = true;
            dialogUI.StartDialog("npc1_dialog.json", 10);
        }
    }


}
