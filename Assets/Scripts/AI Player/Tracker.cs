using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tracker : MonoBehaviour
{
    public bool Passive = false;

    public int P;

    public int Team;
    public Players GM;

    public Transform Grid;

    public List<GameObject> Enemies = new List<GameObject>();
    public List<GameObject> MyUnits = new List<GameObject>();

    public GameObject[] MUArray;
      
    private List<GameObject> Checked = new List<GameObject>();
    private List<GameObject> ToCheck = new List<GameObject>();

    public List<GameObject> CanMoveTo = new List<GameObject>();

    public List<GameObject> CanAttack = new List<GameObject>();

    public List<GameObject> EndPoints = new List<GameObject>();

    public List<GameObject> PotentialTargets = new List<GameObject>();

    GameObject Destination;

    GameObject CurrentUnit;

    bool ActionInprogress = false;

    public int i = 0;

    public GameObject[] f;
    public GameObject N;
    public GameObject EndPoint;

    public GameObject[] Path;
    public GameObject[] FinalPath;

    public List<GameObject> ThisMove = new List<GameObject>();
    public List<GameObject> Alternatives = new List<GameObject>();

    public List<GameObject> LongTermPlan = new List<GameObject>();

    public int LongRangeTemp = 0;

    public GameObject[] EnTemp;

    public List<GameObject> ET = new List<GameObject>();

    public int ii = 0;

    public float[] EnTempDist;

    // Start is called before the first frame update
    void Start()
    {


        Grid = GameObject.Find("Grid").transform;
        GM = Camera.main.GetComponent<Players>();
        Team = GM.ThisGame[P].Alliance;


    }

    public void FindUnits()
    {
        Enemies.Clear();
        MyUnits.Clear();

        for (int k = 0; k < GM.ThisGame.Length; k++)
        {
            if (P != k)
            {
                if (Team != GM.ThisGame[k].Alliance)
                {
                    foreach (GameObject U in GM.ThisGame[k].Units)
                    {
                        Enemies.Add(U);
                    }
                }
            }
            else
            {
                foreach (GameObject U in GM.ThisGame[k].Units)
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
        //MUArray = MyUnits.ToArray();

        if (GM.CurrentTurn == P)
        {
            i = -1;
            foreach (GameObject U in MyUnits)
            {
                if (U != null)
                {
                    U.GetComponent<Unit>().AIActionTaken = false;
                }
            }
            NextUnit();
        }
    }

    public void NextUnit()
    {
        i++;
        if(i >= MUArray.Length)
        {

            GM.EndCurrentTurn();
            return;

        }
        else if (MUArray[i] != null)
        {
            Grid.GetComponent<Grid>().ResetAllPathing();
            MUArray[i].GetComponent<Pathfinding>().GenerateMovementOptions();
            MUArray[i].GetComponent<Unit>().AIActionTaken = true;
            MUArray[i].GetComponent<Pathfinding>().MoveTo(FindDestination(MUArray[i]));

        }
        else
        {
            NextUnit();
        }

    }


    public GameObject FindDestination(GameObject U)
    {
        ToCheck.Clear();
        Checked.Clear();
        CanMoveTo.Clear();
        CanAttack.Clear();
        PotentialTargets.Clear();
        EndPoints.Clear();

        U.GetComponent<Pathfinding>().GenerateMovementOptions();

        bool a = false;

        foreach(GameObject t in U.GetComponent<Pathfinding>().CurrentLocation.GetComponent<Tile>().Adjacent)
        {
            if (t.GetComponent<Pathnode>().CurrentOccupant != null)
            {
                if (t.GetComponent<Pathnode>().CurrentOccupant.GetComponent<Unit>().Alliance != Team)
                {
                    a = true;

                }
            }
        }

        if(a == true)
        {
            return null;
        }



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

            if(node.GetComponent<Pathnode>().CurrentOccupant != null)
            {
                node.GetComponent<Pathnode>().SetMPRequired(1000);
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
                    if(T.GetComponent<Tile>().thisTile == TileType.Def)
                    {
                        T.GetComponent<Tile>().AIDefense = 40;
                    }
                }

                int floor = 0;

                List<GameObject> temp = new List<GameObject>();

                foreach(GameObject x in EndPoints)
                {
                    if(x.GetComponent<Tile>().AIDefense > floor)
                    {
                        floor = x.GetComponent<Tile>().AIDefense;
                    }
                }

                foreach(GameObject q in EndPoints)
                {
                    if(q.GetComponent<Tile>().AIDefense < floor)
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
                    if(f[0] == U.GetComponent<Pathfinding>().CurrentLocation)
                    {
                        GetComponent<AICombat>().LocateTarget(U);
                        return null;

                    }
                    else
                    {
                        return f[0];
                    }
                }

                else
                {
                    floor = 0;

                    foreach (GameObject x in EndPoints)
                    {
                        if (x.GetComponent<Pathnode>().MPRemain > floor)
                        {
                            floor = x.GetComponent<Pathnode>().MPRemain;
                        }
                    }

                    foreach (GameObject q in EndPoints)
                    {
                        if (q.GetComponent<Tile>().AIDefense < floor)
                        {
                            temp.Add(q);
                        }
                    }

                    foreach (GameObject q in temp)
                    {
                        EndPoints.Remove(q);
                    }
                    temp.Clear();

                    bool c = false;

                    foreach(GameObject G in EndPoints)
                    {
                        if(G == U.GetComponent<Pathfinding>().CurrentLocation)
                        {
                            c = true;
                        }
                    }

                    if (c == true)
                    {
                        GetComponent<AICombat>().LocateTarget(U);
                        return null;

                    }
                    else
                    {

                        GameObject[] h = EndPoints.ToArray();

                        GameObject i = h[Random.Range(0, EndPoints.Count)];

                        if (i == U.GetComponent<Pathfinding>().CurrentLocation)
                        {
                            GetComponent<AICombat>().LocateTarget(U);
                            return null;

                        }
                        else
                        {
                            return i;
                        }
                    }
                }
            }
            else
            {
                return LongRange(U);
            }            
        }
        else
        {
            return LongRange(U);
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

    /*

        shpere collide, if they are in the bubble take them off the list

        then A* what's left, heuristic can be very simple, vector3.distance

    */


    public GameObject LongRange(GameObject U)
    {

        if(Passive == true)
        {
            return null;
        }

        LongTermPlan.Clear();

        ET.Clear();        

        foreach(GameObject E in Enemies)
        {
            ET.Add(E);
        }

        foreach (GameObject E in Enemies)
        {
            if (ET.Contains(E) == true)
            {
                Collider[] A = Physics.OverlapSphere(E.transform.position, 3);
                foreach (Collider T in A)
                {
                    if (Enemies.Contains(T.gameObject) == true
                        && ET.Contains(T.gameObject) == true
                        && T.gameObject != E)
                    {
                        ET.Remove(T.gameObject);
                    }
                }
            }
        }

        EnTemp = ET.ToArray();

        EnTempDist = new float[EnTemp.Length];

        for(int z = 0; z < EnTemp.Length; z++)
        {
            EnTempDist[z] = AStar(U, EnTemp[z]);
        }
               
        ii = 0;
        float x = 10000000000000000000;
               
        for(int q = 0; q < EnTemp.Length; q++)
        {
            if(EnTempDist[q] < x)
            {
                ii = q;
                x = EnTempDist[q];
            }
        }

        EndPoint = EnTemp[ii].GetComponent<Pathfinding>().CurrentLocation;
         
        while(EndPoint.GetComponent<Pathnode>().MPRemain < 0)
        {
            EndPoint = EndPoint.GetComponent<Pathnode>().Previous;
        }       

        return EndPoint;
    }


    public float AStar(GameObject U, GameObject E)
    {
        GameObject End = E.GetComponent<Pathfinding>().CurrentLocation;

        GameObject Start = U.GetComponent<Pathfinding>().CurrentLocation;

        List<GameObject> OpenSet = new List<GameObject>();
        List<GameObject> ClosedSet = new List<GameObject>();

        OpenSet.Add(Start);

        Start.GetComponent<Pathnode>().g = 0;

        foreach (Transform T in Grid)
        {
            T.GetComponent<Pathnode>().h = Vector3.Distance(T.position, End.transform.position);
            T.GetComponent<Pathnode>().f = T.GetComponent<Pathnode>().h;
        }

        while(OpenSet.Count > 0)
        {
            GameObject T = null;
            float x = 10000000000000000000;

            foreach(GameObject B in OpenSet)
            {
                if(B.GetComponent<Pathnode>().f < x)
                {
                    x = B.GetComponent<Pathnode>().f;
                }
            }

            foreach (GameObject B in OpenSet)
            {
                if (B.GetComponent<Pathnode>().f == x)
                {
                    T = B;
                }                
            }

            if(T == null)
            {
                T = U;
                Debug.Log("big error in A*");
                return 0;
            }

            foreach(GameObject TT in T.GetComponent<Tile>().Adjacent)
            {
                if (ClosedSet.Contains(TT) == false)
                {

                    if (TT == End)
                    {
                        TT.GetComponent<Pathnode>().g = T.GetComponent<Pathnode>().g + TT.GetComponent<Pathnode>().MPRequired;
                        TT.GetComponent<Pathnode>().Previous = T;
                        TT.GetComponent<Pathnode>().MPRemain = -(int)TT.GetComponent<Pathnode>().g;
                        return TT.GetComponent<Pathnode>().g;
                    }

                    TT.GetComponent<Pathnode>().Previous = T;
                    TT.GetComponent<Pathnode>().g = T.GetComponent<Pathnode>().g + TT.GetComponent<Pathnode>().MPRequired;
                    TT.GetComponent<Pathnode>().f = TT.GetComponent<Pathnode>().h + TT.GetComponent<Pathnode>().g;
                    TT.GetComponent<Pathnode>().MPRemain = U.GetComponent<Pathfinding>().MovePoints - (int)TT.GetComponent<Pathnode>().g;

                    if(TT.GetComponent<Pathnode>().CurrentOccupant != null)
                    {
                        TT.GetComponent<Pathnode>().g = 10000000000000;
                    }

                    //Debug.Log("name " + TT.name +
                    //    " previous " + TT.GetComponent<Pathnode>().Previous.name +
                    //    " g " + TT.GetComponent<Pathnode>().g +
                    //    " h " + TT.GetComponent<Pathnode>().h +
                    //    " f " + TT.GetComponent<Pathnode>().f +
                    //    " MP Remain " + TT.GetComponent<Pathnode>().MPRemain);

                    if (OpenSet.Contains(TT) == false)
                    {
                        OpenSet.Add(TT);
                    }
                }
            }
            OpenSet.Remove(T);
            ClosedSet.Add(T);
        }

        Debug.Log("A* returned no value. BIG PROBLEM");
        return 0;

    }

    //public void NoMP()
    //{
    //    Debug.Log("no mp has been called");
    //    if(EEEEE.GetComponent<Pathnode>().MPRemain < 0)
    //    {
    //        EEEEE = EEEEE.GetComponent<Pathnode>().Previous;
    //    }

    //    if(EEEEE.GetComponent<Pathnode>().MPRemain < 0)
    //    {
    //        NoMP();
    //    }
    //    else
    //    {

    //        return;
    //    }

    //}


    /*

    public GameObject LongRange(GameObject U)
    {
        LongTermPlan.Clear();

        LongRangeTemp = U.GetComponent<Pathfinding>().MovePoints;

        U.GetComponent<Pathfinding>().MovePoints = 1000;
        U.GetComponent<Pathfinding>().GenerateMovementOptions();

        U.GetComponent<Pathfinding>().MovePoints = LongRangeTemp;

        foreach(GameObject T in U.GetComponent<Pathfinding>().CanMoveTo)
        {
            foreach(GameObject TT in T.GetComponent<Tile>().Adjacent)
            {
                if (TT.GetComponent<Pathnode>().CurrentOccupant != null)
                {
                    //deal with alliances

                    if(TT.GetComponent<Pathnode>().CurrentOccupant.GetComponent<Unit>().Alliance != Team)
                    {
                        LongTermPlan.Add(T);
                    }
                }
            }
        }

        LongRangeTemp = 0;
        
        foreach(GameObject T in LongTermPlan)
        {
            if(T.GetComponent<Pathnode>().MPRemain > LongRangeTemp)
            {
                LongRangeTemp = T.GetComponent<Pathnode>().MPRemain;
            }
        }

        List<GameObject> garbage = new List<GameObject>();

        foreach(GameObject T in LongTermPlan)
        {
            if(T.GetComponent<Pathnode>().MPRemain < LongRangeTemp)
            {
                garbage.Add(T);
            }
        }

        f = LongTermPlan.ToArray();

        N = f[Random.Range(0, f.Length)];

        EndPoint = N;

        ThisMove = new List<GameObject>();

        int j = 0;

        while (N.GetComponent<Pathnode>().Previous != null)
        {

            ThisMove.Add(N.GetComponent<Pathnode>().Previous);
            N = N.GetComponent<Pathnode>().Previous;

        }

        //Debug.Log("creating Path");
        Path = new GameObject[ThisMove.Count+1];
        //Debug.Log("Path created successfully");
        Path.SetValue(EndPoint, j);
        j++;

        N = EndPoint;

        while (N.GetComponent<Pathnode>().Previous != null)
        {

            Path.SetValue(N.GetComponent<Pathnode>().Previous, j);
            j++;
            N = N.GetComponent<Pathnode>().Previous;

        }

        U.GetComponent<Pathfinding>().GenerateMovementOptions();

        FinalPath = new GameObject[U.GetComponent<Pathfinding>().MovePoints];

        for(int k = Path.Length -1; k >= 0; k--)
        {

            if(Path[k].GetComponent<Pathnode>().MPRemain <= 0)
            {
                EndPoint = Path[k];
                break;
            }

        }

        if (EndPoint.GetComponent<Pathnode>().MPRemain < 0)
        {
            EndPoint = EndPoint.GetComponent<Pathnode>().Previous;
        }        

        if (EndPoint.GetComponent<Pathnode>().CurrentOccupant != null)
        {
            Debug.Log("finding alternate route");
            Alternatives = new List<GameObject>();

            foreach (GameObject T in EndPoint.GetComponent<Tile>().Adjacent)
            {
                if (T.GetComponent<Pathnode>().MPRemain >= 0)
                {
                    Alternatives.Add(T);
                }
            }

            int LRT = 10;

            foreach (GameObject T in Alternatives)
            {
                if (T.GetComponent<Pathnode>().MPRemain < LRT)
                {
                    LRT = T.GetComponent<Pathnode>().MPRemain;
                }
            }

            garbage.Clear();

            foreach (GameObject T in Alternatives)
            {
                if (T.GetComponent<Pathnode>().MPRemain > LongRangeTemp)
                {
                    garbage.Add(T);
                }
                if(T.GetComponent<Pathnode>().CurrentOccupant != null)
                {
                    garbage.Add(T);
                }
            }

            foreach (GameObject T in garbage)
            {
                Alternatives.Remove(T);
            }

            Alternatives.Remove(EndPoint);

            GameObject[] q = Alternatives.ToArray();
            EndPoint = q[Random.Range(0, q.Length)];            
        }
        return EndPoint;        
    }*/
}
