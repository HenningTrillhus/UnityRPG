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

    private float PathFindingPositonX;
    private float PathFindingPositonY;

    private bool nextDirectionSwitch = true;

    public int tilesMovedSoFar = 0;
    private int NumberOfRetakes = 0;
    public int maxMovesAllowed = 50;
    public int numberOfFindPathTries = 10;
    private int numberOfFindPathTriesSoFar = 0;
    private int idealPathLength;

    public float PauseTimeAtStops = 2f;

    private bool errorCode1 = false;
    public bool errorCode1ActiveInThisAttempt = false;
    public int numberOfFindPathTriesWithErrorCode1 = 10;
    
    // Track moves away from goal to prevent infinite loops when navigating obstacles

    private int tilesMoved = 0;

    private bool isMoving = false;

    public int stopps = 5;
    private int stoppsTaken = 0;

    private int LastStop;
    

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
        ListOfCordsToWalkTo.Add(new Cords {x = -14.5f, y = 17.5f});
        ListOfCordsToWalkTo.Add(new Cords {x = -6.5f, y = 22.5f});
        ListOfCordsToWalkTo.Add(new Cords {x = -10.5f, y = 22.5f});
        //StartCoroutine(MoveTo(new Vector3(positonX, positonY, 0)));
        
        
        
    }

    void Start() {
        //creatPathwayTo(-8.5f,21.5f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    int getRandomNextStop()
    {
        int randomNextStop = UnityEngine.Random.Range(0, 7);
        if (randomNextStop == LastStop)
        {
            return getRandomNextStop();
        }
        else
        {
            LastStop = randomNextStop;
            return randomNextStop;
        }
    }

    IEnumerator atStopp()
    {
        if (stoppsTaken >= stopps)
        {
            Debug.Log("Done Moving");
        }
        else
        {
            stoppsTaken--;
            yield return new WaitForSeconds(PauseTimeAtStops); // waits 2 seconds
            int randomNextStop = getRandomNextStop();
            Debug.Log("Moving to stop " + (randomNextStop + 1));
            creatPathwayTo(ListOfCordsToWalkTo[randomNextStop].x,ListOfCordsToWalkTo[randomNextStop].y);
        }
    }

    bool IsBlocked(float x, float y)
    {
        //Round to closest int 

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
        if (north && south && west && east)
        {
            Debug.LogWarning("All sides are blocked, ahh shit");
        }
        return (north, south, west, east);
    }

    /// Checks if the direct path to the goal is possible without obstacles blocking the direct route

    bool IsDirectPathPossible(float GoalX, float GoalY)
    {
        // Calculate which directions we need to move
        bool needMoveNorth = PathFindingPositonY < GoalY;
        bool needMoveSouth = PathFindingPositonY > GoalY;
        bool needMoveEast = PathFindingPositonX < GoalX;
        bool needMoveWest = PathFindingPositonX > GoalX;

        // Check if the direct path directions are blocked
        (bool north, bool south, bool west, bool east) = checkForObsticales(PathFindingPositonX, PathFindingPositonY);

        // If we need to move north but north is blocked
        if (needMoveNorth && north)
            return false;
        // If we need to move south but south is blocked
        if (needMoveSouth && south)
            return false;
        // If we need to move east but east is blocked
        if (needMoveEast && east)
            return false;
        // If we need to move west but west is blocked
        if (needMoveWest && west)
            return false;

        return true;
    }

    IEnumerator MoveTo(Vector3 destination, int i)
    {
        while (Vector3.Distance(Costumer.transform.position, destination) > 0.001f)
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
            errorCode1 = false;
            errorCode1ActiveInThisAttempt = false;
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

    void compearCurrentPathToCheepestPath(float GoalX, float GoalY)
    {
        if (currentPathWay.Count < CheepestPathWay.Count || CheepestPathWay.Count == 0)
        {
            //if the current path is cheeper then the cheapest path, copy the current path to the cheapest path.
            CheepestPathWay.Clear();    
            CheepestPathWay.AddRange(currentPathWay);
            resetPathFindingPostion(GoalX, GoalY);
        }
        else
        {
            //If new path i longer just remove it and try again.    
            resetPathFindingPostion(GoalX, GoalY);
        }
    }

    public void MoveToRandomTile(float GoalX, float GoalY)
    {

        for (int i = 0; i < maxMovesAllowed; i++)
        {
            
            //checks if path found the goal.
            /*if (PathFindingPositonX == GoalX && PathFindingPositonY == GoalY)
            {
                compearCurrentPathToCheepestPath(GoalX, GoalY);
                return;
            }*/


            //Check if the path is found using Approximatly function to avoid edge cases with float
            if (Mathf.Approximately(PathFindingPositonX, GoalX) && Mathf.Approximately(PathFindingPositonY, GoalY))
            {
                return; // found the goal, done
            }

            //Checks surrounding tiles for obsticales
            (bool north, bool south, bool west, bool east) = checkForObsticales(PathFindingPositonX, PathFindingPositonY);
            //Creat list of non blocked directions 
            List<int> validDirections = new List<int>();

            //Checks if direction is vallied based on if its blocked, then if it is smart/fastes or if error code 1 is active and it has to move around an object.
            if (!north  && (errorCode1 || PathFindingPositonY <= GoalY)) validDirections.Add(0);
            if (!east   && (errorCode1 || PathFindingPositonX <= GoalX)) validDirections.Add(1);
            if (!south  && (errorCode1 || PathFindingPositonY >= GoalY)) validDirections.Add(2);
            if (!west   && (errorCode1 || PathFindingPositonX >= GoalX)) validDirections.Add(3);

            if (validDirections.Count == 0)
            {
                Debug.LogWarning("Costumer is completely surrounded, aborting path attempt");
                return;
            }

            //Gets a random number between 0 and the number of valid directions.
            int chosenDirection = validDirections[UnityEngine.Random.Range(0, validDirections.Count)];
            

            //Moves the imagenary position in the chosen direction and adds the direction to the current path.
            switch (chosenDirection)
            {
                case 0: PathFindingPositonY++; break;
                case 1: PathFindingPositonX++; break;
                case 2: PathFindingPositonY--; break;
                case 3: PathFindingPositonX--; break;
            }
            currentPathWay.Add(chosenDirection);


            //If it has not found a path and has went trough a certain number of tries, activate error code 1, to move more freely.
            if (i > maxMovesAllowed - numberOfFindPathTriesWithErrorCode1 && !errorCode1)
            {
                Debug.LogWarning("Activating error code 1, allowing more random movement to find a path");
                errorCode1 = true;
                errorCode1ActiveInThisAttempt = true;
            }

            //First check if the way is open then if it smart, then if errorcode1 is active then forget about being smart. 
            //means it has to move around object.
            /*if (randomNextWay == 0 && !north && PathFindingPositonY <= GoalY || randomNextWay == 0 && !north && errorCode1)
            {
                PathFindingPositonY ++;
                currentPathWay.Add(0);
            }
            else if (randomNextWay == 1 && !east && PathFindingPositonX <= GoalX || randomNextWay == 1 && !east && errorCode1)
            {
                PathFindingPositonX ++;
                currentPathWay.Add(1);
            }
            else if (randomNextWay == 2 && !south && PathFindingPositonY >= GoalY || randomNextWay == 2 && !south && errorCode1)
            {
                PathFindingPositonY --;
                currentPathWay.Add(2);
            }
            else if (randomNextWay == 3 && !west && PathFindingPositonX >= GoalX || randomNextWay == 3 && !west && errorCode1)
            {
                PathFindingPositonX --;
                currentPathWay.Add(3);
            }*/
            
            
        }
        //If it has not found a path after max moves allowed, reset and try again, up to a certain number of tries.
        /*if (PathFindingPositonX != GoalX || PathFindingPositonY != GoalY)
        {
            resetPathFindingPostion(GoalX, GoalY);
        }
        //Only happens if found path on last try, if i dont have this line its an edge case that stops the movement
        else if (PathFindingPositonX == GoalX && PathFindingPositonY == GoalY){
            compearCurrentPathToCheepestPath(GoalX, GoalY);
        }*/
    }

    void resetPathFindingPostion(float x, float y)
    {
        numberOfFindPathTriesSoFar++;
        //Reset the imagenary position to the start position 
        PathFindingPositonY = transform.position.y;
        PathFindingPositonX = transform.position.x;
        //Clean the current path to be used again.
        currentPathWay.Clear(); 
        creatPathwayTo(x, y);
    }

    public void creatPathwayTo(float x, float y)
    {
        //Calculate the ideal path length using Manhattan distance
        idealPathLength = (int)(Mathf.Abs(transform.position.x - x) + Mathf.Abs(transform.position.y - y));
        numberOfFindPathTriesSoFar = 0;
        errorCode1 = false;
        CheepestPathWay.Clear();
        while (numberOfFindPathTriesSoFar <= numberOfFindPathTries)
        {
            PathFindingPositonX = transform.position.x;
            PathFindingPositonY = transform.position.y;
            currentPathWay.Clear();

            MoveToRandomTile(x, y);

            // Compare after each attempt
            if (currentPathWay.Count < CheepestPathWay.Count || CheepestPathWay.Count == 0)
            {
                CheepestPathWay.Clear();
                CheepestPathWay.AddRange(currentPathWay);
                if (CheepestPathWay.Count <= idealPathLength)
                {
                    Debug.Log("Found an optimal path! Ideal: " + idealPathLength + ", Actual: " + CheepestPathWay.Count);
                    break; // Found an optimal path, no need to continue
                }
            }

            numberOfFindPathTriesSoFar++;

            errorCode1 = false;
        }
        if (CheepestPathWay.Count != 0)
        {
            tilesMoved = 0;
            isMoving = true;
            StartMoving(CheepestPathWay[0]);
        }
        else
        {
            Debug.LogWarning("Could not find any path after all attempts");
        }






        //Checks if max number of tries is reached, i want it to go trough all of them. 
        /*if (numberOfFindPathTriesSoFar <= numberOfFindPathTries)
        {
            //If it havent gone trough them all yet run a new round.
            MoveToRandomTile(x, y);
        }
        else if (CheepestPathWay.Count != 0)
        {
            tilesMoved = 0;
            isMoving = true;
            StartMoving(CheepestPathWay[0]);
        }*/

    }
}


//get direction not based on goal. check if now potion is same as goal. 
