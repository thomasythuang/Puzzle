using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour {

    public GameObject leftTile;
    public GameObject upTile;
    public GameObject rightTile;
    public GameObject downTile;
    public bool painted;
    private LevelManager levelManager;

	// Use this for initialization
	void Start () {
        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
	    
        if (Input.touchCount > 0 && levelManager.currentMoves > 0)
        {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            if (GetComponent<Collider2D>().OverlapPoint(wp))
            {
                //Debug.Log(transform.position.x.ToString() + ", " + transform.position.y.ToString());
                if (!painted)
                {
                    StartPainting();
                    levelManager.currentMoves--;
                }
            }
        }
	}

    private void StartPainting()
    {
        PaintThisTile(Color.green);
        if (leftTile)
        {
            leftTile.GetComponent<TileScript>().PaintInDirection(levelManager.Left);
        }
        if (upTile)
        {
            upTile.GetComponent<TileScript>().PaintInDirection(levelManager.Up);
        }
        if (rightTile)
        {
            rightTile.GetComponent<TileScript>().PaintInDirection(levelManager.Right);
        }
        if (downTile)
        {
            downTile.GetComponent<TileScript>().PaintInDirection(levelManager.Down);
        }

    }

    public void PaintInDirection(int direction)
    {
        if (!painted)
        {
            PaintThisTile(Color.green);
            painted = true;
            if (direction == levelManager.Left && leftTile)
            {
                leftTile.GetComponent<TileScript>().PaintInDirection(levelManager.Left);
            }
            else if(direction == levelManager.Up && upTile)
            {
                upTile.GetComponent<TileScript>().PaintInDirection(levelManager.Up);
            }
            else if(direction == levelManager.Right && rightTile)
            {
                rightTile.GetComponent<TileScript>().PaintInDirection(levelManager.Right);
            }
            else if(direction == levelManager.Down && downTile)
            {
                downTile.GetComponent<TileScript>().PaintInDirection(levelManager.Down);
            }
        }
    }

    public void PaintThisTile(Color color)
    {
        gameObject.GetComponent<Renderer>().material.color = color;
        painted = true;
        levelManager.paintedTiles++;
        if (levelManager.paintedTiles == levelManager.totalTiles)
        {
            Debug.Log("Completed Level!");
        }
    }
}
