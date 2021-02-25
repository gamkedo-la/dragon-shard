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

    Grid myGrid;

    List<Tile> tilesToUpdate = new List<Tile>();
    List<Tile> ring = new List<Tile>();

    private void OnEnable()
    {
        if (myTerrain == null)
        {
            myTerrain = GameObject.Find("TerrainEditor").GetComponent<Terrain>();
            myGrid = GameObject.Find("Grid").GetComponent<Grid>();


            myGrid.FindAllNeighbors();
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
        if(myGrid == null)
        {
            myGrid = GameObject.Find("Grid").GetComponent<Grid>();
            myGrid.FindAllNeighbors();
        }
        if (myTerrain != null)
        {
            Event e = Event.current;
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        //Debug.Log(myTerrain.PaintBrush);
                        Paint();
                    }
                    break;
                case EventType.MouseDrag:
                    if (e.button == 0) 
                    {
                        //Debug.Log(myTerrain.PaintBrush);
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

                tilesToUpdate.Add(T);

                if (myTerrain.BrushSize > 1)
                {
                    for(int i = 2; i <= myTerrain.BrushSize; i++)
                    {

                        foreach(Tile TT in tilesToUpdate)
                        {

                            foreach(GameObject adj in TT.Adjacent)
                            {

                                if(tilesToUpdate.Contains(adj.GetComponent<Tile>()) == false &&
                                    ring.Contains(adj.GetComponent<Tile>()) == false)
                                {
                                    ring.Add(adj.GetComponent<Tile>());

                                }
                            }
                        }
                        foreach(Tile R in ring)
                        {
                            tilesToUpdate.Add(R);

                        }
                    }                    
                }
            }

            foreach(Tile tile in tilesToUpdate)
            {
                Undo.RecordObject(tile.gameObject, "tile");
                tile.SetTile(myTerrain.PaintBrush);


                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                EditorUtility.SetDirty(tile);

                tile.TileUpdate();

            }
            tilesToUpdate.Clear();
            ring.Clear();
        }
    }
}
