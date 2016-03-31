using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public int Left = 1;
    public int Up = 2;
    public int Right = 3;
    public int Down = 4;
    public int totalMoves;
    public int currentMoves;
    public int totalTiles;
    public int paintedTiles;

	// Use this for initialization
	void Start () {
        currentMoves = totalMoves;
        paintedTiles = 0;
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
