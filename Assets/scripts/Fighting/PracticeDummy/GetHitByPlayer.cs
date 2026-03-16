using UnityEngine;

public class GetHitByPlayer : MonoBehaviour
{
    public StartQuest questDialogLogic;
    public GameObject HealthBar;
    
    public Transform healthBarFill;
    private Vector3 startPosition;
    public SpriteRenderer dummySprite;
    public Sprite deadSprite; 
    private float startScaleX;

    private float maxHealth = 100f;
    private float health = 100f;
    private bool isAlive = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = healthBarFill.localPosition;
        startScaleX = healthBarFill.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check by Tag (simplest approach)
        if (other.CompareTag("AttackHitBox"))
        {
            if (isAlive){
                TakeDamage(10);
            }
        }
    }

    public void SetHealth(float current, float max)
    {
        float percent = current / max;

        // Shrink width
        Vector3 newScale = healthBarFill.localScale;
        newScale.x = startScaleX * percent;
        healthBarFill.localScale = newScale;

        // Reanchor to the left so it shrinks rightward
        Vector3 newPos = startPosition;
        newPos.x = startPosition.x - (startScaleX - newScale.x) / 2f;
        healthBarFill.localPosition = newPos;
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        SetHealth(health, maxHealth);

        if (health <= 0 && isAlive) Die();
    }

    void Die()
    {
        dummySprite.sprite = deadSprite; 
        isAlive = false;
        HealthBar.SetActive(false);    
        questDialogLogic.enemyKilled("Dummy");
    }
}
