using UnityEngine;
using System;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class TickLogic : MonoBehaviour
{
    public TextMeshProUGUI TickTextBox;

    public float tickSpeed = 1f;
    private float tickRate = 0.5f;
    private int currentTick = 0;

    public static event Action OnTick;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(TickLoop());
    }

    IEnumerator TickLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(tickRate / tickSpeed);
            currentTick++;
            OnTick?.Invoke();
            OnTickTotal();
        }
    }

    void OnTickTotal()
    {
        TickTextBox.text = "Tick: " + currentTick.ToString();
        
        //Debug.Log("Tick: " + currentTick);
        // everything that needs to update per tick goes here
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
