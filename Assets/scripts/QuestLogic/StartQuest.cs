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


    private string questNameText;
    private string questNPCNameText;
    private string displayText1;
    private string displayText2;

    public Player_Inventory playerInventory;
    public npc_interactable npcScript;
    

    List<int> activQuests = new List<int>();
    List<int> possibleQuests = new List<int>();
    List<int> completedQuests = new List<int>();

    private quests[] Quests;

    private string fileName = "quests.json";

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
                    if (activQuests[i] == quest.id){
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
                
                if (NPC == "Aldric Woodrow" && possibleQuests[i] == 1)
                //Run quest 1
                {
                    Debug.Log("Now running dialog man, postible quest now are: " + possibleQuests[i]);
                    //return Dialog Id for quest 1
                    return 10;
                }
                if (NPC == "Aldric Woodrow" && possibleQuests[i] == 2)
                //Run quest 1
                {
                    Debug.Log("Now running dialog man, postible quest now are: " + possibleQuests[i]);
                    //return Dialog Id for quest 1
                    return 18;
                }
                else{
                    continue;
                }   
            }
            return 0;
        }
        else{
            Debug.Log("no posible quests found");
            return 0;
        }
        
        
    }

    public void SwitchQuestDisplayLeft()
    {
        Debug.Log("Left pressed");
    }
}
