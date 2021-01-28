using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrushSize : MonoBehaviour
{

    public InputField B;

    public int brush;

    public Terrain terrain;

    // Start is called before the first frame update
    void Start()
    {
        B.text = terrain.BrushSize.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        brush = int.Parse(B.text);
    }

    public void UpdateBrushSize()
    {

        terrain.BrushSize = brush;
    }
}
