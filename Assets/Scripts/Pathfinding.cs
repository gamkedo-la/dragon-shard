using System.Collections;
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

    int MP;

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


    // Start is called before the first frame update
    void Start()
    {
        Grid = GameObject.Find("Grid").transform;
        thisClicker = GetComponent<Unit>().Click;
        
        MP = MovePoints;
        RaycastHit rhInfo;
        bool didHit = Physics.Raycast(transform.position, Vector3.down, out rhInfo, 20.0f);
        if (didHit)
        {
            CurrentLocation = rhInfo.collider.gameObject;
            CurrentLocation.GetComponent<Pathnode>().CurrentOccupant = gameObject;
            transform.position = CurrentLocation.transform.position;

            transform.Rotate(-90, 0, 0);

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
                CurrentLocation.GetComponent<Pathnode>().CurrentOccupant = gameObject;
                Path[Path.Length - step].GetComponent<Pathnode>().CurrentOccupant = null;
                MovePoints -= CurrentLocation.GetComponent<Pathnode>().GetMPRequired();

                GetComponent<Attack>().SetDef();

                if(Path.Length - 1 - step <= 0)
                {
                    moving = false;

                    step = 1;
                    Checked.Clear();
                    CanMoveTo.Clear();
                    ToCheck.Clear();
                    ThisMove.Clear();
                    thisClicker.Clear();

                    Vector3 temp = transform.position;
                    
                    transform.position = temp;

                    t = 0;

                    return;

                }
                step += 1;
                t = 0;
                tempP = Path[Path.Length - step].transform.position;
                tempP.y = transform.position.y;
                tempN = Path[Path.Length - step - 1].transform.position;
                tempN.y = transform.position.y;

            }

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
            if (T.GetComponent<Pathnode>().CurrentOccupant.GetComponent<Unit>().Owner != GetComponent<Unit>().Owner)
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

            if (node.GetComponent<Tile>().GetTile() == TileType.grass)
            {

                node.GetComponent<Pathnode>().SetMPRequired(Grass);
            }

            if (node.GetComponent<Tile>().GetTile() == TileType.forest)
            {

                node.GetComponent<Pathnode>().SetMPRequired(Forest);
            }
            if (node.GetComponent<Tile>().GetTile() == TileType.water)
            {

                node.GetComponent<Pathnode>().SetMPRequired(Water);
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

        if(CanMoveTo.Contains(EndPoint) == false)
        {

            Checked.Clear();
            CanMoveTo.Clear();
            ToCheck.Clear();
            ThisMove.Clear();
            thisClicker.Clear();

            Debug.Log("invalid destination");
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

        moving = true;
        thisClicker.ActionInProgress = true;


    }



    public void TurnStart()
    {
        MovePoints = MP;

    }


}
