using UnityEngine;

public class BushInteraction : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Sprite newSprite;
    private SpriteRenderer sr;
    public Player_Inventory playerInventory;
    public GameObject interactionSprite;
    private bool playerNear = false;
    private bool hasBerry = true;

    void Start()
    {
        interactionSprite.SetActive(false);
        hasBerry = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E) && hasBerry)
        {
            playerInteractWithBush();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactionSprite.SetActive(true);
            playerNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactionSprite.SetActive(false);
            playerNear = false; 
        }
    }

    void playerInteractWithBush(){
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = newSprite;
        playerInventory.inventoryV2.AddItemV2("Berry", 5);
        hasBerry = false;
    }
}
