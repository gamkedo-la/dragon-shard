using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ArmorColor : MonoBehaviour
{
    public List<GameObject> ColoredPieces = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AssignColor(Material M)
    {
        foreach(GameObject G in ColoredPieces)
        {
            G.GetComponent<MeshRenderer>().material = M;

        }


    }

}
