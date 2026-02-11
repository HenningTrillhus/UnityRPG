using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TreeSorting : MonoBehaviour
{
    public Transform player;      // assign the player in Inspector
    public float baseY;           // the Y position where player goes behind the tree
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        if (player == null) return;

        if (player.position.y < baseY)
        {
            // Player is below the tree base → player in front
            sr.sortingOrder = Mathf.RoundToInt(player.GetComponent<SpriteRenderer>().sortingOrder) - 1;
        }
        else
        {
            // Player is above the tree base → tree in front
            sr.sortingOrder = Mathf.RoundToInt(player.GetComponent<SpriteRenderer>().sortingOrder) + 1;
        }
    }
}
