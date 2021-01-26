using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Terrain))]
public class TerrainEditor : Editor
{

    TileType PaintBrush;
    int BrushSize;

    bool PaintingTerrain;

    Terrain myTerrain;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();



        myTerrain = (Terrain)target;

        PaintBrush = myTerrain.PaintBrush;
        BrushSize = myTerrain.BrushSize;
        PaintingTerrain = myTerrain.PaintingTerrain;
    }


    private void OnSceneGUI()
    {




#if UNITY_EDITOR

        if (Event.current.type == EventType.MouseDrag || Event.current.type == EventType.MouseDown)
        {

            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            RaycastHit rhInfo;

            if (Physics.Raycast(ray, out rhInfo, 10000))
            {

                if (rhInfo.collider.gameObject.GetComponent<Tile>() != null)
                {
                    Tile T = rhInfo.collider.GetComponent<Tile>();

                    T.thisTile = PaintBrush;
                    T.TileUpdate();

                    if (BrushSize > 1)
                    {
                        T.FindNeighbors();
                        foreach (GameObject TT in T.Adjacent)
                        {

                            TT.GetComponent<Tile>().thisTile = PaintBrush;
                            TT.GetComponent<Tile>().TileUpdate();

                        }
                    }


                }


            }
            
        }
        if (PaintingTerrain)
        {
            Selection.activeGameObject = myTerrain.transform.gameObject;
        }

#endif
    }
}
