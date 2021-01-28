using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSelectorButton : MonoBehaviour
{
    public TileType thisTile;

    public Terrain T;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPaintBrush()
    {
        T.PBSet(thisTile);

    }

}
