using UnityEngine;

public class MousePosition : MonoBehaviour
{
    public Transform HowerBox;
    public Transform Player;
    public GameObject TestTile;

    public float ReachRangeForHowerBox = 4f;

    public bool howerBoxVisible = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get mouse position in world space (what you want for 2D games)
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // always set z to 0 for 2D

        float mouseX = Mathf.FloorToInt(mousePos.x) + 0.5f;
        float mouseY = Mathf.FloorToInt(mousePos.y) + 0.5f;

        
        if (Vector2.Distance(Player.position, mousePos) < ReachRangeForHowerBox)
        {
            HowerBox.transform.position = new Vector3(mouseX, mouseY, 0f);
            HowerBox.GetComponent<SpriteRenderer>().enabled = true;
            howerBoxVisible = true;

        }
        else{
            HowerBox.GetComponent<SpriteRenderer>().enabled = false;
            howerBoxVisible = false;
        }

        if (Input.GetMouseButtonDown(0) && howerBoxVisible)
        {
            Instantiate(TestTile, new Vector3(mouseX, mouseY, 0f), Quaternion.identity);
        }
    }
}
