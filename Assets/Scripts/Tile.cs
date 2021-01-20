using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType { unassigned, grass, water, forest }

[ExecuteInEditMode]
public class Tile : MonoBehaviour
{
    public int Row;
    public int Column;

    public TileType thisTile;

    public Sprite DEFAULT;
    public Sprite WATER;
    public Sprite GRASS;
    public Sprite FOREST;

    Sprite thisSprite;

    public List<GameObject> Adjacent = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        FindNeighbors();
    }

    // Update is called once per frame
    void Update()
    {
        TileUpdate();
    }

    public void TileUpdate()
    {
        if (thisTile == TileType.forest)
        {
            thisSprite = FOREST;

        }
        if (thisTile == TileType.water)
        {
            thisSprite = WATER;

        }
        if (thisTile == TileType.grass)
        {
            thisSprite = GRASS;

        }
        if (thisTile == TileType.unassigned)
        {
            thisSprite = DEFAULT;

        }
        GetComponent<SpriteRenderer>().sprite = thisSprite;


    }

    public void FindNeighbors()
    {
        Adjacent.Clear();

        Collider[] A = Physics.OverlapSphere(transform.position, 1);
        foreach (Collider T in A)
        {
            if (T.gameObject != gameObject && T.gameObject.tag == "map")
            {
                Adjacent.Add(T.gameObject);
            }
        }


    }


}
