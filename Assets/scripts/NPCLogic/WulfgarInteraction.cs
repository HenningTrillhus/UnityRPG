using UnityEngine;

public class Wulfgar : MonoBehaviour
{
    public GameObject interactionIcon;

    public StartQuest questDialogLogic;

    private bool playerNearby = false;

    public int nextDialogID = 10; 

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
                nextDialogID = questDialogLogic.DialogQuestManager("Wulfgar");
                //Debug.Log(nextDialogID);
                dialogUI.StartDialog("Wulfgar_Dialog.json", nextDialogID);
            }
            if (DialogState == "Completing" || DialogState == "Idle")
            {
                Debug.Log(nextDialogID);
                dialogUI.StartDialog("Wulfgar_Dialog.json", nextDialogID);
            }
            
            //Debug.Log(questDialogLogic.possibleQuests[0]);
        }
    }
}
