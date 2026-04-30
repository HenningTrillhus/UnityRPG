using UnityEngine;
using System;
using TMPro;
using System.Collections.Generic;


public class CostumerStates : MonoBehaviour
{
    public GameObject hoverBox;
    public GameObject errorCode1Symbol;
    public CostumerSpawner CostumerSpawner;
    public CostumerPathFinder CostumerPathfinding;

    
    
    public string costumerName;
    public string costumerClass;

    private int[] listOfPosibleItemsID;
    private List<string> listOfItemsLookingfor = new List<string>();
    private int randomNumberForItem1;
    private int randomNumberForItem2;
    private int randomNumberForItem3;

    private int StoppsToTake = 3;

    public float hoverDistance = 0.5f;

    [SerializeField] private TextMeshPro costumerNameBox;
    [SerializeField] private TextMeshPro costumerClassBox;

    [SerializeField] private SpriteRenderer lookingForItemSprite1;
    [SerializeField] private SpriteRenderer lookingForItemSprite2;
    [SerializeField] private SpriteRenderer lookingForItemSprite3;

    string[] farmerNames = {
    "Edmund Hayward",   "Aldric Fieldson",  "Oswin Millward",   "Godwin Thatcher",  "Leofric Shepherd",
    "Hilda Hayward",    "Mildred Fieldson",  "Edith Millward",   "Aelswith Thatcher", "Wulfrun Shepherd"
    };

    string[] minerNames = {
    "Brom Stoneback",   "Durwin Pickford",  "Grimwald Ironson",  "Thorbert Coalward", "Aldwin Deepstone",
    "Sigrid Stoneback", "Brunhild Pickford", "Gudrun Ironson",   "Helga Coalward",    "Marta Deepstone"
    };

    string[] lumberjackNames = {
    "Wulfgar Woodson",  "Ragnar Timberfall", "Bjorn Ashford",   "Eldric Oakward",    "Osbert Logsworth",
    "Ingrid Woodson",   "Astrid Timberfall", "Ragnhild Ashford", "Thyra Oakward",     "Bergit Logsworth"
    };

    void Awake()
    {
        costumerClass = getClass();
        if (costumerClass == "Farmer")
        {
            costumerName = farmerNames[UnityEngine.Random.Range(0, farmerNames.Length)];
        }
        if (costumerClass == "Miner")
        {
            costumerName = minerNames[UnityEngine.Random.Range(0, minerNames.Length)];
        }
        if (costumerClass == "Lumberjack")
        {
            costumerName = lumberjackNames[UnityEngine.Random.Range(0, lumberjackNames.Length)];
        }   
    }
    
    //Toughts-------------
    //At Spawn: Get class (Farmer, ...) based on likings select 5+- things the costumer wants look trough list of shelfs for where costumer need to go.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        listOfPosibleItemsID = new int[] { 7, 12, 13, 14, 15, 16, 17 };
        
        errorCode1Symbol.SetActive(false);
        hoverBox.SetActive(false);
        costumerNameBox.text = costumerName;
        costumerClassBox.text = costumerClass;
        randomNumberForItem1 = UnityEngine.Random.Range(0, listOfPosibleItemsID.Length);
        randomNumberForItem2 = UnityEngine.Random.Range(0, listOfPosibleItemsID.Length);
        randomNumberForItem3 = UnityEngine.Random.Range(0, listOfPosibleItemsID.Length);
        listOfItemsLookingfor.Add(CostumerSpawner.getNameByID(listOfPosibleItemsID[randomNumberForItem1]));
        listOfItemsLookingfor.Add(CostumerSpawner.getNameByID(listOfPosibleItemsID[randomNumberForItem2]));
        listOfItemsLookingfor.Add(CostumerSpawner.getNameByID(listOfPosibleItemsID[randomNumberForItem3]));


        lookingForItemSprite1.sprite = CostumerSpawner.getSpriteByID(listOfPosibleItemsID[randomNumberForItem1]);
        lookingForItemSprite2.sprite = CostumerSpawner.getSpriteByID(listOfPosibleItemsID[randomNumberForItem2]);
        lookingForItemSprite3.sprite = CostumerSpawner.getSpriteByID(listOfPosibleItemsID[randomNumberForItem3]);
        
    }

    private string getClass()
    {
        int randomInt = UnityEngine.Random.Range(0, 3);  
        if (randomInt == 0)
        {
            return "Farmer";
        }
        if (randomInt == 1)
        {
            return "Miner";
        }
        if (randomInt == 2){
            return "Lumberjack";
        }
        if (randomInt == 3)
        {
            return "Priest";
        }

        return "Farmer";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            findShelfToMoveTo();
        }
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        float distance = Vector2.Distance(mousePos, hoverBox.transform.position);

        if (distance < hoverDistance)
        {
            hoverBox.transform.rotation = Quaternion.Euler(0f, 0f, 0f); 
            hoverBox.SetActive(true);
            
        }
        else
        {
            hoverBox.SetActive(false);
        }
        if (CostumerPathfinding.errorCode1ActiveInThisAttempt)
        {
            errorCode1Symbol.SetActive(true);
        }
        else
        {
            errorCode1Symbol.SetActive(false);
        }
    }

    public void findShelfToMoveTo()
    {
        Debug.Log(ShelfInventoryManager.Instance.findShelfToMoveTo("Apple"));
        for (int i = 0; i < StoppsToTake; i++)
        {
            if (i == 0)
            {
                if (ShelfInventoryManager.Instance.findShelfToMoveTo("Apple") != Vector3.zero)
                {
                    CostumerPathfinding.addStop(ShelfInventoryManager.Instance.findShelfToMoveTo("Apple"), false);
                }
            }
            if (i == 1)
            {
                if (ShelfInventoryManager.Instance.findShelfToMoveTo("Carrot") != Vector3.zero)
                {
                    CostumerPathfinding.addStop(ShelfInventoryManager.Instance.findShelfToMoveTo("Carrot"), false);
                }
            }
            if (i == 2)
            {
                if (ShelfInventoryManager.Instance.findShelfToMoveTo("Bread") != Vector3.zero)
                {
                    CostumerPathfinding.addStop(ShelfInventoryManager.Instance.findShelfToMoveTo("Bread"),false);
                }
            }
        }
        //Add the exit as the final stop
        CostumerPathfinding.addStop(Vector3.zero, true);
        CostumerPathfinding.incitateMovement();
        //CostumerPathfinding.addStop(ShelfInventoryManager.Instance.findShelfToMoveTo("Apple"));

    }
}
