﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorInterface : MonoBehaviour
{
    public GameObject ScenarioPropertiesEditor;
    public GameObject TerrainSelector;


    public Terrain terrain;
    public Grid grid;


    // Start is called before the first frame update
    void Start()
    {
        //terrain.PaintingTerrain = true;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EditTerrainModeOn()
    {
        ScenarioPropertiesEditor.SetActive(false);
        TerrainSelector.SetActive(true);
        terrain.PaintingTerrain = true;

    }

    public void EditUnitsMode()
    {
        ScenarioPropertiesEditor.SetActive(false);
        TerrainSelector.SetActive(false);
        terrain.PaintingTerrain = false;

    }

    public void EditScenarioMode()
    {
        ScenarioPropertiesEditor.SetActive(true);
        TerrainSelector.SetActive(false);
        terrain.PaintingTerrain = false;
    }

}
