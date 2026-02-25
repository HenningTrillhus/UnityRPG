using UnityEngine;



public class npc_interactable : MonoBehaviour
{
    public GameObject interactionIcon;

    public StartQuest questDialogLogic;

    private bool playerNearby = false;

    public int nextDialogID = 10; 
    public string DialogState = "HasQuest";

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
            Dialog_open_ui.DialogAcctivRN = true;
            if (DialogState == "HasQuest"){
                nextDialogID = questDialogLogic.DialogQuestManager("Aldric Woodrow");
                //Debug.Log(nextDialogID);
                dialogUI.StartDialog("npc1_dialog.json", nextDialogID);
            }
            if (DialogState == "Completing" || DialogState == "Idle")
            {
                dialogUI.StartDialog("npc1_dialog.json", nextDialogID);
            }
            
            //Debug.Log(questDialogLogic.possibleQuests[0]);
        }
    }


}
