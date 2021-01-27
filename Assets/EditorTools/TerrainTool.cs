using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[EditorTool("Terrain Tool")]
public class TerrainTool : EditorTool
{

    [SerializeField]
    Texture2D icon;

    Terrain myTerrain;
    


    private void OnEnable()
    {
        if (myTerrain == null)
        {
            myTerrain = GameObject.Find("TerrainEditor").GetComponent<Terrain>();
        }
    }

    public override GUIContent toolbarIcon =>
        new GUIContent()
        {
            image = icon,
            text = "Terrain Tool",
            tooltip = "Dragon Shard terrain editor"

        };

    public override void OnToolGUI(EditorWindow window)
    {
        HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
        HandleMouse();



    }

    void HandleMouse()
    {
        if (myTerrain == null)
        {
            myTerrain = GameObject.Find("TerrainEditor").GetComponent<Terrain>();
        }
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

                Undo.RecordObject(T.gameObject, "tile");
                T.SetTile(myTerrain.PaintBrush);

                
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                EditorUtility.SetDirty(T);

                T.TileUpdate();

                if (myTerrain.BrushSize > 1)
                {
                    T.FindNeighbors();
                    foreach (GameObject TT in T.Adjacent)
                    {
                        TT.GetComponent<Tile>().SetTile(myTerrain.PaintBrush);

                        TT.GetComponent<Tile>().TileUpdate();
                        EditorUtility.SetDirty(TT.GetComponent<Tile>());
                        

                    }
                }


            }

        }


    }





}
