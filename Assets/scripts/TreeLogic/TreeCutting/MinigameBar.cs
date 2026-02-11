using UnityEngine;
using UnityEngine.UI;

public class MinigameBar : MonoBehaviour
{
    public RectTransform sweetSpot;  // green rectangle
    public float speed = 200f;       // pixels per second
    public float perfectZoneWidth = 60f; // pixels, the “sweet spot” in middle

    public TreeHP targetTree;   // assign the tree currently being chopped
    public int perfectHitDamage = 2;
    public int normalHitDamage = 0;

    public float hitCooldown = 0.5f; // 0.5 seconds between hits
    private float lastHitTime = -10f; // stores last hit time


    private float direction = 1f;
    private float halfBarWidth;

    void Start()
    {
        if (sweetSpot == null)
        {
            Debug.LogWarning("SweetSpot not assigned!");
            return;
        }

        RectTransform barRect = GetComponent<RectTransform>();
        halfBarWidth = barRect.rect.width / 2 - sweetSpot.rect.width / 2;
    }

    void Update()
    {
        if (sweetSpot == null) return;

        // Move sweet spot using unscaled delta time
        float newX = sweetSpot.localPosition.x + direction * speed * Time.unscaledDeltaTime;

        if (newX > halfBarWidth)
        {
            newX = halfBarWidth;
            direction *= -1;
        }
        else if (newX < -halfBarWidth)
        {
            newX = -halfBarWidth;
            direction *= -1;
        }

        sweetSpot.localPosition = new Vector3(newX, sweetSpot.localPosition.y, sweetSpot.localPosition.z);

        // Check for player pressing Space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Check cooldown
            if (Time.unscaledTime - lastHitTime >= hitCooldown)
            {
                CheckHit();
                lastHitTime = Time.unscaledTime; // update last hit time
            }
            else
            {
                Debug.Log("Hit on cooldown");
            }
        }
    }


    void CheckHit()
    {
        float xPos = sweetSpot.localPosition.x;

        if (Mathf.Abs(xPos) <= perfectZoneWidth / 2)
        {
            Debug.Log("Perfect Hit!");
            if (targetTree != null) targetTree.TakeDamage(perfectHitDamage);
        }
        else
        {
            Debug.Log("Miss or Partial Hit");
            if (targetTree != null) targetTree.TakeDamage(normalHitDamage);
        }
    }
}
