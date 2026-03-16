using UnityEngine;
using TMPro;

public class PlayerLevel : MonoBehaviour
{
    public int playerLevel = 0;



    private float maxHealth = 100f;
    private float health = 100f;

    
    private Vector3 startPosition;
    private float startScaleX;

    [Header("Xp Bar")]
    //public GameObject XpBar;
    public TMP_Text levelText;
    public RectTransform XpBar;

    private float fullWidth;

    //private RectTransform rect;

    private void Awake() {
        //XpBar = GetComponent<RectTransform>();
    }    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*startPosition = XpBar.localPosition;
        startScaleX = XpBar.localScale.x;
        if (XpBar == null)
        {
//            Debug.LogError("XpBar is NOT assigned!");
        }
        getXpBarWidth(250,500);*/
        fullWidth = XpBar.sizeDelta.x;
        //updateXpBar();
        SetXpBarWidth(50f);
//        Debug.Log(fullWidth);
    }

    // Update is called once per frame
    void Update()
    {
        SetXpBarWidth(250f);
    }
    /*public void updateXpBar()
    {
        if (currentXP != 0)
        {
            prossentOfFullNeededXp = (float)currentXP / xpNeeded * 100;
            maxX = (float)maxX / 100 * prossentOfFullNeededXp;
            float NewWidth = (maxWidth / 100f) * prossentOfFullNeededXp;


            rect.sizeDelta = new Vector2(NewWidth, rect.sizeDelta.y);
        }
        
    }*/

    /*void getXpBarWidth(float current, float max)
    {
        float percent = current / max;

        // Shrink width
        Vector3 newScale = XpBar.localScale;
        newScale.x = startScaleX * percent;
        XpBar.localScale = newScale;

        // Reanchor to the left so it shrinks rightward
        Vector3 newPos = startPosition;
        newPos.x = startPosition.x - (startScaleX - newScale.x) / 2f;
        XpBar.localPosition = newPos;
    }*/

    void SetXpBarWidth(/*float current, float max*/ float percent)
    {
        /*float percent = current / max;

        Vector2 size = XpBar.sizeDelta;
        size.x = fullWidth * percent;
        XpBar.sizeDelta = size;*/
        Vector2 size = XpBar.sizeDelta;
        size.x = fullWidth * (percent / 100f);
        XpBar.sizeDelta = size;
    }
}
