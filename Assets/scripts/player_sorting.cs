using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class player_sorting : MonoBehaviour
{
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        // Set sorting order based on Y position
        sr.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100)+1;
    }
}
