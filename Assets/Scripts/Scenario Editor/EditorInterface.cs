using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorInterface : MonoBehaviour
{

    public GameObject TerrainSelector;

    public Terrain terrain;
    public Grid grid;


    // Start is called before the first frame update
    void Start()
    {
        terrain.PaintingTerrain = true;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EditTerrainModeOn()
    {
        TerrainSelector.SetActive(true);


    }

    public void EditUnitsMode()
    {
        TerrainSelector.SetActive(false);

    }

}
