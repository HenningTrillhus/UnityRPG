using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CropPlacementLogic : MonoBehaviour
{
    public MousePosition PosData;


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool checkTiles(float x, float y)
    {
        
        if (PosData.cropPositions.Count!= 0)
        {
            foreach (MousePosition.CropPos pos in PosData.cropPositions)
            {
                if (x == pos.x && y == pos.y){
                    return true;
                }
            }
            return false;
            
        }
        else
        {
            return false;
        }
    }

    public bool[] checkNeighbourTiles(float x, float y)
    {
        bool[] neighbours = new bool[8];
        
        neighbours[0] = checkTiles(x-1, y+1); // top left
        neighbours[1] = checkTiles(x,   y+1); // top
        neighbours[2] = checkTiles(x+1, y+1); // top right
        neighbours[3] = checkTiles(x-1, y);   // left
        neighbours[4] = checkTiles(x+1, y);   // right
        neighbours[5] = checkTiles(x-1, y-1); // bottom left
        neighbours[6] = checkTiles(x,   y-1); // bottom
        neighbours[7] = checkTiles(x+1, y-1); // bottom right

        return neighbours;
    }
}
