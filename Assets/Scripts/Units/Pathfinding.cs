﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public GameObject CurrentLocation;

    private List<GameObject> Checked = new List<GameObject>();
    private List<GameObject> ToCheck = new List<GameObject>();

    public List<GameObject> CanMoveTo = new List<GameObject>();

    private List<GameObject> ThisMove = new List<GameObject>();

    private GameObject[] Path;

    public int MovePoints;

    [HideInInspector]
    public int MP;

    private bool moving = false;

    float t = 0;

    int step = 1;

    float mapSpeed = 5;

    Vector3 tempP;
    Vector3 tempN;

    public Transform Grid;

    Clicker thisClicker;

    public int Grass;
    public int Forest;
    public int Water;
    public int Sand;
    public int Hills;
    public int Castle;

    public bool slowed = false;
    public int slowAmt;

    int runs = 0;


    // Start is called before the first frame update
    void Start()
    {
        Grid = GameObject.Find("Grid").transform;
        thisClicker = Camera.main.GetComponent<Clicker>();
        
        MP = MovePoints;

        if (CurrentLocation == null)
        {
            RaycastHit rhInfo;
            bool didHit = Physics.Raycast(transform.position, Vector3.down, out rhInfo, 20.0f);
            if (didHit)
            {
                if (rhInfo.collider.gameObject.GetComponent<Pathnode>() != null)
                {
                    PlaceUnit(rhInfo.collider.gameObject);
                }
            }
        }
        GetComponent<Attack>().SetDef();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(moving == true)
        {

            transform.position = Vector3.Lerp(tempP, tempN, t);
            t += Time.deltaTime * mapSpeed;

            if(t >= 1)
            {
                CurrentLocation = Path[Path.Length - 1 - step];
                Path[Path.Length - 1 - step].GetComponent<Pathnode>().CurrentOccupant = gameObject;
                Path[Path.Length - step].GetComponent<Pathnode>().CurrentOccupant = null;
                MovePoints -= CurrentLocation.GetComponent<Pathnode>().GetMPRequired();

                GetComponent<Attack>().SetDef();

                if(Path.Length - 1 - step <= 0)
                {
                    moving = false;
                    Debug.Log("checked " + runs + " tiles");

                    step = 1;
                    Checked.Clear();
                    CanMoveTo.Clear();
                    ToCheck.Clear();
                    ThisMove.Clear();
                    thisClicker.Clear();

                    transform.position = CurrentLocation.GetComponent<Tile>().UnitAnchor.transform.position;

                    t = 0;

                    if (GetComponent<Unit>().controlledByAI == true)
                    {
                        GetComponent<Unit>().AIOverlord.GetComponent<AICombat>().LocateTarget(gameObject);
                    }

                    return;

                }
                step += 1;
                t = 0;
                tempP = Path[Path.Length - step].transform.position;
                tempP.y = transform.position.y;
                tempN = Path[Path.Length - step - 1].transform.position;
                tempN.y = transform.position.y;
                transform.LookAt(tempN);

            }
        }
    }

    public void PlaceUnit(GameObject T)
    {

        if(CurrentLocation != null)
        {
            CurrentLocation.GetComponent<Pathnode>().CurrentOccupant = null;
        }
        CurrentLocation = T;
        CurrentLocation.GetComponent<Pathnode>().CurrentOccupant = gameObject;
        transform.position = CurrentLocation.transform.position;

        transform.rotation = Quaternion.identity;

    }

    void FindAvailableTiles(GameObject T)
    {

        if (Checked.Contains(T))
        {
            return;

        }

        runs++;

        Checked.Add(T);
        ToCheck.Remove(T);

        if (T.GetComponent<Pathnode>().CurrentOccupant != null)
        {
            if (T.GetComponent<Pathnode>().CurrentOccupant.GetComponent<Unit>().Alliance != GetComponent<Unit>().Alliance)
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


    public void GenerateMovementOptions()
    {
        
        ToCheck.Clear();
        Checked.Clear();
        CanMoveTo.Clear();

        foreach(Transform node in Grid)
        {

            node.GetComponent<Pathnode>().SetMPRequired(1);

            TileType tileType = node.GetComponent<Tile>().GetTile();

            if (tileType == TileType.grass)
            {
                node.GetComponent<Pathnode>().SetMPRequired(Grass);
            }

            if (tileType == TileType.forest)
            {
                node.GetComponent<Pathnode>().SetMPRequired(Forest);
            }

            if (tileType == TileType.water)
            {
                node.GetComponent<Pathnode>().SetMPRequired(Water);
            }

            if (tileType == TileType.hills)
            {
                node.GetComponent<Pathnode>().SetMPRequired(Hills);
            }

            if (tileType == TileType.castle)
            {
                node.GetComponent<Pathnode>().SetMPRequired(Castle);
            }

            if (tileType == TileType.sand)
            {
                node.GetComponent<Pathnode>().SetMPRequired(Sand);
            }

        }

        //Debug.Log("calculating movement");

        CurrentLocation.GetComponent<Pathnode>().MPRemain = MovePoints;

        ToCheck.Add(CurrentLocation);

        CanMoveTo.Add(CurrentLocation);

        FindAvailableTiles(CurrentLocation);


        foreach (GameObject T in ToCheck.ToArray())
        {

            FindAvailableTiles(T);
        }

        foreach (Transform TTT in Grid)
        {

            if (CanMoveTo.Contains(TTT.gameObject) == false)
            {

                TTT.GetComponent<Pathnode>().Fade();

            }

        }


    }

    public void MoveTo(GameObject EndPoint)
    {

        if(CanMoveTo.Contains(EndPoint) == false || EndPoint == null)
        {

            Checked.Clear();
            CanMoveTo.Clear();
            ToCheck.Clear();
            ThisMove.Clear();
            thisClicker.Clear();

            if (GetComponent<Unit>().controlledByAI == true)
            {                
                GetComponent<Unit>().AIOverlord.GetComponent<AICombat>().LocateTarget(gameObject);
            }            
            
            if (EndPoint == null)
            {
                Debug.Log("EndPoint = null");
            }
            else
            {
                Debug.Log("invalid destination " + EndPoint.name);
            }

            return;
        }


        int i = 0;

        ThisMove.Add(EndPoint);

        GameObject N = EndPoint;

        while (N.GetComponent<Pathnode>().Previous != null)
        {

            ThisMove.Add(N.GetComponent<Pathnode>().Previous);
            N = N.GetComponent<Pathnode>().Previous;

        }

        //Debug.Log("creating Path");
        Path = new GameObject[ThisMove.Count];
        //Debug.Log("Path created successfully");
        Path.SetValue(EndPoint, i);
        i++;

        N = EndPoint;

        while(N.GetComponent<Pathnode>().Previous != null)
        {

            Path.SetValue(N.GetComponent<Pathnode>().Previous, i);
            i++;
            N = N.GetComponent<Pathnode>().Previous;

        }

        tempP = Path[Path.Length - 1].transform.position;
        tempP.y = transform.position.y;
        tempN = Path[Path.Length - 2].transform.position;
        tempN.y = transform.position.y;

        transform.LookAt(tempN);

        moving = true;
        thisClicker.ActionInProgress = true;


    }



    public void TurnStart()
    {
        MovePoints = MP;
        if(slowed == true)
        {
            MovePoints -= slowAmt;
            slowed = false;
        }
        runs = 0;
    }

    public void Slow(float percent)
    {
        slowed = true;

        float i = MP * percent;

        if (i < 1)
        {
            i = 1;
        }
        slowAmt = (int)i;

    }


}
