using UnityEngine;
using TMPro;

public class TreeHP : MonoBehaviour
{
    public TMP_Text damageText; // Reference to the damage text UI element

    public int maxHP = 5;
    private int currentHP;

    public ItemData woodItem; // drag your Wood asset here in Inspector
    public Player_Inventory playerInventory;

    public float damageVisibleTime = 0.5f; // 0.5 seconds between hits
    private float lastDamageTime = -10f; // stores last hit time

    // Reference to the interaction script
    public TreeInteraction treeInteraction;

    void Start()
    {
        currentHP = maxHP;
        damageText.gameObject.SetActive(false); // Hide damage text at start
    }

    private void Update() {
        if (Time.unscaledTime - lastDamageTime >= damageVisibleTime)
        {
            damageText.gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        //Debug.Log(gameObject.name + " took " + amount + " damage. HP: " + currentHP);
        damageText.text = amount.ToString(); // Update damage text
        damageText.gameObject.SetActive(true); // Show damage text when taking damage
        lastDamageTime = Time.unscaledTime;
        if (currentHP <= 0)
        {
            ChopDown();
        }
    }

    void ChopDown()
    {
        Debug.Log(gameObject.name + " chopped down!");

        // Give player wood
        Player_Inventory playerInv = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Inventory>();
        if (playerInv != null && woodItem != null)
        {
            playerInventory.inventoryV2.AddItemV2(woodItem, 5);
            //Debug.Log("Player received " + 5 + " wood!");
        }

        // Close the minigame UI if open
        if (treeInteraction != null)
        {
            treeInteraction.CloseMiniGame();
        }

        // Destroy the tree
        Destroy(gameObject);
    }
}
