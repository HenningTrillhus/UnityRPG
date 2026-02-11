using UnityEngine;
using System.IO;
using System.Collections.Generic;

[System.Serializable]
//for the dialog of the NPC
public class DialogMessage
{
    public int id; //id of Dialog of NPC
    public string speaker;
    public string text;
    
    public List<DialogChoice> choices; 
}

[System.Serializable]
//For the cialog chooses of the player
public class DialogChoice
{
    public string text;
    public string next_id;
}

[System.Serializable]
public class DialogMessageArray
{
    public DialogMessage[] messages;
}

public class Dialog_JSON_test : MonoBehaviour
{
    void Start()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "npc1_dialog.json");

        string json = File.ReadAllText(path);

        DialogMessageArray data = JsonUtility.FromJson<DialogMessageArray>(json);

        // Test: print each message separately
        foreach (DialogMessage msg in data.messages)
        {
            //Debug.Log("Message: " + msg.text);
            if (msg.choices != null && msg.choices.Count > 0){
                foreach (DialogChoice choice in msg.choices){
                    //Debug.Log("Choice: " + choice.text + " -> " + choice.next_id);
                }
            }
        }
    }
}
