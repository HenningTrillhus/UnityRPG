using UnityEngine;
using System.Collections.Generic;

public class ShelfInventoryManager : MonoBehaviour
{
    public static ShelfInventoryManager Instance { get; private set; }

    public class ShelfData
    {
        public string[] itemNames;
        public Vector3[] shelfPosition;
    }

    public List<ShelfData> allShelfData = new List<ShelfData>();

    void Awake()
    {
        //Creat instence/Refrence of logic destroy if existing one is found
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetShelfIDByPosition(Vector3 position)
    {
        for (int i = 0; i < allShelfData.Count; i++)
        {
            if (allShelfData[i].shelfPosition[0] == position)
            {
                return i + 1; // Return the shelf ID (index + 1)
            }
        }
        return -1; // Return -1 if no matching shelf is found
    }

    public void addItemToShelf(int shelfId, string itemName)
    {
        // Find the shelf data with the matching ID
        int index = shelfId - 1; // Assuming shelfId starts from 1 and corresponds to the index in the list
        if (index >= 0 && index < allShelfData.Count)
        {
            // Add the item name to the shelf's itemNames array of the matching shelf id bassed on the index
            List<string> itemList = new List<string>(allShelfData[index].itemNames);
            itemList.Add(itemName);
            allShelfData[index].itemNames = itemList.ToArray();
            Debug.Log("Added item " + itemName + " to shelf ID " + shelfId);
        }
        else
        {
            Debug.LogError($"Shelf with ID {shelfId} not found.");
        }
    }

    public void AddShelfData(Vector3 position)
    {
        //Adding new empty shelf to list
        ShelfData newShelfData = new ShelfData
        {
            // Assign a unique ID based on the current count of shelves
            itemNames = new string[0],
            shelfPosition = new Vector3[] { position }
        };
        allShelfData.Add(newShelfData);
        Debug.Log("           " + newShelfData.shelfPosition[0]);
    }
}
