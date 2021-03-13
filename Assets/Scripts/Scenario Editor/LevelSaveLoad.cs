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
            LoadMap("11;20;CGGGGGGGGGCGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGWWWWWGGGGWWGGFGWGGGGWGGGFGGWGGWGGGFGGWGGGGWGGFGGGWGGWFFGFGGGWGGWFFFFFFFWGGWGGGFGGWGGGWGGGFGGGWGGWGGGFGGWWGGGWWWFGWWGGGGGGWWWGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGCGGGGGGGGGC;P,0,h,;R,1,a,;G,2,h,;");
        }
    }

    public void SaveMap()
    {
        Debug.Log(grid.SaveString());
        Debug.Log(players.SavePlayers());
    }

    public void LoadMap(string m)
    {
        Debug.Log("Load Map");
        grid.LoadString(m);
        players.LoadPlayers(m);

    }
}
