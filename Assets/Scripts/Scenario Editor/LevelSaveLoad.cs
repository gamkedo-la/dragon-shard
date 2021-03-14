using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSaveLoad : MonoBehaviour
{
    public Players players;
    public Grid grid;

    public string LevelToLoad;

    public InputField input;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SaveMap();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            LoadMap("11;20;CGGGGGGGGGCGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGWWWWWGGGGWWGGFGWGGGGWGGGFGGWGGWGGGFGGWGGGGWGGFGGGWGGWFFGFGGGWGGWFFFFFFFWGGWGGGFGGWGGGWGGGFGGGWGGWGGGFGGWWGGGWWWFGWWGGGGGGWWWGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGCGGGGGGGGGC;P,0,h,HS_4_17_,HS_6_3_,HS_8_12_,;R,1,a,HS_2_7_,;G,2,h,HS_2_17_,HC_11_19_,HC_3_2_,HC_9_4_,;");
        }
        */
    }

    public void SaveMap()
    {
        string Result = grid.SaveString() + players.SavePlayers();

        Debug.Log(grid.SaveString());
        Debug.Log(players.SavePlayers());

        input.text = Result;
    }

    public void LoadMap()
    {
        Debug.Log("Load Map");
        grid.LoadString(LevelToLoad);
        players.LoadPlayers(LevelToLoad);

    }

    public void GetString(string M)
    {
        LevelToLoad = M;
    }
}
