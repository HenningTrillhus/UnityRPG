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
    
    public void removeItemFromShelf(int shelfId, string itemName)
    {
        // Find the shelf data with the matching ID
        int index = shelfId - 1; // Assuming shelfId starts from 1 and corresponds to the index in the list
        if (index >= 0 && index < allShelfData.Count)
        {
            // Remove the item name from the shelf's itemNames array of the matching shelf id bassed on the index
            List<string> itemList = new List<string>(allShelfData[index].itemNames);
            if (itemList.Remove(itemName))
            {
                allShelfData[index].itemNames = itemList.ToArray();
                Debug.Log("Removed item " + itemName + " from shelf ID " + shelfId);
            }
            else
            {
                Debug.LogError($"Item {itemName} not found on shelf ID {shelfId}.");
            }
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

    public Vector3 findShelfToMoveTo(string itemName)
    {
        for (int i = 0; i < allShelfData.Count; i++)
        {
            if (System.Array.Exists(allShelfData[i].itemNames, name => name == itemName))
            {
                Debug.Log("Found shelf with item " + itemName + " at position: " + allShelfData[i].shelfPosition[0]);
                // Move the costumer to the shelf's position
                // You can implement the movement logic here, for example:
                // costumer.transform.position = allShelfData[i].shelfPosition[0];
                return allShelfData[i].shelfPosition[0];
            }
        }
        Debug.Log("No shelf found with item " + itemName);
        return Vector3.zero; // Return a default position if no shelf is found
    }
}
