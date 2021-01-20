using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathnode : MonoBehaviour
{
    public GameObject Previous;
    public int MPRemain = 0;



    private int CurrentOccupant = -1;
    public int MPRequired = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {

        Previous = null;
        MPRemain = 0;
        GetComponent<SpriteRenderer>().color = Color.white;

    }

    public void Fade()
    {
        GetComponent<SpriteRenderer>().color = Color.grey;

    }

    public int GetCurrentOccupant()
    {

        return CurrentOccupant;

    }

    public void SetCurrentOccupant(int O)
    {

        CurrentOccupant = O;
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
