using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorInterface : MonoBehaviour
{

    public GameObject TerrainSelector;


    // Start is called before the first frame update
    void Start()
    {
        
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
