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
                if (quest.type == "Gather")
                {
                    //displayText = quest.questTexts[0].text + "   (" + playerInventory.inventoryV2.CheckForItem(woodItem) + "/25)";
                    //questInstructions.text = displayText;

                    //Debug.Log(inventory.checkForItem("Wood Log"));
                    Debug.Log("quest " + quest.id + " started, check for item: " + woodItem.itemName + " player has " + playerInventory.inventoryV2.CheckForItem(woodItem));
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
                        

                        if (playerInventory.inventoryV2.CheckForItemUsingName(quest.materialToGather[0].material) >= quest.materialToGather[0].amount)
                        {
                            displayText1 = quest.questTexts[0].text + "   (" + quest.materialToGather[0].amount + "/" + quest.materialToGather[0].amount + ")";
                            questInstructions1.text = displayText1;
                            displayText2 = quest.questTexts[1].text;
                            questInstructions2.text = displayText2;
                            DialogManager(quest.npcGiver, quest.completeDialogId);
                        }
                        else{
                            displayText1 = quest.questTexts[0].text + "   (" + playerInventory.inventoryV2.CheckForItemUsingName(quest.materialToGather[0].material) + "/" + quest.materialToGather[0].amount + ")";
                            questInstructions1.text = displayText1;
                            displayText2 = "";
                            questInstructions2.text = displayText2;
                            DialogManager(quest.npcGiver, quest.idleDialogId);
                        }
                        //Debug.Log(quest.materialToGather.amount[0]);
                        //if (playerInventory.inventoryV2.CheckForItem(woodItem) > quest.materialToGather[0].amount){
                        //    Debug.Log("Stage complete");
                        //}
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
        foreach (quests quest in Quests)
        {
            if (quest.id == questID)
            {
                completedQuests.Add(quest.id);
                activQuests.Remove(quest.id);
                for (int i = 0; i < quest.nextPosibleQuests.Count; i++)
                {
                    possibleQuests.Add(quest.nextPosibleQuests[i].id);
                }
            }
        }
    }

    public void DialogManager(string NPC, int dialogID)
    {
        if (NPC == "Aldric Woodrow")
        {
            npcScript.nextDialogID = dialogID;
        }
    }
}
