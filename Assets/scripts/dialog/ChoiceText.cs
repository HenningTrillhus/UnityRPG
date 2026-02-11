using UnityEngine;
using TMPro;

public class ChoiceText : MonoBehaviour
{
    public TMP_Text text;   // assign in Inspector

    private bool selected = true;

    // colors
    public Color normalColor = Color.white;
    public Color blinkColor = Color.yellow;

    void Update()
    {
        if (selected)
        {
            float t = Mathf.PingPong(Time.unscaledTime * 2f, 1f); // 2 = speed
            text.color = Color.Lerp(normalColor, blinkColor, t);
        }
        else
        {
            text.color = normalColor;
        }
    }

    /*public void SetText(string t)
    {
        text.text = t;
    }

    public void SetSelected(bool isSelected)
    {
        selected = isSelected;
    }*/
}
