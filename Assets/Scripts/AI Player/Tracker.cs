﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tracker : MonoBehaviour
{
    public int P;
    public Players GM;

    public Transform Grid;

    public List<GameObject> Enemies = new List<GameObject>();
    public List<GameObject> MyUnits = new List<GameObject>();

    public GameObject[] MUArray;


    private List<GameObject> Checked = new List<GameObject>();
    private List<GameObject> ToCheck = new List<GameObject>();

    public List<GameObject> CanMoveTo = new List<GameObject>();

    public List<GameObject> CanAttack = new List<GameObject>();


    private List<GameObject> PotentialTargets = new List<GameObject>();

    GameObject Destination;

    GameObject CurrentUnit;

    bool ActionInprogress = false;

    public int i;

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
                    U.GetComponent<Unit>().controlledByAI = true;
                    U.GetComponent<Unit>().AIOverlord = gameObject;

                }
            }
        }
        MUArray = MyUnits.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        



    }

    public void TurnStart()
    {

        if (GM.CurrentTurn == P)
        {
            i = 0;
            foreach (GameObject U in MyUnits)
            {

                U.GetComponent<Unit>().AIActionTaken = false;

            }
            NextUnit();
        }
    }

    public void NextUnit()
    {

        if(i >= MUArray.Length)
        {

            GM.EndCurrentTurn();
            return;

        }

        MUArray[i].GetComponent<Pathfinding>().GenerateMovementOptions();
        MUArray[i].GetComponent<Pathfinding>().MoveTo(FindDestination(MUArray[i]));
        MUArray[i].GetComponent<Unit>().AIActionTaken = true;


    }


    public GameObject FindDestination(GameObject U)
    {

        bool a = false;

        foreach(GameObject t in U.GetComponent<Pathfinding>().CurrentLocation.GetComponent<Tile>().Adjacent)
        {
            if (t.GetComponent<Pathnode>().CurrentOccupant != null)
            {
                if (t.GetComponent<Pathnode>().CurrentOccupant.GetComponent<Unit>().Owner != P)
                {
                    a = true;

                }
            }

        }

        if(a == true)
        {

            //start combat here
            return null;

        }

        ToCheck.Clear();
        Checked.Clear();
        CanMoveTo.Clear();
        CanAttack.Clear();
        PotentialTargets.Clear();

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
            List<GameObject> EndPoints = new List<GameObject>();
            foreach(GameObject G in PotentialTargets)
            {

                foreach (GameObject tt in G.GetComponent<Pathfinding>().CurrentLocation.GetComponent<Tile>().Adjacent)
                {

                    if (CanMoveTo.Contains(tt) && tt.GetComponent<Pathnode>().CurrentOccupant == null)
                    {
                        EndPoints.Add(tt);

                    }

                }

            }


            if (EndPoints.Count >= 1)
            {
                //GameObject[] T = EndPoints.ToArray();
                //return T[Random.Range(0, T.Length)];

                foreach(GameObject T in EndPoints)
                {

                    if (T.GetComponent<Tile>().thisTile == TileType.forest)
                    {
                        T.GetComponent<Tile>().AIDefense = U.GetComponent<Attack>().ForestDef;

                    }
                    if (T.GetComponent<Tile>().thisTile == TileType.grass)
                    {
                        T.GetComponent<Tile>().AIDefense = U.GetComponent<Attack>().GrassDef;

                    }
                    if (T.GetComponent<Tile>().thisTile == TileType.hills)
                    {
                        T.GetComponent<Tile>().AIDefense = U.GetComponent<Attack>().HillsDef;

                    }
                    if (T.GetComponent<Tile>().thisTile == TileType.water)
                    {
                        T.GetComponent<Tile>().AIDefense = U.GetComponent<Attack>().WaterDef;

                    }
                    if (T.GetComponent<Tile>().thisTile == TileType.sand)
                    {
                        T.GetComponent<Tile>().AIDefense = U.GetComponent<Attack>().SandDef;

                    }
                    if (T.GetComponent<Tile>().thisTile == TileType.castle)
                    {
                        T.GetComponent<Tile>().AIDefense = U.GetComponent<Attack>().CastleDef;

                    }
                }

                EndPoints.Sort(delegate (GameObject x, GameObject y)
                {
                    return x.GetComponent<Tile>().AIDefense.CompareTo(y.GetComponent<Tile>().AIDefense);
                });

                GameObject[] EP = EndPoints.ToArray();

                List<GameObject> temp = new List<GameObject>();

                foreach(GameObject q in EndPoints)
                {
                    if(q.GetComponent<Tile>().AIDefense < EP[0].GetComponent<Tile>().AIDefense)
                    {
                        temp.Add(q);
                    }

                }
                foreach(GameObject q in temp)
                {
                    EndPoints.Remove(q);

                }

                temp.Clear();

                if(EndPoints.Count == 1)
                {
                    GameObject[] f = EndPoints.ToArray();
                    return f[0];

                }
                else
                {
                    EndPoints.Sort(delegate (GameObject x, GameObject y)
                    {
                        return x.GetComponent<Pathnode>().MPRemain.CompareTo(y.GetComponent<Pathnode>().MPRemain);
                    });

                    GameObject[] f = EndPoints.ToArray();

                    foreach (GameObject q in EndPoints)
                    {
                        if (q.GetComponent<Pathnode>().MPRemain < EP[0].GetComponent<Pathnode>().MPRemain)
                        {
                            temp.Add(q);
                        }

                    }

                    foreach (GameObject q in temp)
                    {
                        EndPoints.Remove(q);

                    }

                    temp.Clear();

                    GameObject[] h = EndPoints.ToArray();

                    return h[Random.Range(0, EndPoints.Count)];

                }

            }
            else
            {

                return AStar(U);
            }
            

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