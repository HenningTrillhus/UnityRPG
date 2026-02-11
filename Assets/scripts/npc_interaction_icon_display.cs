using UnityEngine;

public class npc_interaction_icon_display : MonoBehaviour
{
    public GameObject interactionIcon;

    void Start()
    {
        interactionIcon.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactionIcon.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactionIcon.SetActive(false);
        }
    }
}
