using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class JustSoldItemDisplay : MonoBehaviour
{
    public static JustSoldItemDisplay Instance { get; private set; }


    public GameObject JustSoldPanel;
    public GameObject JustSoldPanelItemImage;
    public TMP_Text JustSoldPanelCoinAmount;

    private int startYpos = -420;

    public Canvas canvas;

    private List<GameObject> spawnedPanels = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake() {
        Instance = this;
    }

    public void printSoldItemPanel(string itemName)
    {
        (string Name, Sprite itemIcon, int itemValue) = CostumerSpawner.Instance.getItemInfoByID(0, itemName);
        Debug.Log(itemValue);
        JustSoldPanelCoinAmount.text = itemValue.ToString();
        
        GameObject panel = Instantiate(JustSoldPanel, canvas.transform);
        Debug.Log("JustSoldPanelItemImage: " + JustSoldPanelItemImage);
        Debug.Log("itemIcon: " + itemIcon);
        panel.transform.Find("JustsoldItemBG/JustSoldItem").GetComponent<UnityEngine.UI.RawImage>().texture = itemIcon.texture;
        panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1052, startYpos);
        spawnedPanels.Insert(0,panel);
        FixPanelPositions();
        StartCoroutine(destroyPanel(panel));
    }

    private void FixPanelPositions()
    {
        for (int i = 0; i < spawnedPanels.Count; i++)
        {
            if (i >= 3)
            {
                Destroy(spawnedPanels[i]);
                spawnedPanels.RemoveAt(i);
            }
            if (i < spawnedPanels.Count)
            {
                spawnedPanels[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(-1052, startYpos + (125 * i));
            }
        }
    }

    private IEnumerator destroyPanel(GameObject panel)
    {
        yield return new WaitForSeconds(3.5f);
        spawnedPanels.Remove(panel);
        Destroy(panel);
        FixPanelPositions();
    }


    void Start()
    {
        JustSoldPanel.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
