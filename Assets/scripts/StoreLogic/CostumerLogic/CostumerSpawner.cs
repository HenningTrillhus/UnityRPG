using UnityEngine;

public class CostumerSpawner : MonoBehaviour
{
    public static CostumerSpawner Instance { get; private set; }

    [Header("Sprite Refrence")]
    [SerializeField]public Sprite CarrotSprite;
    [SerializeField]public Sprite AppleSprite;
    [SerializeField]public Sprite PotatoSprite;
    [SerializeField]public Sprite BreadSprite;
    [SerializeField]public Sprite RedMushroomSprite;
    [SerializeField]public Sprite BrownMushroomSprite;
    [SerializeField]public Sprite DiamondSprite;

    [Header("Tick Logic")]
    [SerializeField]public TickLogic tickLogic;

    [Header("Store Logic")]
    [SerializeField]public StoreAreaLogic StoreLogic;

    [Header("Costumer Prefab")]
    public GameObject Costumer;

    private int tickCount = 0;
    private int LastTick = 0;

    public int maxNumberOfCostumers = 1;
    public int NumberOfCosstumersInStore = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake() {
        Instance = this;
    }

    void Start()
    {
        TickLogic.OnTick += CheckForTicks;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    void CheckForTicks()
    {
        if (StoreLogic.StoreIsOpen && NumberOfCosstumersInStore < maxNumberOfCostumers)
        {
            tickCount ++;
            int randomSpawnTick = UnityEngine.Random.Range(0, 10);
            if (randomSpawnTick == 5)
            {
                Debug.Log("Spawn Costumer");
                NumberOfCosstumersInStore++;
                Instantiate(Costumer, new Vector3 (-9.5f, 17.5f, 0f), Quaternion.identity);
            }
        }
        
        
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
