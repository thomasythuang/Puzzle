using UnityEngine;
using UnityEditor;
using System.Collections;

public class GridGenerator : MonoBehaviour {

    public GameObject tilePrefab;
    public int xBound;
    public int yBound;
    public bool run;
    public bool savePrefab;
    public string fileName;
    private string filePath;
    private GameObject grid;

    // Use this for initialization
    void Start () {
        filePath = "Assets/Resources/Prefabs/" + fileName + ".prefab";
        if (run)
        {
            grid = new GameObject();
            CreateTiles();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void CreateTiles()
    {
        int xlen = 2 * xBound + 1;
        int ylen = 2 * yBound + 1;
        GameObject[][] tileArray = new GameObject[xlen][];
        for (int i = 0; i < xlen; i++)
        {
            tileArray[i] = new GameObject[ylen];
        }

        for (int y = yBound * -1; y <= yBound; y++)
        {
            for (int x = xBound * -1; x <= xBound; x++)
            {
                GameObject tile = Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity) as GameObject;
                tile.transform.parent = grid.transform;
                tileArray[x + xBound][y + yBound] = tile;
            }
        }

        for (int y = 0; y < ylen; y++)
        {
            for (int x = 0; x < xlen; x++)
            {
                //Debug.Log(tileArray[x][y].transform.position.x.ToString() + ", " + tileArray[x][y].transform.position.y.ToString());
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

        if (savePrefab)
        {
            PrefabUtility.CreatePrefab(filePath, grid);
        }
    }
}
