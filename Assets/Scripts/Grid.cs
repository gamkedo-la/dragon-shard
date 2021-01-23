using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int Rows;
    public int Columns;

    public int LR;
    public int LC;



    public List<TileSpace> GridList = new List<TileSpace>();



    public GameObject BaseTile;

    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CreateGrid()
    {

        if(Columns < LC)
        {
            
            foreach(TileSpace tile in GridList.ToArray())
            {
                if(tile.C > Columns)
                {
                    
                    DestroyImmediate(tile.T);
                    GridList.Remove(tile);

                }

            }
            LC = Columns;


        }
        if(Rows < LR)
        {

            foreach (TileSpace tile in GridList.ToArray())
            {
                if (tile.R > Rows)
                {
                    
                    DestroyImmediate(tile.T);
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


                        GameObject tl = Instantiate(BaseTile, new Vector3((Mathf.Sqrt(3) * (i - 1)) / 2, 0, j), Quaternion.identity, gameObject.transform);

                        tl.name = "Hex_" + i + "_" + j;

                        tl.GetComponent<Tile>().Row = j;
                        tl.GetComponent<Tile>().Column = i;

                        TileSpace temp;

                        temp.T = tl;

                        temp.C = i;

                        temp.R = j;

                        GridList.Add(temp);


                    }
                }

                else
                {

                    for (int j = LR+1; j <= Rows; j++)

                    {



                        GameObject tl = Instantiate(BaseTile, new Vector3((Mathf.Sqrt(3) * (i - 1)) / 2, 0, j - .5f), Quaternion.identity, gameObject.transform);

                        tl.name = "Hex_" + i + "_" + j;

                        tl.GetComponent<Tile>().Row = j;
                        tl.GetComponent<Tile>().Column = i;

                        TileSpace temp;

                        temp.T = tl;

                        temp.C = i;

                        temp.R = j;

                        GridList.Add(temp);

                    }
                }



            }
            else
            {

                if (i % 2 == 0)
                {
                    for (int j = 1; j <= Rows; j++)
                    {


                        GameObject tl = Instantiate(BaseTile, new Vector3((Mathf.Sqrt(3) * (i - 1)) / 2, 0, j), Quaternion.identity, gameObject.transform);

                        tl.name = "Hex_" + i + "_" + j;

                        tl.GetComponent<Tile>().Row = j;
                        tl.GetComponent<Tile>().Column = i;

                        TileSpace temp;

                        temp.T = tl;

                        temp.C = i;

                        temp.R = j;

                        GridList.Add(temp);


                    }
                }

                else
                {

                    for (int j = 1; j <= Rows; j++)

                    {



                        GameObject tl = Instantiate(BaseTile, new Vector3((Mathf.Sqrt(3) * (i - 1)) / 2, 0, j - .5f), Quaternion.identity, gameObject.transform);

                        tl.name = "Hex_" + i + "_" + j;

                        tl.GetComponent<Tile>().Row = j;
                        tl.GetComponent<Tile>().Column = i;

                        TileSpace temp;

                        temp.T = tl;

                        temp.C = i;

                        temp.R = j;

                        GridList.Add(temp);

                    }
                }
            }

        }

        foreach(TileSpace T in GridList)
        {

            if(T.C == LC || T.R == LR)
            {

                T.T.GetComponent<Tile>().FindNeighbors();

            }

        }


        LR = Rows;
        LC = Columns;


    }

    public void ResetGrid()
    {
        foreach(TileSpace tile in GridList)
        {
            DestroyImmediate(tile.T, true);




        }
        GridList.Clear();
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
}
