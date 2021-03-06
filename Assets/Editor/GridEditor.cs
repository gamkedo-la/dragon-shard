﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Grid))]
public class GridEditor : Editor
{
    public override void OnInspectorGUI()
    {

        base.DrawDefaultInspector();

        Grid myGrid = (Grid)target;

        if (GUILayout.Button("Update Grid Size"))
        {
            myGrid.CreateGrid();
            myGrid.FindAllNeighbors();
            EditorUtility.SetDirty(myGrid);
        }

        if (GUILayout.Button("Clear"))
        {

            myGrid.ResetGrid();
            EditorUtility.SetDirty(myGrid);

        }

    }
}
