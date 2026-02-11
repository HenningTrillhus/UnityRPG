using UnityEngine;
using TMPro;
using System.IO;
using System.Collections.Generic;
[System.Serializable]
public class questMultipleSegmentsText
{
    public string text;
}


[System.Serializable]
public class quests
{
    public string text;
    public string type;
    public int id;

    public List<questMultipleSegmentsText> questTexts;

}

[System.Serializable]
public class questsData
{
    public quests[] quests;
}

public class StartQuest : MonoBehaviour 
{
    public ItemData woodItem; 
    public TMP_Text questInstructions;
    private string displayText;

    public Inventory inventory;

    List<int> activQuests = new List<int>();

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
                    displayText = quest.questTexts[0].text;
                    questInstructions.text = displayText;

                    //Debug.Log(inventory.checkForItem("Wood Log"));
                    inventory.checkForItem("Wood Log");
                }
                //Debug.Log("quest " + activQuests[0] + " started");
            }
        }
    }
}
