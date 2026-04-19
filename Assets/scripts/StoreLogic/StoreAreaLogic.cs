using UnityEngine;
using System.Collections.Generic;

public class StoreAreaLogic : MonoBehaviour
{
    public float storeAreaWidth = 10f;
    public float storeAreaHight = 6f;


    public float wallStartWidthX = -14.5f;
    public float wallStartWidthY = 16.5f;
    public float wallStartHightX = -15.5f;
    public float wallStartHightY = 17.5f;

    public class Obstacle
    {
        public string Type;
        public float x;
        public float y;
    }

    public List<Obstacle> ListOfObstacles = new List<Obstacle>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ListOfObstacles.Add(new Obstacle {Type = "Shelf", x = 6f, y = 4f});
        //ListOfObstacles.Add(new Obstacle {Type = "Shelf", x = 3f, y = 2f});
        //ListOfObstacles.Add(new Obstacle {Type = "Shelf", x = 8f, y = 3f});
        //ListOfObstacles.Add(new Obstacle {Type = "Shelf", x = 7f, y = 2f});
        //ListOfObstacles.Add(new Obstacle {Type = "Shelf", x = 4f, y = 5f});
        

    }

    // Update is called once per frame
    void Update()
    {}

    void Awake() {
        creatObstacleWallAroundStore();
    }

    public void addObstacle(string type ,float x, float y)
    {
        ListOfObstacles.Add(new Obstacle {Type = type, x = x, y = y});
    }

    void creatObstacleWallAroundStore()
    {
        for (float i = 0; i < storeAreaWidth; i++)
        {
            ListOfObstacles.Add(new Obstacle {Type = "Wall", x = wallStartWidthX + (1*i), y = wallStartWidthY});
        }
        for (float i = 0; i < storeAreaHight; i++)
        {
            ListOfObstacles.Add(new Obstacle {Type = "Wall", x = wallStartHightX, y = wallStartHightY + (1*i)});
        }
    }
}
