using UnityEngine;
using TMPro;
using System;

public class CropDisplayInfo : MonoBehaviour
{
    public static event Action OnNewCropPlanted;

    public GameObject InfoOBJ;
    public GameObject Crop;

    private MousePosition mouseValues;
    private TickLogic tickLogic;
    private CropPlacementLogic cropPlacementLogic;


    private TextMeshPro CropSeed;
    private TextMeshPro CropLevel;
    private TextMeshPro CropProsent;
    private TextMeshPro CropState;
    private RectTransform WaterBarBG;
    [SerializeField] private GameObject WaterLevelBar;
    
    public float hoverDistance = 0.5f; // how close the mouse needs to be

    private string cropState;
    private int cropLevel = 0;

    private int tickCount = 0;

    public int amountOfTickToNextLevel = 200;

    private float waterLevel = 100;
    private float fullHeightWaterBar;
    float barBottomY;

    private SpriteRenderer sr;
    [Header("Plant Sprite")]
    [SerializeField] private SpriteRenderer childSprite;

    [Header("Empty Crop Sprits")]
    public Sprite NoNeighbour;

    // Single sides
    public Sprite NeighbourTop;
    public Sprite NeighbourBottom;
    public Sprite NeighbourLeft;
    public Sprite NeighbourRight;

    // Two sides
    public Sprite NeighbourTopLeft;
    public Sprite NeighbourTopRight;
    public Sprite NeighbourTopBottom;
    public Sprite NeighbourLeftRight;
    public Sprite NeighbourLeftBottom;
    public Sprite NeighbourRightBottom;

    // Three sides
    public Sprite NeighbourTopLeftRight;
    public Sprite NeighbourTopLeftBottom;
    public Sprite NeighbourTopRightBottom;
    public Sprite NeighbourLeftRightBottom;

    // All around
    public Sprite NeighbourAllAround;

    [Header("Carrot Sprits")]
    public Sprite CarrotStage1;
    public Sprite CarrotStage2;
    public Sprite CarrotStage3;
    public Sprite CarrotStage4;
    public Sprite CarrotStage5;
    public Sprite CarrotStage6;
    public Sprite CarrotStage7;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        childSprite.enabled  = false;
        sr = GetComponent<SpriteRenderer>();
        OnNewCropPlanted += ReactToNewCrop;
        //ReactToNewCrop();

        mouseValues = FindObjectOfType<MousePosition>();
        tickLogic = FindObjectOfType<TickLogic>();
        cropPlacementLogic = FindObjectOfType<CropPlacementLogic>();
        TickLogic.OnTick += checkForTick;

        

        TextMeshPro[] allTexts = GetComponentsInChildren<TextMeshPro>(true);

        foreach (TextMeshPro tmp in allTexts)
        {
            switch (tmp.gameObject.name)
            {
                case "CropSeed":    CropSeed    = tmp; break;
                case "CropLevel":   CropLevel   = tmp; break;
                case "CropProsent": CropProsent = tmp; break;
                case "CropState":   CropState   = tmp; break;
            }
        }

        RectTransform[] allRects = GetComponentsInChildren<RectTransform>(true);
        
        foreach (RectTransform rect in allRects)
        {
            
            //Debug.Log("Found rect: " + rect.gameObject.name); // print all names
            //if (rect.gameObject.name == "WaterLevelBar")waterLevelBar = rect;
            switch (rect.gameObject.name)
            {
                case "WaterBarBG":     WaterBarBG     = rect; break;
                //case "WaterLevelBar":  WaterLevelBar  = rect; break;
            }
        }

        /*SpriteRenderer[] allSprites = GetComponentsInChildren<SpriteRenderer>(true);

        foreach (SpriteRenderer sprite in allSprites)
        {
            if (sprite.gameObject.name == "CropPlantSprite")
                childSr = sprite;
        }*/
        fullHeightWaterBar = WaterLevelBar.transform.localScale.y;
        barBottomY = WaterLevelBar.transform.position.y - (WaterLevelBar.transform.localScale.y / 2f);

        //SetBarHeight(50f);

        InfoOBJ.SetActive(false);
        cropState = "Empty";
        ReactToNewCrop();
    }

    // Update is called once per frame
    
    

    void Update()
    {
        //childSr.sprite = CarrotStage7;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        float distance = Vector2.Distance(mousePos, Crop.transform.position);

        if (distance < hoverDistance)
        {
            InfoOBJ.SetActive(true);

            
            if (Input.GetMouseButtonDown(0) && mouseValues.building && cropState == "Empty")
            {
                plantSeed(mouseValues.buildingBlock);
            }
        }
        else
        {
            InfoOBJ.SetActive(false);
        }
        if (cropLevel > 0)
        {
            CropProsent.text = Mathf.FloorToInt(getPorsentOfTick()).ToString() + "%";
        }

    }


    void plantSeed(string seedType)
    {
        childSprite.enabled = true;
        childSprite.sprite = CarrotStage1;
        CropSeed.text = seedType;
        CropLevel.text = "Level 1";
        CropProsent.text = "0%";
        CropState.text = "needs water";
        tickCount = 0;
        cropLevel = 1;
        cropState = "Growing";
    }

    void checkForTick()
    {
        if (cropLevel > 0 && waterLevel > 0)
        {
            tickCount ++;
            waterLevel -= waterLoos();
            SetBarHeight(waterLevel);
            if (waterLevel <= 20)
            {
                CropState.text = "needs water";
            }
            else
            {
                CropState.text = "Hydrated";  
            }
            //Debug.Log(waterLevel);
        }
        if (tickCount >= amountOfTickToNextLevel)
        {
            nextStage(cropLevel +1);
        }
        
    }

    void nextStage(int Stage)
    {
        tickCount = 0;
        cropLevel = Stage;
        CropLevel.text = "Level " + Stage;
        CropProsent.text = "0%";
        if (Stage==2){
            childSprite.sprite = CarrotStage2;
        }
        
    }

    float getPorsentOfTick()
    {
        if (tickCount >= amountOfTickToNextLevel){
            return 100f;
        }
        else 
        {
            float prosent = tickCount/ (float)amountOfTickToNextLevel  * 100;
            return prosent;
        }
    }

    float waterLoos()
    {
        int randomWaterLoos = UnityEngine.Random.Range(-7,2);
        if (randomWaterLoos < 1)
        {
            return 0f;
        }
        else
        {
            return randomWaterLoos;
        }
    }
    void SetBarHeight(float percent)
    {
        Vector3 scale = WaterLevelBar.transform.localScale;
        scale.y = fullHeightWaterBar * (percent / 100f);
        WaterLevelBar.transform.localScale = scale;

        WaterLevelBar.transform.position = new Vector3(
            WaterLevelBar.transform.position.x,
            barBottomY + (scale.y / 2f),
            WaterLevelBar.transform.position.z
        );
    }

    void ReactToNewCrop()
    {
        bool[] neighbours = cropPlacementLogic.checkNeighbourTiles(Crop.transform.position.x, Crop.transform.position.y);
        //Debug.Log(neighbours[2]);
        /*if (neighbours[1])
        {
            Debug.Log("Only Top");
        }
        if (neighbours[3])
        {
            Debug.Log("Only Left");
        }
        if (neighbours[4])
        {
            Debug.Log("Only Right");
        }
        if (neighbours[6])
        {
            Debug.Log("Only Bottom");
        }
        if (neighbours[1] && neighbours[3])
        {
            Debug.Log("Left And Top");
        }
        if (neighbours[1] && neighbours[4])
        {
            Debug.Log("Right And Top");
        }

        if (neighbours[0] && neighbours[1] &&neighbours[2] && neighbours[3] && neighbours[4] && neighbours[5] && neighbours[6] && neighbours[7])
        {
            Debug.Log("All Around");
        }*/

        // Single sides
        if (neighbours[1] && !neighbours[3] && !neighbours[4] && !neighbours[6]) sr.sprite = NeighbourTop;
        if (neighbours[3] && !neighbours[1] && !neighbours[4] && !neighbours[6]) sr.sprite = NeighbourLeft;
        if (neighbours[4] && !neighbours[1] && !neighbours[3] && !neighbours[6]) sr.sprite = NeighbourRight;
        if (neighbours[6] && !neighbours[1] && !neighbours[3] && !neighbours[4]) sr.sprite = NeighbourBottom;

        // Two sides
        if (neighbours[1] && neighbours[3] && !neighbours[4] && !neighbours[6]) sr.sprite = NeighbourTopLeft;
        if (neighbours[1] && neighbours[4] && !neighbours[3] && !neighbours[6]) sr.sprite = NeighbourTopRight;
        if (neighbours[1] && neighbours[6] && !neighbours[3] && !neighbours[4]) sr.sprite = NeighbourTopBottom;
        if (neighbours[3] && neighbours[4] && !neighbours[1] && !neighbours[6]) sr.sprite = NeighbourLeftRight;
        if (neighbours[3] && neighbours[6] && !neighbours[1] && !neighbours[4]) sr.sprite = NeighbourLeftBottom;
        if (neighbours[4] && neighbours[6] && !neighbours[1] && !neighbours[3]) sr.sprite = NeighbourRightBottom;

        // Three sides
        if (neighbours[1] && neighbours[3] && neighbours[4] && !neighbours[6]) sr.sprite = NeighbourTopLeftRight;
        if (neighbours[1] && neighbours[3] && neighbours[6] && !neighbours[4]) sr.sprite = NeighbourTopLeftBottom;
        if (neighbours[1] && neighbours[4] && neighbours[6] && !neighbours[3]) sr.sprite = NeighbourTopRightBottom;
        if (neighbours[3] && neighbours[4] && neighbours[6] && !neighbours[1]) sr.sprite = NeighbourLeftRightBottom;

        // Four cardinal sides
        if (neighbours[1] && neighbours[3] && neighbours[4] && neighbours[6]) sr.sprite = NeighbourAllAround;

        // All around
        if (neighbours[0] && neighbours[1] && neighbours[2] && neighbours[3] && neighbours[4] && neighbours[5] && neighbours[6] && neighbours[7]) sr.sprite = NeighbourAllAround;

        // None
        if (!neighbours[0] && !neighbours[1] && !neighbours[2] && !neighbours[3] && !neighbours[4] && !neighbours[5] && !neighbours[6] && !neighbours[7]) sr.sprite = NoNeighbour;

        // Corners only (no cardinal sides)
        if (neighbours[0] && !neighbours[1] && !neighbours[3]) Debug.Log("Top Left Corner");
        if (neighbours[2] && !neighbours[1] && !neighbours[4]) Debug.Log("Top Right Corner");
        if (neighbours[5] && !neighbours[3] && !neighbours[6]) Debug.Log("Bottom Left Corner");
        if (neighbours[7] && !neighbours[4] && !neighbours[6]) Debug.Log("Bottom Right Corner");

        // Cardinal + corners
        if (neighbours[1] && neighbours[3] && neighbours[0]) Debug.Log("Top Left And Top Left Corner");
        if (neighbours[1] && neighbours[4] && neighbours[2]) Debug.Log("Top Right And Top Right Corner");
        if (neighbours[3] && neighbours[6] && neighbours[5]) Debug.Log("Left Bottom And Bottom Left Corner");
        if (neighbours[4] && neighbours[6] && neighbours[7]) Debug.Log("Right Bottom And Bottom Right Corner");

        rendersprite();                
    }

    public static void TriggerOnNewCropPlanted()
    {
        OnNewCropPlanted?.Invoke();
    }

    void rendersprite()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    

    
}
