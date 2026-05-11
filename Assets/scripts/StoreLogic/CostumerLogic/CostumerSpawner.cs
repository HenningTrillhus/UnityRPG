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

    [Header("Item Refrence")]
    [SerializeField]public ItemData Carrot;
    [SerializeField]public ItemData Apple;
    [SerializeField]public ItemData Potato;
    [SerializeField]public ItemData Bread;
    [SerializeField]public ItemData RedMushroom;
    [SerializeField]public ItemData BrownMushroom;
    [SerializeField]public ItemData Diamond;

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
            int randomSpawnTick = UnityEngine.Random.Range(0, 6);
            if (randomSpawnTick == 5)
            {
                
                NumberOfCosstumersInStore++;
                Instantiate(Costumer, new Vector3 (-9.5f, 17.5f, 0f), Quaternion.identity);
            }
        }
        
        
    }

    public (int, Sprite) getItemValueAndSpriteByID(string itemName)
    {
        if (itemName == "Bread")
        {
            return (Bread.valueInGameCurrency,Bread.icon);
        }
        if (itemName == "Carrot")
        {
            return (Carrot.valueInGameCurrency, Carrot.icon);
        }
        if (itemName == "Apple")
        {
            return (Apple.valueInGameCurrency, Apple.icon);
        }
        if (itemName == "Potato")
        {
            return (Potato.valueInGameCurrency, Potato.icon);
        }
        if (itemName == "RedMushroom")
        {
            return (RedMushroom.valueInGameCurrency, RedMushroom.icon);
        }
        if (itemName == "BrownMushroom")
        {
            return (BrownMushroom.valueInGameCurrency, BrownMushroom.icon);
        }
        if (itemName == "Diamond")
        {
            return (Diamond.valueInGameCurrency, Diamond.icon);
        }
        else
        {
            return (0, null);
        }
    }

    public Sprite getSpriteByID(int id)
    {
        if (id == 7)
        {
            return Bread.icon;
        }
        if (id == 12)
        {
            return Carrot.icon;
        }
        if (id == 13)
        {
            return Apple.icon;
        }
        if (id == 14)
        {
            return Potato.icon;
        }
        if (id == 15)
        {
            return RedMushroom.icon;
        }
        if (id == 16)
        {
            return BrownMushroom.icon;
        }
        if (id == 17)
        {
            return Diamond.icon;
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

    public (string, Sprite, int) getItemInfoByID(int id, string itemName)
    {
        if (id != 0)
        {
            if (id == 7)
            {
                return (Bread.itemName, Bread.icon, Bread.valueInGameCurrency);
            }
            if (id == 12)
            {
                return (Carrot.itemName, Carrot.icon, Carrot.valueInGameCurrency);
            }
            if (id == 13)
            {
                return (Apple.itemName, Apple.icon, Apple.valueInGameCurrency);
            }
            if (id == 14)
            {
                return (Potato.itemName, Potato.icon, Potato.valueInGameCurrency);
            }
            if (id == 15)
            {
                return (RedMushroom.itemName, RedMushroom.icon, RedMushroom.valueInGameCurrency);
            }
            if (id == 16)
            {
                return (BrownMushroom.itemName, BrownMushroom.icon, BrownMushroom.valueInGameCurrency);
            }
            if (id == 17)
            {
                return (Diamond.itemName, Diamond.icon, Diamond.valueInGameCurrency);
            }
            else
            {
                Debug.Log("fuck balls, dont have the info for this id. id sendt in: " + id);
                return (null, null, 0);
            }
        }
        if (itemName != null)
        {
            if (itemName == "Bread")
            {
                return (Bread.itemName, Bread.icon, Bread.valueInGameCurrency);
            }
            if (itemName == "Carrot")
            {
                return (Carrot.itemName, Carrot.icon, Carrot.valueInGameCurrency);
            }
            if (itemName == "Apple")
            {
                return (Apple.itemName, Apple.icon, Apple.valueInGameCurrency);
            }
            if (itemName == "Potato")
            {
                return (Potato.itemName, Potato.icon, Potato.valueInGameCurrency);
            }
            if (itemName == "Red Mushroom")
            {
                return (RedMushroom.itemName, RedMushroom.icon, RedMushroom.valueInGameCurrency);
            }
            if (itemName == "Brown Mushroom")
            {
                return (BrownMushroom.itemName, BrownMushroom.icon, BrownMushroom.valueInGameCurrency);
            }
            if (itemName == "Diamond")
            {
                return (Diamond.itemName, Diamond.icon, Diamond.valueInGameCurrency);
            }
            else
            {
                Debug.Log("fuck balls, dont have the info for this id. id sendt in: " + id);
                return (null, null, 0);
            }
        }
        else
        {
            Debug.Log("fuck balls, dont have the info for this id. id sendt in: " + id);
            return (null, null, 0);
        }
    }
}
