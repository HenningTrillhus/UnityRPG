using TMPro; 
using UnityEngine;

public class Dialog_display_text : MonoBehaviour
{
    public TMP_Text dialog_text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialog_text.text = "Welcome to our village, traveler!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDialog(string message)
    {
        dialog_text.text = message;
    }
}
