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

    // Start is called before the first frame update
    void Start()
    {
        for(int k = 0; k<GM.ThisGame.Length; k++)
        {
            if(P != k)
            {

                foreach(GameObject U in GM.ThisGame[k].Units)
                {

                    Enemies.Add(U);

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
        if (MUArray[i] != null)
        {

            MUArray[i].GetComponent<Pathfinding>().GenerateMovementOptions();
            MUArray[i].GetComponent<Pathfinding>().MoveTo(FindDestination(MUArray[i]));
            MUArray[i].GetComponent<Unit>().AIActionTaken = true;
        }
        else
        {
            NextUnit();
        }

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
        EndPoints.Clear();

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

    public GameObject LongRange(GameObject U)
    {
        List<GameObject> LongTermPlan = new List<GameObject>();


        int temp = U.GetComponent<Pathfinding>().MovePoints;

        U.GetComponent<Pathfinding>().MovePoints = 1000;
        U.GetComponent<Pathfinding>().GenerateMovementOptions();

        U.GetComponent<Pathfinding>().MovePoints = temp;

        foreach(GameObject T in U.GetComponent<Pathfinding>().CanMoveTo)
        {
            foreach(GameObject TT in T.GetComponent<Tile>().Adjacent)
            {
                if (TT.GetComponent<Pathnode>().CurrentOccupant != null)
                {
                    if(TT.GetComponent<Pathnode>().CurrentOccupant.GetComponent<Unit>().Owner != P)
                    {
                        LongTermPlan.Add(T);
                    }
                }
            }
        }

        temp = 0;
        
        foreach(GameObject T in LongTermPlan)
        {
            if(T.GetComponent<Pathnode>().MPRemain > temp)
            {
                temp = T.GetComponent<Pathnode>().MPRemain;
            }
        }

        List<GameObject> garbage = new List<GameObject>();

        foreach(GameObject T in LongTermPlan)
        {
            if(T.GetComponent<Pathnode>().MPRemain < temp)
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

        int l = 0;

        for(int k = 0; k < Path.Length; k++)
        {

            if(Path[k].GetComponent<Pathnode>().MPRemain <= 0)
            {
                FinalPath.SetValue(Path[k], l);
                l++;
            }

        }

        l = 0;
        if(FinalPath[l].GetComponent<Pathnode>().MPRemain < 0)
        {
            l++;
        }

        EndPoint = FinalPath[l];
        

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
            
            temp = 10;

            foreach (GameObject T in Alternatives)
            {
                if (T.GetComponent<Pathnode>().MPRemain < temp)
                {
                    temp = T.GetComponent<Pathnode>().MPRemain;
                }
            }

            garbage.Clear();

            foreach (GameObject T in Alternatives)
            {
                if (T.GetComponent<Pathnode>().MPRemain > temp)
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
    }
}
