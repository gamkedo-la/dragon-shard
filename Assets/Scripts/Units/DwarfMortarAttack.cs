using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfMortarAttack : MonoBehaviour
{

    private Pathfinding pathfinding;

    public List<GameObject> Range = new List<GameObject>();
    public List<GameObject> Targets = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        pathfinding = GetComponent<Pathfinding>();
     
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FindTargets()
    {
        Range.Clear();
        Targets.Clear();

        foreach (GameObject A in pathfinding.CurrentLocation.GetComponent<Tile>().Adjacent)
        {
            if (Range.Contains(A) == false)
            {
                Range.Add(A);
            }

            foreach (GameObject B in A.GetComponent<Tile>().Adjacent)
            {
                if (Range.Contains(B) == false)
                {
                    Range.Add(B);
                }
            }
        }

        foreach(GameObject R in Range)
        {

            if(R.GetComponent<Pathnode>().CurrentOccupant != null)
            {
                if(R.GetComponent<Pathnode>().CurrentOccupant.GetComponent<Unit>().Alliance != GetComponent<Unit>().Alliance)
                {
                    Targets.Add(R.GetComponent<Pathnode>().CurrentOccupant);
                }
            }

        }

    }

}
