using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaderControler : MonoBehaviour
{
    public List<GameObject> mats = new List<GameObject>();


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

        foreach(GameObject g in mats)
        {
            g.GetComponent<Fader>().ResetTile();

        }

    }

    public void Fade()
    {
        foreach (GameObject g in mats)
        {
            g.GetComponent<Fader>().Fade();

        }
    }

}
