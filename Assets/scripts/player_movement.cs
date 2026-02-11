using UnityEngine;
using UnityEngine.InputSystem; // needed for the new Input System

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 movement;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input from the new system
        if (Keyboard.current != null)
        {
            movement.x = 0;
            movement.y = 0;

            if (Keyboard.current.wKey.isPressed) movement.y += 1;
            if (Keyboard.current.sKey.isPressed) movement.y -= 1;
            if (Keyboard.current.aKey.isPressed) movement.x -= 1;
            if (Keyboard.current.dKey.isPressed) movement.x += 1;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
