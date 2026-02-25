using UnityEngine;
using TMPro;

public class MetaMessage : MonoBehaviour
{
    [Header("Script Ref")]

    [Header("UI Ref")]
    public GameObject MetaMessageContainer;
    public TMP_Text MetaMessageText;
    public Transform canvasTransform;

    private GameObject MetaMessagePreFab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMetaMessage(string massage)
    {
        MetaMessageText.text = massage;
        GameObject MetaMessagePreFab = Instantiate(MetaMessageContainer, canvasTransform);
        MetaMessagePreFab.SetActive(true);
        
        Destroy(MetaMessagePreFab, 2f);
    }
}
