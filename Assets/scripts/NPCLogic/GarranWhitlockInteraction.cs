using UnityEngine;

public class GarranWhitlockInteraction : MonoBehaviour
{
    public GameObject interactionIcon;

    public StartQuest questDialogLogic;

    private bool playerNearby = false;

    public int nextDialogID = 10; 

    //Set to (has quest) by start so if player interacte but no posible quest just return some random dialog like "hey want to suck my dick?"
    public string DialogState = "HasQuest";

    public Dialog_open_ui dialogUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
            Dialog_open_ui.DialogAcctivRN = true;
            if (DialogState == "HasQuest"){
                nextDialogID = questDialogLogic.DialogQuestManager("Garran Whitlock");
                //Debug.Log(nextDialogID);
                dialogUI.StartDialog("GarranWhitelock_Dialog.json", nextDialogID);
            }
            if (DialogState == "Completing" || DialogState == "Idle")
            {
                dialogUI.StartDialog("GarranWhitelock_Dialog.json", nextDialogID);
            }
            
            //Debug.Log(questDialogLogic.possibleQuests[0]);
        }
    }
}
