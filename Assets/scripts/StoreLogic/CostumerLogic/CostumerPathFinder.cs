using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.InputSystem; // needed for the new Input System

public class CostumerPathFinder : MonoBehaviour
{
    public StoreAreaLogic Area;
    public GameObject Costumer;
    public float speed = 2f;

    public float positonX = 6f;
    public float positonY = 1f;

    //Real world Cord for 1,1
    public float realWorldOffsetX = -15.5f;
    public float realWorldOffsetY = 16.5f;

    private float PathFindingPositonX;
    private float PathFindingPositonY;

    private bool nextDirectionSwitch = true;

    public int tilesMovedSoFar = 0;
    private int NumberOfRetakes = 0;
    public int maxMovesAllowed = 50;
    public int numberOfFindPathTries = 60;
    private int numberOfFindPathTriesSoFar = 0;

    private int tilesMoved = 0;

    private bool isMoving = false;

    public int stopps = 5;
    private int stoppsTaken = 0;

    //public int[] stopps = 

    //int[] pathWayCheepest;
    //int[] currentPathWay;

    public class Cords
    {
        public float x;
        public float y;
    }

    public List<int> CheepestPathWay = new List<int>();
    public List<int> currentPathWay = new List<int>();
    public List<Cords> ListOfCordsToWalkTo = new List<Cords>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
        PathFindingPositonX = transform.position.x;
        PathFindingPositonY = transform.position.y;
        ListOfCordsToWalkTo.Add(new Cords {x = -8.5f, y = 21.5f});
        ListOfCordsToWalkTo.Add(new Cords {x = -11.5f, y = 18.5f});
        ListOfCordsToWalkTo.Add(new Cords {x = -13.5f, y = 21.5f});
        ListOfCordsToWalkTo.Add(new Cords {x = -5.5f, y = 17.5f});
        ListOfCordsToWalkTo.Add(new Cords {x = -9.5f, y = 17.5f});
        //StartCoroutine(MoveTo(new Vector3(positonX, positonY, 0)));
        
        
        
    }

    void Start() {
        creatPathwayTo(-8.5f,21.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.lKey.isPressed)
        {
            
//            Debug.LogWarning("Starting Pathfinding");
              //creatPathwayTo(7f,f);
        }
    }

    void setUpWalkPointList()
    {

    }

    //StartCoroutine(atStopp());

    IEnumerator atStopp()
    {
        if (stoppsTaken >= stopps)
        {
            Debug.Log("Done Moving");
        }
        else
        {
            yield return new WaitForSeconds(2f); // waits 2 seconds
            creatPathwayTo(ListOfCordsToWalkTo[stoppsTaken].x,ListOfCordsToWalkTo[stoppsTaken].y);
        }
    }

    bool IsBlocked(float x, float y)
    {
        foreach(StoreAreaLogic.Obstacle obstacle in Area.ListOfObstacles)
        {
            if (obstacle.x == x && obstacle.y == y)
            {
                return true;
            }
        }
        return false;
    }
    
    (bool north, bool south, bool west, bool east) checkForObsticales(float x, float y)
    {
        bool north = IsBlocked(x, y + 1);
        bool south = IsBlocked(x, y - 1);
        bool west  = IsBlocked(x - 1, y);
        bool east  = IsBlocked(x + 1, y);

        return (north, south, west, east);
    }

    IEnumerator MoveTo(Vector3 destination, int i)
    {
        while (Vector3.Distance(Costumer.transform.position, destination) > 0.01f)
        {
            Costumer.transform.position = Vector3.MoveTowards(
                Costumer.transform.position,
                destination,
                speed * Time.deltaTime
            );
            yield return null; // wait for next frame
        }

        Costumer.transform.position = destination; // snap exactly to end point
        if (tilesMoved >= CheepestPathWay.Count)
        {
            CheepestPathWay.Clear(); 
            currentPathWay.Clear(); 
            numberOfFindPathTriesSoFar = 0;
            PathFindingPositonX = Costumer.transform.position.x;
            PathFindingPositonY = Costumer.transform.position.y;
            //Debug.Log("Finished Moving, current postion is now : X " +PathFindingPositonX + "  Y " + PathFindingPositonY);
            isMoving = false;
            stoppsTaken ++;
            StartCoroutine(atStopp());
            
        }
        else if (tilesMoved < CheepestPathWay.Count)
        {
            StartMoving(CheepestPathWay[tilesMoved]);
        }
        
    }
    

    public void StartMoving(int direction)
    {
        tilesMoved ++;
        if (direction == 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f); 
            StartCoroutine(MoveTo(new Vector3(Costumer.transform.position.x, Costumer.transform.position.y + 1, 0), tilesMoved));
        }
        else if (direction == 1)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 270f); 
            StartCoroutine(MoveTo(new Vector3(Costumer.transform.position.x + 1, Costumer.transform.position.y, 0), tilesMoved));
        }
        else if (direction == 2)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 180f); 
            StartCoroutine(MoveTo(new Vector3(Costumer.transform.position.x, Costumer.transform.position.y - 1, 0), tilesMoved));
        }
        else if (direction == 3)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f); 
            StartCoroutine(MoveTo(new Vector3(Costumer.transform.position.x - 1, Costumer.transform.position.y, 0), tilesMoved));
        }

    }

    public void MoveToRandomTile(float GoalX, float GoalY)
    {
        (bool north, bool south, bool west, bool east) = checkForObsticales(PathFindingPositonX, PathFindingPositonY);

        int randomNextWay  = UnityEngine.Random.Range(0, 4);
        //Debug.Log(randomNextWay + "Is the random number    North: " +  north+ " east: " +  east + " south: " +  south+  " west: " +  west);
        if (randomNextWay == 0 && !north && PathFindingPositonY <= GoalY)
        {
            //Debug.Log("Moving North");
            PathFindingPositonY ++;
            tilesMovedSoFar++;
            currentPathWay.Add(0);
            //Debug.Log("Current postion is now : X " +PathFindingPositonX + "  Y " + PathFindingPositonY);
            creatPathwayTo(GoalX, GoalY);
        }
        else if (randomNextWay == 1 && !east && PathFindingPositonX <= GoalX)
        {
            //Debug.Log("Moving East");
            PathFindingPositonX ++;
            tilesMovedSoFar++;
            currentPathWay.Add(1);
            //Debug.Log("Current postion is now : X " +PathFindingPositonX + "  Y " + PathFindingPositonY);
            creatPathwayTo(GoalX, GoalY);
        }
        else if (randomNextWay == 2 && !south && PathFindingPositonY >= GoalY)
        {
            //Debug.Log("Moving South");
            PathFindingPositonY --;
            tilesMovedSoFar++;
            currentPathWay.Add(2);
            //Debug.Log("Current postion is now : X " +PathFindingPositonX + "  Y " + PathFindingPositonY);
            creatPathwayTo(GoalX, GoalY);
        }
        else if (randomNextWay == 3 && !west && PathFindingPositonX >= GoalX)
        {
            //Debug.Log("Moving West");
            PathFindingPositonX --;
            tilesMovedSoFar++;
            currentPathWay.Add(3);
            //Debug.Log("Current postion is now : X " +PathFindingPositonX + "  Y " + PathFindingPositonY);
            creatPathwayTo(GoalX, GoalY);
        }
        else
        {
            NumberOfRetakes++;
            if(NumberOfRetakes > 5)
            {
                //Debug.LogWarning("Too many retakes, stopping");
                resetPathFindingPostion(GoalX, GoalY);
            }
            else
            {
                //Debug.LogWarning("Retakes number was " +  randomNextWay + " North: " +  north+ " east: " +  east + " south: " +  south+  " west: " +  west);
                MoveToRandomTile(GoalX,GoalY);
            }
            
        }
    }

    void resetPathFindingPostion(float x, float y)
    {
        PathFindingPositonX = transform.position.x;
        PathFindingPositonY = transform.position.y;
        tilesMovedSoFar = 0;
        NumberOfRetakes = 0;
        numberOfFindPathTriesSoFar++;
        currentPathWay.Clear(); 
        creatPathwayTo(x, y);
    }

    public void creatPathwayTo(float x, float y)
    {
        //Debug.Log("Trying to find path to X: " + x + " Y: " + y + "   Try number: " + numberOfFindPathTriesSoFar + "   Current Pathway length: " + currentPathWay.Count + "   Cheapest Pathway length: " + CheepestPathWay.Count + "current postion is now : X " +PathFindingPositonX + "  Y " + PathFindingPositonY);
        //Check for how many times we have tried to find a path.
        
        if (numberOfFindPathTriesSoFar <= numberOfFindPathTries)
        {
            //Checks for how many moves so far.
            if (tilesMovedSoFar > maxMovesAllowed)
            {
                //Debug.LogWarning("Too many moves, Starting over");
                resetPathFindingPostion(x, y);
            }
            NumberOfRetakes = 0;
            //Debug.Log(PathFindingPositonX + "    " +  PathFindingPositonY + " looking for " + x + "  " + y);
            if (PathFindingPositonX == x && PathFindingPositonY == y)
            {
                tilesMovedSoFar = 0;
                if (CheepestPathWay.Count == 0)
                {
                    //Copyes the current path to the cheapest path becuse there is no other path to compare with.
                    CheepestPathWay.Clear();    
                    CheepestPathWay.AddRange(currentPathWay);
                    resetPathFindingPostion(x, y);
                    
                }
                else
                {
                    if (currentPathWay.Count < CheepestPathWay.Count)
                    {
                        //if the current path is cheeper then the cheapest path, copy the current path to the cheapest path.
                        CheepestPathWay.Clear();    
                        CheepestPathWay.AddRange(currentPathWay);
                        resetPathFindingPostion(x, y);
                    }
                    else
                    {
                        //If new path i longer just remove it and try again.    
                        resetPathFindingPostion(x, y);
                    }
                }
            }
            else if (PathFindingPositonX != x || PathFindingPositonY != y)
            {
                MoveToRandomTile(x, y);
            }
        }
        else
        {
            if (!isMoving /*&& CheepestPathWay.Count != 0*/)
            {
                //Debug.Log("Found the Best path at " + CheepestPathWay.Count + " moves");
                tilesMoved = 0;
                isMoving = true;
                StartMoving(CheepestPathWay[0]);
                return;
            }
            else if (CheepestPathWay.Count == 0)
            {
                Debug.LogWarning("Cant reach it");
            }
        }
    }
}


//get direction not based on goal. check if now potion is same as goal. 
