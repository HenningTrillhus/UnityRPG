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
    public List<string> listOfItemsLookingfor = new List<string>();
    public List<int> listOfItemsLookingForShelfId = new List <int>();
    private int randomNumberForItem1;
    private int randomNumberForItem2;
    private int randomNumberForItem3;

    private int StoppsToTake = 3;

    public float hoverDistance = 0.5f;

    //sat up 1y becuse the pathfinding moves it down one
    private Vector3 LastPostion = new Vector3(-9.5f, 18.5f, 0f);

    [SerializeField] private TextMeshPro costumerNameBox;
    [SerializeField] private TextMeshPro costumerClassBox;

    [SerializeField] private SpriteRenderer lookingForItemSprite1;
    [SerializeField] private SpriteRenderer lookingForItemSprite2;
    [SerializeField] private SpriteRenderer lookingForItemSprite3;

    [Header("Sprite Refrence")]
    [SerializeField]public Sprite CarrotSprite;
    [SerializeField]public Sprite AppleSprite;
    [SerializeField]public Sprite PotatoSprite;
    [SerializeField]public Sprite BreadSprite;
    [SerializeField]public Sprite RedMushroomSprite;
    [SerializeField]public Sprite BrownMushroomSprite;
    [SerializeField]public Sprite DiamondSprite;

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
        listOfItemsLookingfor.Add(getNameByID(listOfPosibleItemsID[randomNumberForItem1]));
        listOfItemsLookingfor.Add(getNameByID(listOfPosibleItemsID[randomNumberForItem2]));
        listOfItemsLookingfor.Add(getNameByID(listOfPosibleItemsID[randomNumberForItem3]));


        lookingForItemSprite1.sprite = getSpriteByID(listOfPosibleItemsID[randomNumberForItem1]);
        lookingForItemSprite2.sprite = getSpriteByID(listOfPosibleItemsID[randomNumberForItem2]);
        lookingForItemSprite3.sprite = getSpriteByID(listOfPosibleItemsID[randomNumberForItem3]);

        findShelfToMoveTo();        
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
        //Debug.Log(ShelfInventoryManager.Instance.findShelfToMoveTo("Apple"));
        for (int i = 0; i < StoppsToTake; i++)
        {
            //finds the shelf for each item, if not exists in any shelfs then add next stop to be at the same as last.
            if (i == 0)
            {
                (Vector3 position, int index) = ShelfInventoryManager.Instance.findShelfToMoveTo(listOfItemsLookingfor[0]);
                if (position != Vector3.zero)
                {
                    listOfItemsLookingForShelfId.Add(index);
                    LastPostion = position;
                    CostumerPathfinding.addStop(position,false,listOfItemsLookingfor[0]);
                }
                else
                {
                    listOfItemsLookingfor[0] = "";
                    listOfItemsLookingForShelfId.Add(-2);
                    CostumerPathfinding.addStop(LastPostion,false,"");
                }
            }
            if (i == 1)
            {
                (Vector3 position, int index) = ShelfInventoryManager.Instance.findShelfToMoveTo(listOfItemsLookingfor[1]);
                if (position != Vector3.zero)
                {
                    listOfItemsLookingForShelfId.Add(index);
                    LastPostion = position;
                    CostumerPathfinding.addStop(position,false,listOfItemsLookingfor[1]);
                }
                else
                {
                    listOfItemsLookingfor[1] = "";
                    listOfItemsLookingForShelfId.Add(-2);
                    CostumerPathfinding.addStop(LastPostion,false,"");
                }
            }
            if (i == 2)
            {
                (Vector3 position, int index) = ShelfInventoryManager.Instance.findShelfToMoveTo(listOfItemsLookingfor[2]);
                if (position != Vector3.zero)
                {
                    listOfItemsLookingForShelfId.Add(index);
                    LastPostion = position;
                    CostumerPathfinding.addStop(position,false,listOfItemsLookingfor[2]);
                }
                else
                {
                    listOfItemsLookingfor[2] = "";
                    listOfItemsLookingForShelfId.Add(-2);
                    CostumerPathfinding.addStop(LastPostion,false,"");
                }
            }
        }
        listOfItemsLookingfor.Add("");
        //Add the exit as the final stop
        CostumerPathfinding.addStop(Vector3.zero, true, "");
        CostumerPathfinding.incitateMovement();
        //CostumerPathfinding.addStop(ShelfInventoryManager.Instance.findShelfToMoveTo("Apple"));

    }

    public Sprite getSpriteByID(int id)
    {
        if (id == 7)
        {
            return BreadSprite;
        }
        if (id == 12)
        {
            return CarrotSprite;
        }
        if (id == 13)
        {
            return AppleSprite;
        }
        if (id == 14)
        {
            return PotatoSprite;
        }
        if (id == 15)
        {
            return RedMushroomSprite;
        }
        if (id == 16)
        {
            return BrownMushroomSprite;
        }
        if (id == 17)
        {
            return DiamondSprite;
        }
        else
        {
            Debug.Log("fuck balls, dont have the spirt for this id. id sendt in: " + id);
            return null;
        }
    }

    public string getNameByID(int id)
    {
        if (id == 7)
        {
            return "Bread";
        }
        if (id == 12)
        {
            return "Carrot";
        }
        if (id == 13)
        {
            return "Apple";
        }
        if (id == 14)
        {
            return "Potato";
        }
        if (id == 15)
        {
            return "Red Mushroom";
        }
        if (id == 16)
        {
            return "Brown Mushroom";
        }
        if (id == 17)
        {
            return "Diamond";
        }
        else
        {
            Debug.Log("fuck balls, dont have the name for this id. id sendt in: " + id);
            return null;
        }
    }
}
