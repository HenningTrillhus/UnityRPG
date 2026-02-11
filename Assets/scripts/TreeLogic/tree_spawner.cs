using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public GameObject treePrefab;
    public int numberOfTrees = 1;
    public Vector2 areaMin = new Vector2(-5, -5);
    public Vector2 areaMax = new Vector2(5, 5);
    public Transform player;  // assign your player in Inspector

    void Start()
    {
        for (int i = 0; i < numberOfTrees; i++)
        {
            SpawnTree();
        }
    }

    void SpawnTree()
    {
        float x = Random.Range(areaMin.x, areaMax.x);
        float y = Random.Range(areaMin.y, areaMax.y);
        Vector3 spawnPosition = new Vector3(x, y, 0);

        GameObject tree = Instantiate(treePrefab, spawnPosition, Quaternion.identity);

        // Assign the player to the TreeSorting script
        TreeSorting treeScript = tree.GetComponent<TreeSorting>();
        if (treeScript != null)
        {
            treeScript.player = player;
        }
    }
}
