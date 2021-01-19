using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int Owner;

    //public MovementStat[] movementStats;

    // Start is called before the first frame update
    void Start()
    {

        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnStart()
    {

        GetComponent<Pathfinding>().TurnStart();


    }

    [System.Serializable]
    public struct MovementStat
    {

        public TileType T;
        public int MovePointsUsed;


    }
}
