using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSize : MonoBehaviour
{

    public InputField R;
    public InputField C;

    public int row;
    public int col;

    public Grid grid;

    // Start is called before the first frame update
    void Start()
    {
        R.text = grid.Rows.ToString();
        C.text = grid.Columns.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        row = int.Parse(R.text);
        col = int.Parse(C.text);
    }

    public void UpdateGrid()
    {

        grid.UpdateRows(row);
        grid.UpdateColumns(col);

    }

    public void ValueChange()
    {



    }


}
