using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gridmap : MonoBehaviour
{
    private int width;
    private int height;
    private float cellSize;
    private int[,] gridArray;
    private GameObject[,] debugTextArray;

    GameObject text;

    public Gridmap(int width, int height, float cellSize)
    {
        text = new GameObject();
        text.AddComponent<TextMesh>();
        text.GetComponent<TextMesh>().fontSize = 20;
        text.GetComponent<TextMesh>().characterSize = .2f;
        text.GetComponent<TextMesh>().alignment = TextAlignment.Center;
        text.GetComponent<TextMesh>().anchor = TextAnchor.MiddleCenter;
        text.GetComponent<TextMesh>().text = 0.ToString();

        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridArray = new int[width, height];
        debugTextArray = new GameObject[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {

            for (int y = 0; y <gridArray.GetLength(1); y++)
            {
                Debug.Log(x + ", " + y);
                debugTextArray[x,y] = Instantiate(text, GetWorldPos(x, y) + new Vector3(cellSize / 2, cellSize/2), Quaternion.identity);

                Debug.DrawLine(GetWorldPos(x, y), GetWorldPos(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPos(x, y), GetWorldPos(x+1, y), Color.white, 100f);

            }

        }
        Debug.DrawLine(GetWorldPos(0, height), GetWorldPos(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPos(width, 0), GetWorldPos(width, height), Color.white, 100f);

        Destroy(text);

        SetValue(2, 1, 56);
    }

    private Vector3 GetWorldPos(float x, float y)
    {

        return new Vector3(x, y) * cellSize;

        /*
        if(x % 2 == 0)
        {
            y += .5f * cellSize;


        }
        return new Vector3(x * Mathf.Sqrt(3), y);
        */
    }

    private void GetXY(Vector3 worldPos, out int x, out int y)
    {

        x = Mathf.FloorToInt(worldPos.x / cellSize);
        y = Mathf.FloorToInt(worldPos.y / cellSize);

    }

    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            debugTextArray[x, y].GetComponent<TextMesh>().text = gridArray[x, y].ToString();
        }
    }

    public void SetValue (Vector3 worldpos, int value)
    {
        int x, y;
        GetXY(worldpos, out x, out y);
        SetValue(x, y, value);


    }



}
