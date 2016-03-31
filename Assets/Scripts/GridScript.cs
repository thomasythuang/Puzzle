using UnityEngine;
using System.Collections;

public class GridScript : MonoBehaviour {

    public float xMax; // This is a ratio in relation to the total screen size: (0,1)
    public float yMax;
    public int xlen;
    public int ylen;

    // Use this for initialization
    void Start()
    {
        AdjustScreen();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void AdjustScreen()
    {
        float width = Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height;
        float height = Camera.main.orthographicSize * 2.0f;

        float xRatio = xlen / width;
        float yRatio = ylen / height;

        float scale = Mathf.Min(xMax / xRatio, yMax / yRatio);

        Debug.Log("xRatio: " + xRatio.ToString());
        Debug.Log("yRatio: " + yRatio.ToString());
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
