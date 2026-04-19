using UnityEngine;

public class ThisIsAnObstacle : MonoBehaviour
{
    public StoreAreaLogic Area;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        float x = (float)transform.position.x;
        float y = (float)transform.position.y;
        Area.addObstacle("Shelf", x, y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
