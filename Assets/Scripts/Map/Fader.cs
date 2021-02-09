using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{

    public Material Normal;
    public Material Faded;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetTile()
    {

        GetComponent<MeshRenderer>().material = Normal;

    }

    public void Fade()
    {

        GetComponent<MeshRenderer>().material = Faded;
    }
}
