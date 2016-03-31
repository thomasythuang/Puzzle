using UnityEngine;
using UnityEditor;
using System.Collections;

public class GridGenerator : MonoBehaviour {

    public GameObject tilePrefab;
    public int xlen;
    public int ylen;
    private float xmin;
    private float ymin;
    public bool run;
    public bool savePrefab;
    public string fileName;
    private string filePath;
    private GameObject grid;

    // Initialization
    void Start () {
        if (run)
        {
            xmin = (float)(xlen - 1) / -2;
            ymin = (float)(ylen - 1) / -2;
            grid = new GameObject();
            CreateTiles();
        }
	}
	
	void Update () {
	
	}

    private void CreateTiles()
    {
        // Init temp array to hold our tile objects
        GameObject[][] tileArray = new GameObject[xlen][];
        for (int i = 0; i < xlen; i++)
        {
            tileArray[i] = new GameObject[ylen];
        }

        // Instantiate the grid of tiles
        for (int y = 0; y < ylen; y++)
        {
            for (int x = 0; x < xlen; x++)
            {
                GameObject tile = Instantiate(tilePrefab, new Vector3(x+xmin, y+ymin), Quaternion.identity) as GameObject;
                tile.transform.parent = grid.transform;
                tileArray[x][y] = tile;
            }
        }

        // For each tile, set references to its neighboring tiles
        for (int y = 0; y < ylen; y++)
        {
            for (int x = 0; x < xlen; x++)
            {
                TileScript tileScript = tileArray[x][y].GetComponent<TileScript>();
                if (x > 0)
                {
                    tileScript.leftTile = tileArray[x - 1][y];
                }    
                if (x < xlen - 1)
                {
                    tileScript.rightTile = tileArray[x + 1][y];
                }
                if (y > 0)
                {
                    tileScript.downTile = tileArray[x][y - 1];
                }
                if (y < ylen - 1)
                {
                    tileScript.upTile = tileArray[x][y + 1];
                }
            }
        }

        // Attach GridScript to the instantiated grid
        grid.AddComponent<GridScript>();
        GridScript gridScript = grid.GetComponent<GridScript>();
        gridScript.xlen = xlen;
        gridScript.ylen = ylen;

        // Save this as a prefab if specified
        if (savePrefab)
        {
            filePath = "Assets/Resources/Prefabs/" + fileName + ".prefab";
            PrefabUtility.CreatePrefab(filePath, grid);
        }
    }
}
