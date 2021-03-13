using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class Grid : MonoBehaviour
{
    public int Rows;
    public int Columns;


    public int LR;

    public int LC;

    public GameObject MinimapCam;

    public List<Tile> GridList = new List<Tile>();

    TileComp tileComparer = new TileComp();

    public GameObject BaseTile;

    // Start is called before the first frame update
    void Start()
    {
        //CreateGrid();
        FindAllNeighbors();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string SaveString()
    {
        string Result = "";

        Result += Rows.ToString() + ";";
        Result += Columns.ToString() + ";";

        //Debug.Log(Result);

        foreach (Tile tileSpace in GridList)
        {

            Result += tileSpace.SaveString();

        }

        Result += ";";

        return Result;
    }

    public void LoadString(string map)
    {
        string[] splitString = map.Split(';');

        Debug.Log(Convert.ToInt32(splitString[0]));
        Debug.Log(splitString[1]);
        Debug.Log(splitString[2]);

        Rows = Convert.ToInt32(splitString[0]);
        Columns = Convert.ToInt32(splitString[1]);

        CreateGrid();

        string tileData = splitString[2];

        int charAt = 0;
        foreach (Tile tileSpace in GridList)
        {

            
            if (charAt >= GridList.Count || charAt >= tileData.Length)
            {
                Debug.Log("loading error, more tiles than map size");
            }
            tileSpace.LoadTile(tileData[charAt]);
            charAt++;

        }

    }


    public void CreateGrid()
    {
        if (Columns < LC)
        {            
            foreach(Tile tile in GridList.ToArray())
            {
                if(tile.Column > Columns)
                {
                    
                    DestroyImmediate(tile.gameObject);
                    GridList.Remove(tile);
                }
            }
            LC = Columns;
        }
        if(Rows < LR)
        {
            foreach (Tile tile in GridList.ToArray())
            {
                if (tile.Row > Rows)
                {
                    
                    DestroyImmediate(tile.gameObject);
                    GridList.Remove(tile);

                }
            }
            LR = Rows;
        }

        for (int i = 1; i <= Columns; i++)
        {
            if (i <= LC)
            {
                if (i % 2 == 0)
                {
                    for (int j = LR+1; j <= Rows; j++)
                    {
                        //GameObject tl = Instantiate(BaseTile, new Vector3((Mathf.Sqrt(3) * (i - 1)) / 2, 0, j), Quaternion.identity, gameObject.transform);

                        GameObject tl = (GameObject)PrefabUtility.InstantiatePrefab(BaseTile, gameObject.transform);

                        tl.transform.position = new Vector3((Mathf.Sqrt(3) * (i - 1)) / 2, 0, j);

                        tl.transform.rotation = Quaternion.identity;

                        tl.name = "Hex_" + i + "_" + j;

                        tl.GetComponent<Tile>().Row = j;
                        tl.GetComponent<Tile>().Column = i;


                        GridList.Add(tl.GetComponent<Tile>());
                    }
                }

                else
                {

                    for (int j = LR+1; j <= Rows; j++)
                    {
                        //GameObject tl = Instantiate(BaseTile, new Vector3((Mathf.Sqrt(3) * (i - 1)) / 2, 0, j - .5f), Quaternion.identity, gameObject.transform);

                        GameObject tl = (GameObject)PrefabUtility.InstantiatePrefab(BaseTile, gameObject.transform);

                        tl.transform.position = new Vector3((Mathf.Sqrt(3) * (i - 1)) / 2, 0, j - .5f);

                        tl.transform.rotation = Quaternion.identity;

                        tl.name = "Hex_" + i + "_" + j;

                        tl.GetComponent<Tile>().Row = j;
                        tl.GetComponent<Tile>().Column = i;

                        GridList.Add(tl.GetComponent<Tile>());
                    }
                }
            }
            else
            {

                if (i % 2 == 0)
                {
                    for (int j = 1; j <= Rows; j++)
                    {
                        //GameObject tl = Instantiate(BaseTile, new Vector3((Mathf.Sqrt(3) * (i - 1)) / 2, 0, j), Quaternion.identity, gameObject.transform);

                        GameObject tl = (GameObject)PrefabUtility.InstantiatePrefab(BaseTile, gameObject.transform);

                        tl.transform.position = new Vector3((Mathf.Sqrt(3) * (i - 1)) / 2, 0, j);

                        tl.transform.rotation = Quaternion.identity;

                        tl.name = "Hex_" + i + "_" + j;

                        tl.GetComponent<Tile>().Row = j;
                        tl.GetComponent<Tile>().Column = i;

                        GridList.Add(tl.GetComponent<Tile>());
                    }
                }

                else
                {
                    for (int j = 1; j <= Rows; j++)
                    {
                        //GameObject tl = Instantiate(BaseTile, new Vector3((Mathf.Sqrt(3) * (i - 1)) / 2, 0, j - .5f), Quaternion.identity, gameObject.transform);

                        GameObject tl = (GameObject)PrefabUtility.InstantiatePrefab(BaseTile, gameObject.transform);

                        tl.transform.position = new Vector3((Mathf.Sqrt(3) * (i - 1)) / 2, 0, j - .5f);

                        tl.transform.rotation = Quaternion.identity;

                        tl.name = "Hex_" + i + "_" + j;

                        tl.GetComponent<Tile>().Row = j;
                        tl.GetComponent<Tile>().Column = i;

                        GridList.Add(tl.GetComponent<Tile>());
                    }
                }
            }
        }



        //GridList.Sort();        
        GridList.Sort(tileComparer);

        MinimapCam.transform.position = new Vector3(((Mathf.Sqrt(3) * ((Columns-1))) / 2)/2, 110, Rows/2);

        if (MinimapCam.transform.position.x > MinimapCam.transform.position.z)
        {
            MinimapCam.GetComponent<Camera>().orthographicSize = MinimapCam.transform.position.x + .6f;
        }
        else
        {
            MinimapCam.GetComponent<Camera>().orthographicSize = MinimapCam.transform.position.z;
        }

        FindAllNeighbors();
    }

    public void FindAllNeighbors()
    {
        foreach (Tile T in GridList)
        {
            T.TileUpdate();
            T.FindNeighbors();

        }

        LR = Rows;
        LC = Columns;

    }

    public void SortTiles()
    {
        


    }

    public void ResetAllPathing()
    {
        foreach (Tile T in GridList)
        {

            T.GetComponent<Pathnode>().MPRemain = -1;

        }

    }

    public void ResetGrid()
    {
        foreach(Tile tile in GridList)
        {
            DestroyImmediate(tile.gameObject, true);
                                 
        }

        GridList.Clear();
        Rows = 0;
        Columns = 0;
        LR = 0;
        LC = 0;
    }

    [System.Serializable]
    public struct TileSpace
    {
        public int R;
        public int C;
        public GameObject T;

    }

    public void UpdateRows(int R)
    {

        Rows = R;
        CreateGrid();

    }

    public void UpdateColumns(int C)
    {

        Columns = C;
        CreateGrid();
    }

}
