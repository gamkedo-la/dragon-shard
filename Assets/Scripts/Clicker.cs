using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    public Unit CurrentUnit;
    public GameObject EndPoint;
    public bool SelectingAction = false;

    public bool ActionInProgress = false;


    // Update is called once per frame
    void Update()
    {
        if (ActionInProgress == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray toMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit rhInfo;
                bool didHit = Physics.Raycast(toMouse, out rhInfo, 50.0f);
                if (didHit)
                {
                    if (CurrentUnit != null)
                    {
                        CurrentUnit.HideOptions();
                    }

                    if (rhInfo.collider.gameObject.GetComponent<Unit>() != null
                        && rhInfo.collider.gameObject.GetComponent<Unit>().Owner == GetComponent<Players>().CurrentTurn
                        && SelectingAction == false)
                    {

                        //Debug.Log(rhInfo.collider.name + " . . " + rhInfo.point);
                        CurrentUnit = rhInfo.collider.GetComponent<Unit>();

                        if (CurrentUnit != null)
                        {
                            CurrentUnit.DisplayOptions();
                            return;

                        }
                        else
                        {

                            //Debug.Log("clicked on empty space");
                        }

                    }
                    else
                    {
                        if(rhInfo.collider.gameObject.tag == "move button")
                        {
                            CurrentUnit.HideOptions();
                            CurrentUnit.GetComponent<Pathfinding>().GenerateMovementOptions();
                            SelectingAction = true;
                            return;


                        }

                        else if(rhInfo.collider.gameObject.tag == "attack button")
                        {
                            if(CurrentUnit == null)
                            {
                                Debug.LogError($"CurrentUnit null when it should not be! {nameof(Clicker)}");
                            }

                            CurrentUnit.GetComponent<SelectAttack>().FindTargets();
                            CurrentUnit.HideOptions();
                            SelectingAction = true;
                            return;                           
                        }

                        else if(rhInfo.collider.gameObject.tag == "DamageBuffButton")
                        {

                            CurrentUnit.GetComponent<HumanMagic>().BuffAttack();
                            CurrentUnit.HideOptions();
                            return;
                        }

                        else if (rhInfo.collider.gameObject.tag == "DefenseBuffButton")
                        {

                            CurrentUnit.GetComponent<HumanMagic>().BuffDeffense();
                            CurrentUnit.HideOptions();
                            return;
                        }


                        else if(rhInfo.collider.GetComponent<Pathnode>() != null 
                            && rhInfo.collider.GetComponent<Pathnode>().CurrentOccupant == null 
                            && CurrentUnit != null
                            && SelectingAction == true
                            && CurrentUnit.GetComponent<Pathfinding>().CanMoveTo.Contains(rhInfo.collider.gameObject))
                        {
                            Debug.Log(rhInfo.collider.name + " . . " + rhInfo.point);
                            EndPoint = rhInfo.collider.gameObject;
                            CurrentUnit.GetComponent<Pathfinding>().MoveTo(EndPoint);
                            SelectingAction = false;
                            ActionInProgress = true;
                            return;
                        }
                       
                        else if(CurrentUnit != null
                            && CurrentUnit.GetComponent<SelectAttack>().ValidTargets.Contains(rhInfo.collider.gameObject) == true
                            && SelectingAction == true
                            && rhInfo.collider.gameObject.GetComponent<HitPoints>() != null)
                        {

                            Debug.Log(CurrentUnit.name + " is fighting " + rhInfo.collider.name);
                            CurrentUnit.GetComponent<SelectAttack>().InitiateCombat(rhInfo.collider.gameObject);
                            SelectingAction = false;
                            ActionInProgress = true;
                            //return;



                        }



                        else
                        {
                            if (CurrentUnit != null)
                            {
                                CurrentUnit.HideOptions();
                            }
                            Clear();
                        }
                    }

                }

            }
        }

    }

    public void Clear()
    {
        Debug.Log("clear");
        if(CurrentUnit != null)
        {
            CurrentUnit.HideOptions();
        }

        CurrentUnit = null;
        EndPoint = null;
        ActionInProgress = false;
        SelectingAction = false;

        foreach (Transform TTT in GameObject.Find("Grid").transform)
        {
            TTT.gameObject.GetComponent<Pathnode>().ResetPath();
        }


    }

}
