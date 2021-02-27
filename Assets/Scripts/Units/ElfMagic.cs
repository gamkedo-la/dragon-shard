using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfMagic : MonoBehaviour
{

    public GameObject Origin;

    public List<GameObject> AoE = new List<GameObject>();

    public float SlowAmount = .3f;

    public int TeleportRange = 3;

    public List<GameObject> ToBeChecked = new List<GameObject>();
    public List<GameObject> ValidTargets = new List<GameObject>();
    public List<GameObject> TargetLocations = new List<GameObject>();

    public List<GameObject> PossibleTeleportDestinations = new List<GameObject>();

    public Transform Grid;

    public GameObject TeleportTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SlowTargeting(GameObject G)
    {
        if (G != Origin)
        {
            //Origin = G;
            AoE.Clear();
            //Debug.Log("targeting slow spell");

            if (GetComponent<Pathfinding>().CurrentLocation.GetComponent<Tile>().Adjacent.Contains(G) == true)
            {
                Origin = G;
                AoE.Add(G);
                foreach (GameObject A in G.GetComponent<Tile>().Adjacent)
                {
                    if (GetComponent<Pathfinding>().CurrentLocation.GetComponent<Tile>().Adjacent.Contains(A) == false)
                    {

                        AoE.Add(A);
                    }
                }
            }
            else
            {
                return;
            }
            AoE.Remove(GetComponent<Pathfinding>().CurrentLocation);
        }
        foreach (Transform T in GetComponent<Pathfinding>().Grid)
        {
            T.GetComponent<Pathnode>().Fade();

        }
        foreach (GameObject A in AoE)
        {
            A.GetComponent<Pathnode>().Unfade();
        }

        GetComponent<Pathfinding>().CurrentLocation.GetComponent<Pathnode>().Unfade();

    }

    public void CastSlow(GameObject G)
    {

        if (G == Origin)
        {
            GetComponent<Unit>().ActedThisTurn = true;
            foreach (GameObject A in AoE)
            {
                if (A.GetComponent<Pathnode>().CurrentOccupant != null)
                {
                    A.GetComponent<Pathnode>().CurrentOccupant.GetComponent<Pathfinding>().Slow(SlowAmount);
                }
            }
        }
        GetComponent<Unit>().Click.Clear();

        return;

    }

    public void TeleportTargetSelect()
    {

        ToBeChecked.Clear();
        ValidTargets.Clear();
        TargetLocations.Clear();
        PossibleTeleportDestinations.Clear();

        if (Grid == null)
        {
            Grid = GetComponent<Pathfinding>().Grid;
        }

        foreach (GameObject T in GetComponent<Pathfinding>().CurrentLocation.GetComponent<Tile>().Adjacent)
        {
            ToBeChecked.Add(T);
        }

        foreach (GameObject T in ToBeChecked)
        {
            if (T.GetComponent<Pathnode>().CurrentOccupant != null)
            {
                if (T.GetComponent<Pathnode>().CurrentOccupant.GetComponent<Unit>().Owner == GetComponent<Unit>().Owner)
                {
                    ValidTargets.Add(T.GetComponent<Pathnode>().CurrentOccupant);
                    TargetLocations.Add(T);
                }
            }
        }

        foreach (Transform TTT in Grid)
        {
            if (TargetLocations.Contains(TTT.gameObject) == false)
            {
                TTT.GetComponent<Pathnode>().Fade();
            }
        }

    }

    public void TeleportTargetSelected(GameObject G)
    {
        if (ValidTargets.Contains(G) == true)
        {
            TeleportTarget = G;
            TeleportDesitinationSelect();
            GetComponent<Unit>().Click.ElfTeleportTargetingTwo = true;
        }
        else
        {
            GetComponent<Unit>().Click.Clear();
            ToBeChecked.Clear();
            ValidTargets.Clear();
            TargetLocations.Clear();
        }
    }

    public void TeleportDesitinationSelect()
    {
        foreach (GameObject T in GetComponent<Pathfinding>().CurrentLocation.GetComponent<Tile>().Adjacent)
        {
            PossibleTeleportDestinations.Add(T);
        }

        List<GameObject> temp = new List<GameObject>();

        for (int k = 2; k <= TeleportRange; k++)
        {
            foreach (GameObject T in PossibleTeleportDestinations)
            {
                foreach (GameObject TT in T.GetComponent<Tile>().Adjacent)
                {
                    if (PossibleTeleportDestinations.Contains(TT) == false)
                    {
                        temp.Add(TT);
                    }
                }
            }

            foreach (GameObject TTT in temp)
            {
                PossibleTeleportDestinations.Add(TTT);
            }
        }

        foreach (Transform T in Grid)
        {
            T.GetComponent<Pathnode>().Fade();          
        }
        foreach(GameObject TT in PossibleTeleportDestinations)
        {
            TT.GetComponent<Pathnode>().Unfade();
        }

    }

    public void TeleportUnit(GameObject Endpoint)
    {
        if(PossibleTeleportDestinations.Contains(Endpoint) == true)
        {
            TeleportTarget.GetComponent<Pathfinding>().PlaceUnit(Endpoint);
        }

        GetComponent<Unit>().Click.Clear();
        ToBeChecked.Clear();
        ValidTargets.Clear();
        TargetLocations.Clear();

        TeleportTarget = null;

        PossibleTeleportDestinations.Clear();

        GetComponent<Unit>().ActedThisTurn = true;
    }
}
