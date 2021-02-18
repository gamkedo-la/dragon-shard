using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorInterface : MonoBehaviour
{
    public GameObject ScenarioPropertiesEditor;
    public GameObject TerrainSelector;
    public GameObject UnitEditor;


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
        ScenarioPropertiesEditor.GetComponent<CanvasGroup>().alpha = 0;
        ScenarioPropertiesEditor.GetComponent<CanvasGroup>().interactable = false;
        ScenarioPropertiesEditor.GetComponent<CanvasGroup>().blocksRaycasts = false;

        UnitEditor.GetComponent<CanvasGroup>().alpha = 0;
        UnitEditor.GetComponent<CanvasGroup>().interactable = false;
        UnitEditor.GetComponent<CanvasGroup>().blocksRaycasts = false;

        TerrainSelector.GetComponent<CanvasGroup>().alpha = 1;
        TerrainSelector.GetComponent<CanvasGroup>().interactable = true;
        TerrainSelector.GetComponent<CanvasGroup>().blocksRaycasts = true;

        terrain.PaintingTerrain = true;

    }

    public void EditUnitsMode()
    {

        ScenarioPropertiesEditor.GetComponent<CanvasGroup>().alpha = 0;
        ScenarioPropertiesEditor.GetComponent<CanvasGroup>().interactable = false;
        ScenarioPropertiesEditor.GetComponent<CanvasGroup>().blocksRaycasts = false;

        UnitEditor.GetComponent<CanvasGroup>().alpha = 1;
        UnitEditor.GetComponent<CanvasGroup>().interactable = true;
        UnitEditor.GetComponent<CanvasGroup>().blocksRaycasts = true;

        TerrainSelector.GetComponent<CanvasGroup>().alpha = 0;
        TerrainSelector.GetComponent<CanvasGroup>().interactable = false;
        TerrainSelector.GetComponent<CanvasGroup>().blocksRaycasts = false;

        terrain.PaintingTerrain = false;

    }

    public void EditScenarioMode()
    {

        ScenarioPropertiesEditor.GetComponent<CanvasGroup>().alpha = 1;
        ScenarioPropertiesEditor.GetComponent<CanvasGroup>().interactable = true;
        ScenarioPropertiesEditor.GetComponent<CanvasGroup>().blocksRaycasts = true;

        UnitEditor.GetComponent<CanvasGroup>().alpha = 0;
        UnitEditor.GetComponent<CanvasGroup>().interactable = false;
        UnitEditor.GetComponent<CanvasGroup>().blocksRaycasts = false;

        TerrainSelector.GetComponent<CanvasGroup>().alpha = 0;
        TerrainSelector.GetComponent<CanvasGroup>().interactable = false;
        TerrainSelector.GetComponent<CanvasGroup>().blocksRaycasts = false;

        terrain.PaintingTerrain = false;

    }

}
