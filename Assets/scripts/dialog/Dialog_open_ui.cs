using UnityEngine;
using TMPro;
using System.IO;
using System.Collections.Generic;


[System.Serializable]
public class NPCDialogChoice
{
    public string text;
    public int next_id;
}

[System.Serializable]
public class NPCDialogLine
{
    public string text;
    public string speaker;
    public int id;
    public int nextID;
    public int QuestID;
    public int completeQuestID;

    public List<NPCDialogChoice> choices;
}

[System.Serializable]
public class NPCDialogData
{
    public NPCDialogLine[] messages;
}

public class Dialog_open_ui : MonoBehaviour
{
    public StartQuest startQuestScript; 

    public static bool DialogAcctivRN = false;

    public GameObject dialog_ui;   // your panel
    public TMP_Text dialog_text;   // TMP text
    public TMP_Text speaker_text;

    public TMP_Text choiceAccept;
    public TMP_Text choiceDecline;

    
    private int currentChoice = 0;   // 0 = first, 1 = second
    private bool choosing = false;   // are we in a choice state?

    public Color normalColor = Color.white;
    public Color blinkColor = Color.yellow;

    private NPCDialogLine[] lines;
    
    private int nextDialogId = 0;

    private int acceptQuestNextID = 0;
    private int declineQiestNextID = 0;

    //private int questId = -1;

    void Start()
    {
        dialog_ui.SetActive(false);
    }
    

    // Call this to start dialog using a json file
    public void StartDialog(string fileName, int Dialog_id)
    {
        string path = Path.Combine(Application.streamingAssetsPath, fileName);

        string json = File.ReadAllText(path);

        NPCDialogData data = JsonUtility.FromJson<NPCDialogData>(json);

        lines = data.messages;

        dialog_ui.SetActive(true);

        ShowNext(Dialog_id);
    }

    void Update()
    {
        if (!dialog_ui.activeSelf)
            return;

        // if we are choosing
        if (choosing)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                currentChoice = 0;
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                currentChoice = 1;
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (currentChoice == 0)
                {
                    Debug.Log("Accepting choice with next ID: " + acceptQuestNextID);
                    ShowNext(acceptQuestNextID);
                }
                if (currentChoice == 1)
                {
                    Debug.Log("Declining choice with next ID: " + declineQiestNextID);
                    ShowNext(declineQiestNextID);
                }

            }
            if (currentChoice == 0)
            {
                float t = Mathf.PingPong(Time.unscaledTime * 2f, 1f); // 2 = speed
                choiceAccept.color = Color.Lerp(normalColor, blinkColor, t);
            }
            else
            {
                choiceAccept.color = normalColor;
            }
            if (currentChoice == 1)
            {
                float t = Mathf.PingPong(Time.unscaledTime * 2f, 1f); // 2 = speed
                choiceDecline.color = Color.Lerp(normalColor, blinkColor, t);
            }
            else
            {
                choiceDecline.color = normalColor;
            }
        }
        else
        {
            // normal dialog progression with space
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ShowNext(nextDialogId);
            }
        }
    }


    // Show next line
    public void ShowNext(int next_id)
    {
        choosing = false;
        choiceAccept.gameObject.SetActive(false);
        choiceDecline.gameObject.SetActive(false);
        if (next_id == 0)
        {
            dialog_ui.SetActive(false);
            Time.timeScale = 1f;
            DialogAcctivRN = false;
            return;
        }
        if (next_id != 0)
        {
            foreach (NPCDialogLine line in lines)
            {
                if (next_id == line.id)
                {
                    speaker_text.text = line.speaker;
                    dialog_text.text = line.text;
                    if (line.QuestID != 0)
                    {
                        startQuestScript.startQuest(line.QuestID);
                        nextDialogId = line.nextID;
                        return;
                    }
                    if (line.completeQuestID != 0)
                    {
                        startQuestScript.completeQuest(line.completeQuestID);
                        nextDialogId = line.nextID;
                        return;
                    }
                    if (line.choices != null && line.choices.Count > 0)
                    {
                        // turn on choice UI
                        acceptQuestNextID = line.choices[0].next_id;
                        declineQiestNextID = line.choices[1].next_id;
                        ShowChoices(line.choices[0].text, line.choices[1].text);
                    }
                    if (line.choices == null && line.nextID != 0 && line.QuestID == 0 && line.completeQuestID == 0)
                    {
                        choosing = false;
                        choiceAccept.gameObject.SetActive(false);
                        choiceDecline.gameObject.SetActive(false);

                        nextDialogId = line.nextID;
                    }
                    else{
                        nextDialogId = line.nextID;
                    }
                    break;
                }
            }
        }
        //if dialog is finished
        /*if (lines == null || currentIndex >= lines.Length)
        {
            dialog_ui.SetActive(false);
            return;
        }

        NPCDialogLine line = lines[currentIndex];

        dialog_text.text = line.text;

        // ⭐ check choices
        

        currentIndex++;*/
        /*if (next_id != 0)
        {
           
            foreach (NPCDialogLine line in lines)
            {
                //Debug.Log("line of somthing idk : "+ line.id);
                if (next_id == line.id)
                {
                    speaker_text.text = line.speaker;
                    dialog_text.text = line.text;
                    //Debug.Log(line.QuestID);
                    if (line.QuestID != 0)
                    {
                        Debug.Log("starting quest with id: " + line.QuestID);
                        startQuestScript.startQuest(line.QuestID);
                        nextDialogId = line.nextID;
                        return;
                    }
                    if (line.choices != null && line.choices.Count > 0)
                    {
                        Debug.Log("showing choices for line id: " + line.id);
                        // turn on choice UI
                        acceptQuestNextID = line.choices[0].next_id;
                        declineQiestNextID = line.choices[1].next_id;
                        ShowChoices(line.choices[0].text, line.choices[1].text);
                    }
                    if (line.completeQuestID != 0)
                    {
                        Debug.Log("completing quest with id: " + line.completeQuestID);
                        startQuestScript.completeQuest(line.completeQuestID);
                        nextDialogId = line.nextID;
                        return;
                    }
                    if (line.choices == null && line.nextID != 0)
                    {
                        Debug.Log("no quest to complete            "+ line.nextID);
                        choosing = false;
                        choiceAccept.gameObject.SetActive(false);
                        choiceDecline.gameObject.SetActive(false);
                        
                        nextDialogId = line.nextID;
                    }
                    break;
                }
            }
        }
        if (next_id == 0)
        {
            dialog_ui.SetActive(false);
            Time.timeScale = 1f;
            DialogAcctivRN = false;
            return;
        }*/

    }





    public void ShowChoices(string option1, string option2)
    {
        choosing = true;
        currentChoice = 0;

        choiceAccept.SetText(option1);
        choiceDecline.SetText(option2);

        choiceAccept.gameObject.SetActive(true);
        choiceDecline.gameObject.SetActive(true);
    }

    /*void ConfirmChoice()
    {
        Debug.Log("Chosen: " + currentChoice);

        choosing = false;
        choiceAccept.SetSelected(false);
        choiceDecline.SetSelected(false);

        choiceAccept.gameObject.SetActive(false);
        choiceDecline.gameObject.SetActive(false);

        // TODO: later → branch dialogue based on currentChoice
    }*/
}

