using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType { Def, grass, water, forest, sand, hills, castle}

[ExecuteInEditMode]
[SelectionBase]
[System.Serializable]
public class Tile : MonoBehaviour
{
    public int Row;
    public int Column;

    public GameObject UnitAnchor;

    [SerializeField]
    public TileType thisTile;

    public GameObject DEFAULT;
    public GameObject WATER;
    public GameObject GRASS;
    public GameObject FOREST;
    public GameObject SAND;
    public GameObject HILLS;
    public GameObject CASTLE;

    public float WH;
    public float GH;
    public float FH;
    public float SH;
    public float HH;
    public float CH;

    Vector3 AnchorPos = new Vector3(0, 0, 0);

    public int AIDefense;


    [SerializeField]
    public GameObject Current;

    public List<GameObject> Adjacent = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        FindNeighbors();
        TileUpdate();
        AnchorPos = transform.position;
    }

    public void SetTile(TileType t)
    {

        thisTile = t;
    }
    public TileType GetTile()
    {
        return thisTile;
    }


    // Update is called once per frame
    void Update()
    {

        //TileUpdate();

    }

    public void TileUpdate()
    {
        AnchorPos = transform.position;

        if (thisTile == TileType.forest)
        {
            FOREST.SetActive(true);
            Current = FOREST;

            DEFAULT.SetActive(false);
            WATER.SetActive(false);
            GRASS.SetActive(false);
            SAND.SetActive(false);
            HILLS.SetActive(false);
            CASTLE.SetActive(false);

            AnchorPos.y += FH;
            UnitAnchor.transform.position = AnchorPos;

        }
        if (thisTile == TileType.water)
        {
            WATER.SetActive(true);
            Current = WATER;

            DEFAULT.SetActive(false);
            FOREST.SetActive(false);
            GRASS.SetActive(false);
            SAND.SetActive(false);
            HILLS.SetActive(false);
            CASTLE.SetActive(false);

            AnchorPos.y += WH;
            UnitAnchor.transform.position = AnchorPos;

        }
        if (thisTile == TileType.grass)
        {
            GRASS.SetActive(true);
            Current = GRASS;

            DEFAULT.SetActive(false);
            WATER.SetActive(false);
            FOREST.SetActive(false);
            SAND.SetActive(false);
            HILLS.SetActive(false);
            CASTLE.SetActive(false);

            AnchorPos.y += GH;
            UnitAnchor.transform.position = AnchorPos;

        }
        if (thisTile == TileType.Def)
        {
            DEFAULT.SetActive(true);
            Current = DEFAULT;

            FOREST.SetActive(false);
            WATER.SetActive(false);
            GRASS.SetActive(false);
            SAND.SetActive(false);
            HILLS.SetActive(false);
            CASTLE.SetActive(false);

            UnitAnchor.transform.position = AnchorPos;

        }

        if (thisTile == TileType.sand)
        {
            SAND.SetActive(true);
            Current = SAND;

            DEFAULT.SetActive(false);
            WATER.SetActive(false);
            FOREST.SetActive(false);
            GRASS.SetActive(false);
            HILLS.SetActive(false);
            CASTLE.SetActive(false);

            AnchorPos.y += SH;
            UnitAnchor.transform.position = AnchorPos;

        }
        if (thisTile == TileType.hills)
        {
            HILLS.SetActive(true);
            Current = HILLS;

            DEFAULT.SetActive(false);
            WATER.SetActive(false);
            FOREST.SetActive(false);
            SAND.SetActive(false);
            GRASS.SetActive(false);
            CASTLE.SetActive(false);

            AnchorPos.y += HH;
            UnitAnchor.transform.position = AnchorPos;

        }
        if (thisTile == TileType.castle)
        {
            CASTLE.SetActive(true);
            Current = CASTLE;

            DEFAULT.SetActive(false);
            WATER.SetActive(false);
            FOREST.SetActive(false);
            SAND.SetActive(false);
            HILLS.SetActive(false);
            GRASS.SetActive(false);

            AnchorPos.y += CH;
            UnitAnchor.transform.position = AnchorPos;

        }


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
