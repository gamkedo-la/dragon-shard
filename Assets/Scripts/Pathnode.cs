using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathnode : MonoBehaviour
{
    public GameObject Previous;
    public int MPRemain = 0;



    public GameObject CurrentOccupant = null;
    public int MPRequired = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetPath()
    {

        Previous = null;
        MPRemain = 0;
        if (GetComponent<Tile>().Current.GetComponent<Fader>() != null)
        {
            GetComponent<Tile>().Current.GetComponent<Fader>().ResetTile();
        }
        else if (GetComponent<Tile>().Current.GetComponent<FaderControler>() != null)
        {
            GetComponent<Tile>().Current.GetComponent<FaderControler>().ResetTile();
        }

    }

    public void Fade()
    {
        if (GetComponent<Tile>().Current.GetComponent<Fader>() != null)
        {
            GetComponent<Tile>().Current.GetComponent<Fader>().Fade();
        }
        else if (GetComponent<Tile>().Current.GetComponent<FaderControler>() != null)
        {
            GetComponent<Tile>().Current.GetComponent<FaderControler>().Fade();
        }
    }



    public int GetMPRequired()
    {
        return MPRequired;

    }

    public void SetMPRequired(int MP)
    {

        MPRequired = MP;

    }
}
