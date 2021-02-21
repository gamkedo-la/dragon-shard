using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Terrain : MonoBehaviour
{

    public bool PaintingTerrain = false;

    public TileType PaintBrush;

    public int BrushSize = 1;

    public Grid grid;

    List<Tile> tilesToUpdate = new List<Tile>();
    List<Tile> ring = new List<Tile>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (PaintingTerrain == true)
        {
            if (Input.GetMouseButton(0))
            {
                grid.FindAllNeighbors();
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
                tilesToUpdate.Add(T);

                if (BrushSize > 1)
                {
                    for (int i = 2; i <= BrushSize; i++)
                    {

                        foreach (Tile TT in tilesToUpdate)
                        {

                            foreach (GameObject adj in TT.Adjacent)
                            {

                                if (tilesToUpdate.Contains(adj.GetComponent<Tile>()) == false &&
                                    ring.Contains(adj.GetComponent<Tile>()) == false)
                                {
                                    ring.Add(adj.GetComponent<Tile>());

                                }
                            }
                        }
                        foreach (Tile R in ring)
                        {
                            tilesToUpdate.Add(R);

                        }
                    }
                }
            }

            foreach (Tile tile in tilesToUpdate)
            {
                tile.SetTile(PaintBrush);
                tile.TileUpdate();
            }
            tilesToUpdate.Clear();
            ring.Clear();
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
