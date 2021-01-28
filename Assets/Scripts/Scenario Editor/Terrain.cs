using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Terrain : MonoBehaviour
{

    public bool PaintingTerrain = false;

    public TileType PaintBrush;

    public int BrushSize = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PaintingTerrain == true)
        {
            if (Input.GetMouseButton(0))
            {
                Paint();

            }
        }
    }

    public void Paint()
    {
        Ray toMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rhInfo;
        bool didHit = Physics.Raycast(toMouse, out rhInfo, 50.0f);

        if (didHit == true)
        {

            if (rhInfo.collider.gameObject.GetComponent<Tile>() != null)
            {
                Tile T = rhInfo.collider.GetComponent<Tile>();

                T.SetTile( PaintBrush);
                T.TileUpdate();

                if (BrushSize > 1)
                {
                    T.FindNeighbors();
                    foreach (GameObject TT in T.Adjacent)
                    {

                        TT.GetComponent<Tile>().SetTile( PaintBrush);
                        TT.GetComponent<Tile>().TileUpdate();

                    }
                }


            }

        }


    }

    public void PTSet(bool b)
    {
        PaintingTerrain = b;

    }

    public bool PTGet()
    {
        return PaintingTerrain;

    }

    public void PTOn()
    {

        PaintingTerrain = true;
    }

    public void PTOff()
    {

        PaintingTerrain = false;
    }

    public void PBSet(TileType tile)
    {

        PaintBrush = tile;
    }

}
