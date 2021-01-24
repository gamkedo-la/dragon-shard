using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int Owner;
    public Players GM;
    public Clicker Click;

    public GameObject Options;

    //public MovementStat[] movementStats;

    // Start is called before the first frame update
    void Start()
    {

        Options = transform.Find("Action Menu").gameObject;
        GM = GameObject.Find("Main Camera").GetComponent<Players>();
        Click = GM.thisClicker;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnStart()
    {

        GetComponent<Pathfinding>().TurnStart();


    }

    public void GetClicked()
    {
        if(GM.CurrentTurn == Owner)
        {

            DisplayOptions();

        }



    }


    public void DisplayOptions()
    {
        Options.SetActive(true);


    }

    public void HideOptions()
    {
        Options.SetActive(false);

    }


    [System.Serializable]
    public struct MovementStat
    {

        public TileType T;
        public int MovePointsUsed;


    }
}
