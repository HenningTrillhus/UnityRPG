using UnityEngine;
using TMPro;
using System.IO;
using System.Collections.Generic;

[System.Serializable]
public class questMultipleSegmentsPosibleQuests
{
    public int id;
}

[System.Serializable]
public class questMultipleSegmentsmaterialToGather
{
    public string material;
    public int amount;
}

[System.Serializable]
public class questMultipleSegmentsStartingLoot
{
    public string item;
    public int amount;
}

[System.Serializable]
public class questMultipleSegmentsText
{
    public string text; 
}


[System.Serializable]
public class quests
{
    public string text;
    public string name;
    public string npcGiver;
    public int idleDialogId;
    public int completeDialogId;
    public string type;
    public int id;
    public int completeCoins;
    public int completeExp;

    public List<questMultipleSegmentsText> questTexts;
    public List<questMultipleSegmentsmaterialToGather> materialToGather;
    public List<questMultipleSegmentsPosibleQuests> nextPosibleQuests;
    public List<questMultipleSegmentsStartingLoot> startingLoot;

}

[System.Serializable]
public class questsData
{
    public quests[] quests;
}



public class StartQuest : MonoBehaviour 
{
    [Header("Items")]
    public ItemData woodItem; 
    [Header("Text OBJs")]
    public TMP_Text questName;
    public TMP_Text questNPCName;
    public TMP_Text questInstructions1;
    public TMP_Text questInstructions2;
    public TMP_Text questInstructions3;


    private string questNameText;
    private string questNPCNameText;
    private string displayText1;
    private string displayText2;
    private string displayText3;

    public Player_Inventory playerInventory;
    public npc_interactable npcScript;
    

    List<int> activQuests = new List<int>();
    List<int> possibleQuests = new List<int>();
    List<int> completedQuests = new List<int>();

    public class questActions
    {
        public int QuestID;
        public string QuestAction;
    }

    public List<questActions> QuestsActions = new List<questActions>();

    private quests[] Quests;

    private string fileName = "quests.json";

    private int IndexOfShowenQuest = 0;

    private void Awake() {
        //Possible start quests
        possibleQuests.Add(1);
    }

    private void Start() {
        string path = Path.Combine(Application.streamingAssetsPath, fileName);

        string json = File.ReadAllText(path);

        questsData data = JsonUtility.FromJson<questsData>(json);

        Quests = data.quests;
    }

    public void startQuest(int questID)
    {
        foreach (quests quest in Quests)
        {
            if (questID == quest.id)
            {
                activQuests.Add(quest.id);
                possibleQuests.Remove(quest.id);
                if (quest.type == "Gather")
                {
                    playerInventory.inventoryV2.AddItemV2(quest.startingLoot[0].item, quest.startingLoot[0].amount);
                    //displayText = quest.questTexts[0].text + "   (" + playerInventory.inventoryV2.CheckForItem(woodItem) + "/25)";
                    //questInstructions.text = displayText;

                    //Debug.Log(inventory.checkForItem("Wood Log"));
                    //Debug.Log("quest " + quest.id + " started, check for item: " + woodItem.itemName + " player has " + playerInventory.inventoryV2.CheckForItem(woodItem));
                    /*if (playerInventory.inventoryV2.CheckForItem(woodItem))
                    {
                        Debug.Log("quest " + quest.id + " completed");
                        activQuests.Remove(quest.id);
                    }
                    else
                    {
                        Debug.Log("quest " + quest.id + " not completed, missing item: " + woodItem.itemName);
                    }*/
                }
                //Debug.Log("quest " + activQuests[0] + " started");
            }
        }
    }

    private void Update() {
        //checks for any activ quests at this time, if not dont do anything
        //Debug.Log(playerInventory.inventoryV2.CheckForItemUsingName("Wood log"));
        if (activQuests.Count > 0)
        {
            for (int i = 0; i < activQuests.Count; i++)
            {
                foreach (quests quest in Quests)
                {
                    if (activQuests[IndexOfShowenQuest] == quest.id){
                        questNameText = quest.name;
                        questName.text = questNameText;
                        questNPCNameText = quest.npcGiver;
                        questNPCName.text = questNPCNameText;
                        
                        if (quest.type == "Gather")
                        {
                            if (playerInventory.inventoryV2.CheckForItemUsingName(quest.materialToGather[0].material) >= quest.materialToGather[0].amount)
                            {
                                //Next Stage ready for completion
                                displayText1 = quest.questTexts[0].text + "   (" + quest.materialToGather[0].amount + "/" + quest.materialToGather[0].amount + ")";
                                questInstructions1.text = displayText1;
                                displayText2 = quest.questTexts[1].text;
                                questInstructions2.text = displayText2;
                                SetDialogState(quest.npcGiver, "Completing");
                                DialogManager(quest.npcGiver, quest.completeDialogId);
                            }
                            else
                            {
                                //not compleeted
                                displayText1 = quest.questTexts[0].text + "   (" + playerInventory.inventoryV2.CheckForItemUsingName(quest.materialToGather[0].material) + "/" + quest.materialToGather[0].amount + ")";
                                questInstructions1.text = displayText1;
                                displayText2 = "";
                                questInstructions2.text = displayText2;
                                SetDialogState(quest.npcGiver, "Idle");
                                DialogManager(quest.npcGiver, quest.idleDialogId);
                            }
                        }
                        if (quest.type == "MeetAndGather")
                        {
                            if (QuestsActions.Count != 0)
                            {
                                for (int x = 0; x < QuestsActions.Count; x++)
                                {
                                    if (QuestsActions[x].QuestID == quest.id){

                                        displayText1 = quest.questTexts[0].text;
                                        questInstructions1.text = displayText1;

                                        displayText2 = quest.questTexts[1].text + "   (" + playerInventory.inventoryV2.CheckForItemUsingName(quest.materialToGather[0].material) + "/" + quest.materialToGather[0].amount + ")";
                                        questInstructions2.text = displayText2;

                                        if (playerInventory.inventoryV2.CheckForItemUsingName(quest.materialToGather[0].material) >= quest.materialToGather[0].amount)
                                        {
                                            //Next Stage ready for completion
                                            displayText2 = quest.questTexts[1].text + "   (" + quest.materialToGather[0].amount + "/" + quest.materialToGather[0].amount + ")";
                                            questInstructions2.text = displayText2;


                                            displayText3 = quest.questTexts[2].text;
                                            questInstructions3.text = displayText3;
                                            SetDialogState(quest.npcGiver, "Completing");
                                            DialogManager(quest.npcGiver, quest.completeDialogId);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //not compleeted
                                displayText1 = quest.questTexts[0].text;
                                questInstructions1.text = displayText1;
                                displayText2 = "";
                                displayText3 = "";
                                questInstructions2.text = displayText2;
                                questInstructions3.text = displayText3;
                                SetDialogState(quest.npcGiver, "Idle");
                                DialogManager(quest.npcGiver, quest.idleDialogId);
                            }
                        }
                    }
                }
                
            }
            //If it is any quests then run trough all of them updating them.
            
        }
        if (activQuests.Count == 0)
        {
            questName.text = "";
            questNPCName.text = "";
            questInstructions1.text = "";
            questInstructions2.text = "";
        }
        
    }

    public void completeQuest(int questID)
    {
        //Runs trough all quests.
        foreach (quests quest in Quests)
        {
            //Fines quest that just completed
            if (quest.id == questID)
            {
                //Add to completedList and removes form acitv list
                completedQuests.Add(quest.id);
                activQuests.Remove(quest.id);

                //adds all the next posible quests to the list of posible quests
                for (int i = 0; i < quest.nextPosibleQuests.Count; i++)
                {
                    Debug.Log("adding to posible quest list " + quest.nextPosibleQuests[i].id);
                    possibleQuests.Add(quest.nextPosibleQuests[i].id);
                }

                //Sett NPC dialog state to HasQuest so its ready to start a new quest in case it has one.
                SetDialogState(quest.npcGiver, "HasQuest");

                //Depening on the quest type, if Gather remove item that you dilivered.
                if (quest.type == "Gather")
                {
                    playerInventory.inventoryV2.RemoveItemV2(quest.materialToGather[0].material,quest.materialToGather[0].amount);
                }
            }
        }
    }

    public void SaveQuestAction(int id, string action)
    {
        QuestsActions.Add(new questActions {QuestID = id, QuestAction = action});
        Debug.Log(QuestsActions[0]);
    }

    public void SetDialogState(string NPC, string State)
    {
        if (NPC == "Aldric Woodrow")
        {
            npcScript.DialogState = State;
        }
    }

    public void DialogManager(string NPC, int dialogID)
    {
        if (NPC == "Aldric Woodrow")
        {
            npcScript.nextDialogID = dialogID;
        }
    }

    public int DialogQuestManager(string NPC)
    {
        
        if (possibleQuests.Count > 0){
            
            //Gonna have to hardcode in quest and dialog some where, this is where.
            for (int i = 0; i < possibleQuests.Count; i++)
            {
                //this only runs if player has the state of "HasQuest"
                
                if (NPC == "Aldric Woodrow")
                //Run quest 1
                {
                    if (possibleQuests[i] == 1){
                        Debug.Log("Now running dialog man, postible quest now are: " + possibleQuests[i]);
                        //return Dialog Id for quest 1
                        return 10;
                    }
                    if (possibleQuests[i] == 2){
                        Debug.Log("Now running dialog man, postible quest now are: " + possibleQuests[i]);
                        //return Dialog Id for quest 1
                        return 18;
                    }
                    else{continue;} 
                    
                }
                if (NPC == "Garran Whitlock"){
                    for (int x = 0; x < activQuests.Count; x++)
                    {
                        if (activQuests[x] == 2)
                        {
                            return 10;
                        }
                    }
                    return -1;
                }
                else{continue;} 
                  
            }
            return 0;
        }
        else{
            Debug.Log("no posible quests found");
            return 0;
        }
        
        
    }

    public void SwitchQuestDisplayRight()
    {
        //If more then 1 activ quest make it posible to change inbetween the quests.
        if (activQuests.Count > 1){
            //If dispaly last quest in list go back to the first one.
            if (activQuests.Count == (IndexOfShowenQuest+1)){
                IndexOfShowenQuest = 0;
            }
            else
            {
                IndexOfShowenQuest++;
            }
        }
        //If non or just one activ quest allways show index of 0.
        else{
            IndexOfShowenQuest = 0;
        }
    }

    public void SwitchQuestDisplayLeft()
    {
        //If more then 1 activ quest make it posible to change inbetween the quests.
        if (activQuests.Count > 1){
            //If dispaly first quest in list dispaly last.
            if (IndexOfShowenQuest == 0){
                IndexOfShowenQuest = (activQuests.Count-1);
            }
            else
            {
                IndexOfShowenQuest--;
            }
        }
        //If non or just one activ quest allways show index of 0.
        else{
            IndexOfShowenQuest = 0;
        }
    }
}
