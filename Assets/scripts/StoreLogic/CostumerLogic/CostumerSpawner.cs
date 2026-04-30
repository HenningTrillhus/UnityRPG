using UnityEngine;

public class CostumerSpawner : MonoBehaviour
{
    [Header("Sprite Refrence")]
    [SerializeField]public Sprite CarrotSprite;
    [SerializeField]public Sprite AppleSprite;
    [SerializeField]public Sprite PotatoSprite;
    [SerializeField]public Sprite BreadSprite;
    [SerializeField]public Sprite RedMushroomSprite;
    [SerializeField]public Sprite BrownMushroomSprite;
    [SerializeField]public Sprite DiamondSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if press button b on keybord start sequennce
        
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
