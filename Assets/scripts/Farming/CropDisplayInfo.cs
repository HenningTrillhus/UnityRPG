using UnityEngine;
using TMPro;

public class CropDisplayInfo : MonoBehaviour
{
    public GameObject InfoOBJ;
    public GameObject Crop;

    private MousePosition mouseValues;
    private TickLogic tickLogic;


    private  TextMeshPro CropSeed;
    private  TextMeshPro CropLevel;
    private  TextMeshPro CropProsent;
    private  TextMeshPro CropState;
    //public MousePosition mouseValues;
    

    private string cropState;
    private int cropLevel = 0;

    private int tickCount = 0;

    private int amountOfTickToNextLevel = 1000;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mouseValues = FindObjectOfType<MousePosition>();
        tickLogic = FindObjectOfType<TickLogic>();
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
        InfoOBJ.SetActive(false);
    }

    // Update is called once per frame
    
    public float hoverDistance = 0.5f; // how close the mouse needs to be

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        float distance = Vector2.Distance(mousePos, Crop.transform.position);

        if (distance < hoverDistance)
        {
            InfoOBJ.SetActive(true);

            
            if (Input.GetMouseButtonDown(0) && mouseValues.building)
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
        CropSeed.text = seedType;
        CropLevel.text = "Level 1";
        CropProsent.text = "0%";
        CropState.text = "needs water";
        tickCount = 0;
        cropLevel = 1;
    }

    void checkForTick()
    {
        tickCount ++;
    }

    float getPorsentOfTick()
    {
        if (tickCount >= 1000){
            return 100f;
        }
        else 
        {
            float prosent = tickCount/ (float)amountOfTickToNextLevel  * 100;
            Debug.Log(tickCount + "   " + prosent);
            return prosent;
        }
    }
}
