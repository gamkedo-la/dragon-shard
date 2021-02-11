using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Unit : MonoBehaviour
{
    public int Owner;
    public Players GM;
    public Clicker Click;

    public int Alliance;

    public GameObject Options;

    public string UnitName;

    //public MovementStat[] movementStats;

    public bool controlledByAI;
    public bool AIActionTaken = false;

    public GameObject AIOverlord;

    GameObject Armor;

    public bool ActedThisTurn = false;

    // Start is called before the first frame update
    void Start()
    {

        Options = transform.Find("Action Menu").gameObject;
        GM = Camera.main.GetComponent<Players>();
        Click = GM.thisClicker;
        Armor = transform.Find("model").gameObject;
        Armor.GetComponent<ArmorColor>().AssignColor(GM.ThisGame[Owner].thisMaterial);


        if(Options.GetComponent<LookAt>() != null)
        {

            Options.GetComponent<LookAt>().Target = Camera.main.gameObject;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnStart()
    {
        ActedThisTurn = false;
        GetComponent<Pathfinding>().TurnStart();
        GetComponent<Attack>().TurnStart();


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
