using UnityEngine;
using System;
using TMPro;


public class CostumerStates : MonoBehaviour
{
    public GameObject hoverBox;
    public GameObject errorCode1Symbol;
    public CostumerSpawner CostumerSpawner;
    public CostumerPathFinder CostumerPathfinding;

    
    
    public string costumerName;
    public string costumerClass;

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
        errorCode1Symbol.SetActive(false);
        hoverBox.SetActive(false);
        costumerNameBox.text = costumerName;
        costumerClassBox.text = costumerClass;
        lookingForItemSprite1.sprite = CostumerSpawner.getSpriteByID(12);
        lookingForItemSprite2.sprite = CostumerSpawner.getSpriteByID(13);
        lookingForItemSprite3.sprite = CostumerSpawner.getSpriteByID(14);
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
}
