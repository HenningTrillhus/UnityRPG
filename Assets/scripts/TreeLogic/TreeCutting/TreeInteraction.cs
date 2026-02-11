using UnityEngine;

public class TreeInteraction : MonoBehaviour
{
    public GameObject interactionSprite;
    public GameObject miniGameUI; // assign your sweet spot minigame UI here
    private bool playerNear = false;

    void Start()
    {
        interactionSprite.SetActive(false);
        miniGameUI.SetActive(false);
    }

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            OpenMiniGame();
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

    void OpenMiniGame()
    {
        if (!InventoryUI.IsOpen)
        {
            // Show the bar UI
            miniGameUI.SetActive(true);

            // Freeze the game
            GameManager.Instance.OpenUI(miniGameUI);

            // Hide interaction sprite
            interactionSprite.SetActive(false);

            // Set the target tree for the minigame
            MinigameBar bar = miniGameUI.GetComponent<MinigameBar>();
            bar.targetTree = GetComponent<TreeHP>();
        }
    }

    // Call this when mini game ends
    public void CloseMiniGame()
    {
        miniGameUI.SetActive(false);
        GameManager.Instance.CloseUI(miniGameUI);
        interactionSprite.SetActive(true);
    }
}
