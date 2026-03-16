using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerHit : MonoBehaviour
{
    public Collider2D attackHitbox;
    public SpriteRenderer attackSprite;
    public PlayerMovement playerMovement;

    public int hitboxDistance = 2;

    private bool isHitting = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isHitting = false;
        //GetComponent<Renderer>().enabled = false;
        attackHitbox.enabled = false; // Make sure it starts off
        attackSprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(PerformAttack());
        }
    }

    IEnumerator PerformAttack()
    {
        switch (playerMovement.facingDirection)
        {
            //Check for facing way, flip the x and y value based on facing way. Gets facing way from player movement.
            case "North": attackHitbox.transform.localPosition = new Vector3(0f,  hitboxDistance, 0f); break;
            case "South": attackHitbox.transform.localPosition = new Vector3(0f, -hitboxDistance, 0f); break;
            case "East":  attackHitbox.transform.localPosition = new Vector3( hitboxDistance, 0f, 0f); break;
            case "West":  attackHitbox.transform.localPosition = new Vector3(-hitboxDistance, 0f, 0f); break;
        }

        attackHitbox.enabled = true;
        attackSprite.enabled = true;
        yield return new WaitForSeconds(0.2f);
        attackHitbox.enabled = false;
        attackSprite.enabled = false;
    }
}
