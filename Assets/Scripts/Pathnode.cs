using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathnode : MonoBehaviour
{
    public GameObject Previous;
    public int MPRemain = 0;

    GameObject F;

    private int CurrentOccupant = -1;
    public int MPRequired = 0;

    // Start is called before the first frame update
    void Start()
    {
        F = transform.Find("fade").gameObject;
        F.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {

        Previous = null;
        MPRemain = 0;
        F.SetActive(false);

    }

    public void Fade()
    {
        F.SetActive(true);

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
