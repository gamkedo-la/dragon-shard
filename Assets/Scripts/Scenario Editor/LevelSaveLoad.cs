using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSaveLoad : MonoBehaviour
{
    public Players players;
    public Grid grid;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SaveMap();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            LoadMap();
        }
    }

    public void SaveMap()
    {
        Debug.Log(grid.SaveString());
    }

    public void LoadMap()
    {
        Debug.Log("Load Map");
        grid.LoadString("11;20;CGGGGGGGGGCGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGWWWWWGGGGWWGGFGWGGGGWGGGFGGWGGWGGGFGGWGGGGWGGFGGGWGGWFFGFGGGWGGWFFFFFFFWGGWGGGFGGWGGGWGGGFGGGWGGWGGGFGGWWGGGWWWFGWWGGGGGGWWWGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGCGGGGGGGGGC");

    }
}
