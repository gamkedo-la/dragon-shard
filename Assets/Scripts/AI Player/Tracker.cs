using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    public int P;
    public Players GM;

    public Transform Grid;

    public List<GameObject> Enemies = new List<GameObject>();
    public List<GameObject> MyUnits = new List<GameObject>();


    private List<GameObject> Checked = new List<GameObject>();
    private List<GameObject> ToCheck = new List<GameObject>();

    public List<GameObject> CanMoveTo = new List<GameObject>();

    public List<GameObject> CanAttack = new List<GameObject>();


    private List<GameObject> PotentialTargets = new List<GameObject>();

    GameObject Destination;

    GameObject CurrentUnit;

    bool ActionInprogress = false;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i<GM.ThisGame.Length; i++)
        {
            if(P != i)
            {

                foreach(GameObject U in GM.ThisGame[i].Units)
                {

                    Enemies.Add(U);

                }
            }
            else
            {

                foreach (GameObject U in GM.ThisGame[i].Units)
                {

                    MyUnits.Add(U);

                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        



    }

    public void TurnStart()
    {

        if (GM.CurrentTurn == P)
        {
            foreach (GameObject U in MyUnits)
            {

                U.GetComponent<Pathfinding>().GenerateMovementOptions();
                U.GetComponent<Pathfinding>().MoveTo(FindDestination(U));


            }




        }


    }


    public GameObject FindDestination(GameObject U)
    {

        ToCheck.Clear();
        Checked.Clear();
        CanMoveTo.Clear();

        foreach (Transform node in Grid)
        {

            node.GetComponent<Pathnode>().SetMPRequired(1);

            if (node.GetComponent<Tile>().GetTile() == TileType.grass)
            {
                node.GetComponent<Pathnode>().SetMPRequired(U.GetComponent<Pathfinding>().Grass);
            }

            if (node.GetComponent<Tile>().GetTile() == TileType.forest)
            {
                node.GetComponent<Pathnode>().SetMPRequired(U.GetComponent<Pathfinding>().Forest);
            }

            if (node.GetComponent<Tile>().GetTile() == TileType.water)
            {
                node.GetComponent<Pathnode>().SetMPRequired(U.GetComponent<Pathfinding>().Water);
            }

            if (node.GetComponent<Tile>().GetTile() == TileType.hills)
            {
                node.GetComponent<Pathnode>().SetMPRequired(U.GetComponent<Pathfinding>().Hills);
            }

            if (node.GetComponent<Tile>().GetTile() == TileType.castle)
            {
                node.GetComponent<Pathnode>().SetMPRequired(U.GetComponent<Pathfinding>().Castle);
            }

            if (node.GetComponent<Tile>().GetTile() == TileType.sand)
            {
                node.GetComponent<Pathnode>().SetMPRequired(U.GetComponent<Pathfinding>().Sand);
            }

        }

        //Debug.Log("calculating movement");

        U.GetComponent<Pathfinding>().CurrentLocation.GetComponent<Pathnode>().MPRemain = U.GetComponent<Pathfinding>().MovePoints;

        ToCheck.Add(U.GetComponent<Pathfinding>().CurrentLocation);

        CanMoveTo.Add(U.GetComponent<Pathfinding>().CurrentLocation);

        FindAvailableTiles(U.GetComponent<Pathfinding>().CurrentLocation);


        foreach (GameObject T in ToCheck.ToArray())
        {

            FindAvailableTiles(T);
        }

        foreach(GameObject T in CanMoveTo)
        {
            CanAttack.Add(T);
            foreach(GameObject adj in T.GetComponent<Tile>().Adjacent)
            {

                if(CanAttack.Contains(adj) == false)
                {
                    CanAttack.Add(adj);

                }
            }
        }

        foreach( GameObject T in CanAttack)
        {
            if(T.GetComponent<Pathnode>().CurrentOccupant != null)
            {
                if (Enemies.Contains(T.GetComponent<Pathnode>().CurrentOccupant) == true)
                {
                    PotentialTargets.Add(T.GetComponent<Pathnode>().CurrentOccupant);
                }
            }

        }

        if(PotentialTargets.Count > 0)
        {

            GameObject[] G = PotentialTargets.ToArray();

            return G[Random.Range(0, G.Length)];

        }
        else
        {

            return AStar(U);


        }


    }

    void FindAvailableTiles(GameObject T)
    {
        if (Checked.Contains(T))
        {
            return;

        }
        Checked.Add(T);
        ToCheck.Remove(T);

        if (T.GetComponent<Pathnode>().CurrentOccupant != null)
        {
            if (T.GetComponent<Pathnode>().CurrentOccupant.GetComponent<Unit>().Owner != P)
            {
                T.GetComponent<Pathnode>().MPRemain = -1;

            }
        }
        //Debug.Log("checking " + T.name);

        if (T.GetComponent<Pathnode>().MPRemain >= 0)
        {
            if (CanMoveTo.Contains(T) == false)
            {

                CanMoveTo.Add(T);
            }
        }

        if (T.GetComponent<Pathnode>().MPRemain <= 0)
        {

            return;
        }

        foreach (GameObject adj in T.GetComponent<Tile>().Adjacent)
        {
            if (ToCheck.Contains(adj) || Checked.Contains(adj))
            {

                if (adj.GetComponent<Pathnode>().Previous != null
                    && adj.GetComponent<Pathnode>().Previous.GetComponent<Pathnode>().MPRemain < T.GetComponent<Pathnode>().MPRemain)
                {

                    adj.GetComponent<Pathnode>().Previous = T;
                    adj.GetComponent<Pathnode>().MPRemain = T.GetComponent<Pathnode>().MPRemain - adj.GetComponent<Pathnode>().GetMPRequired();



                }
            }
            else
            {

                adj.GetComponent<Pathnode>().Previous = T;
                adj.GetComponent<Pathnode>().MPRemain = T.GetComponent<Pathnode>().MPRemain - adj.GetComponent<Pathnode>().GetMPRequired();


            }

        }

        foreach (GameObject TT in T.GetComponent<Tile>().Adjacent)
        {
            if (ToCheck.Contains(TT) == false && Checked.Contains(TT) == false)
            {

                ToCheck.Add(TT);
            }
        }


        if (ToCheck.Count > 0)
        {
            foreach (GameObject T_T in ToCheck.ToArray())
            {
                FindAvailableTiles(T_T);
            }
        }


        //foreach(GameObject T in CurrentLocation.GetComponent<Tile>().Adjacent)
        //{

        //    if(Checked.Contains(T) == false)
        //    {

        //        Checked.Add(T)

        //    }

        //}


    }

    public GameObject AStar(GameObject U)
    {

        return null;


    }


}
