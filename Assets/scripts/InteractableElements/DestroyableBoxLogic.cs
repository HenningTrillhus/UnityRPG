using UnityEngine;
using System.Collections;

public class DestroyableBoxLogic : MonoBehaviour
{
    public ParticleSystem particles;
    public Player_Inventory playerInventory;

    public string lootType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
            StartCoroutine(boxDestroyed());
            Debug.Log("hit");
        }
    }

    IEnumerator boxDestroyed()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        particles.Play();
        giveLoot();
        yield return new WaitForSeconds(1f);
        particles.Stop();

    }

    private void giveLoot()
    {
        if (lootType == "Seeds")
        {
            //playerInventory.inventoryV2.AddItemV2("Carrot Seed", 5); 
        }
    }
}
