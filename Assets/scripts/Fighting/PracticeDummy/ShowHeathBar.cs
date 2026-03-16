using UnityEngine;

public class ShowHeathBar : MonoBehaviour
{
    public Transform player;
    public GameObject HealthBar;

    private bool playerNearby = false;

    public float showDistance = 34; // Tweak this in Inspector

    void Start()
    {
        HealthBar.SetActive(false);
    }

     

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance < showDistance)HealthBar.SetActive(true);
        else HealthBar.SetActive(false);
    }
}
