using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAttack : MonoBehaviour
{
    public List<GameObject> ToBeChecked = new List<GameObject>();
    public List<GameObject> ValidTargets = new List<GameObject>();
    public List<GameObject> TargetLocations = new List<GameObject>();

    public Transform Grid;

    public GameObject CombatMenu;

    // Start is called before the first frame update
    void Start()
    {
        CombatMenu = GameObject.Find("CombatMenu");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FindTargets()
    {
        if(Grid == null)
        {
            Grid = GetComponent<Pathfinding>().Grid;

        }

        foreach(GameObject T in GetComponent<Pathfinding>().CurrentLocation.GetComponent<Tile>().Adjacent)
        {

            ToBeChecked.Add(T);
        }


       

        foreach(GameObject T in ToBeChecked)
        {
            if(T.GetComponent<Pathnode>().CurrentOccupant != null)
            {
                if(T.GetComponent<Pathnode>().CurrentOccupant.GetComponent<Unit>().Alliance != GetComponent<Unit>().Alliance)
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

    public void InitiateCombat(GameObject Enemy)
    {


        ToBeChecked.Clear();
        ValidTargets.Clear();
        TargetLocations.Clear();


        CombatMenu.GetComponent<CombatMenu>().Aggressor = gameObject;
        CombatMenu.GetComponent<CombatMenu>().Defender = Enemy;
        CombatMenu.GetComponent<CombatMenu>().Populate();


        CombatMenu.GetComponent<CanvasGroup>().alpha = 1;
        CombatMenu.GetComponent<CanvasGroup>().interactable = true;
        CombatMenu.GetComponent<CanvasGroup>().blocksRaycasts = true;


    }


}
