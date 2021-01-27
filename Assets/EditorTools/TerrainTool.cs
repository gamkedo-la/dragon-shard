using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.EditorTools;

[EditorTool("Terrain Tool")]
public class TerrainTool : EditorTool
{

    [SerializeField]
    Texture2D icon;

    Terrain myTerrain;
    


    private void OnEnable()
    {
        myTerrain = GameObject.Find("TerrainEditor").GetComponent<Terrain>();
        
    }

    public override GUIContent toolbarIcon =>
        new GUIContent()
        {
            image = icon,
            text = "Terrain Tool",
            tooltip = "Edit terrain with this tool"

        };

    public override void OnToolGUI(EditorWindow window)
    {
        HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
        HandleMouse();

    }

    void HandleMouse()
    {
        if (myTerrain != null)
        {
            Event e = Event.current;
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        Debug.Log(myTerrain.PaintBrush);
                        Paint();
                    }
                    break;
                case EventType.MouseDrag:
                    if (e.button == 0) 
                    {
                        Debug.Log(myTerrain.PaintBrush);
                        Paint();
                    }
                    break;
            }
        }
        else
        {
            Debug.Log("Error: Cannot find terrain script. Please add TerrainEditor prefab to the scene");
        }


    }

    public void Paint()
    {
        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        RaycastHit rhInfo;
        if(Physics.Raycast(ray, out rhInfo, 10000))
        { 

            if (rhInfo.collider.gameObject.GetComponent<Tile>() != null)
            {
                Tile T = rhInfo.collider.GetComponent<Tile>();

                T.thisTile = myTerrain.PaintBrush;
                T.TileUpdate();

                if (myTerrain.BrushSize > 1)
                {
                    T.FindNeighbors();
                    foreach (GameObject TT in T.Adjacent)
                    {

                        TT.GetComponent<Tile>().thisTile = myTerrain.PaintBrush;
                        TT.GetComponent<Tile>().TileUpdate();

                    }
                }


            }

        }


    }





}
