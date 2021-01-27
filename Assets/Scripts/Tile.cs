using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType { Def, grass, water, forest }

[ExecuteInEditMode]
[SelectionBase]
[System.Serializable]
public class Tile : MonoBehaviour
{
    public int Row;
    public int Column;

    [SerializeField]
    public TileType thisTile;

    public GameObject DEFAULT;
    public GameObject WATER;
    public GameObject GRASS;
    public GameObject FOREST;

    [SerializeField]
    public GameObject Current;

    public List<GameObject> Adjacent = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        FindNeighbors();
        TileUpdate();
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


        if (thisTile == TileType.forest)
        {
            FOREST.SetActive(true);
            Current = FOREST;

            DEFAULT.SetActive(false);
            WATER.SetActive(false);
            GRASS.SetActive(false);

        }
        if (thisTile == TileType.water)
        {
            WATER.SetActive(true);
            Current = WATER;

            DEFAULT.SetActive(false);
            FOREST.SetActive(false);
            GRASS.SetActive(false);

        }
        if (thisTile == TileType.grass)
        {
            GRASS.SetActive(true);
            Current = GRASS;

            DEFAULT.SetActive(false);
            WATER.SetActive(false);
            FOREST.SetActive(false);

        }
        if (thisTile == TileType.Def)
        {
            DEFAULT.SetActive(true);
            Current = default;

            FOREST.SetActive(false);
            WATER.SetActive(false);
            GRASS.SetActive(false);

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
