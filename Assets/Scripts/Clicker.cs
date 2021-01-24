using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    public Unit CurrentUnit;
    public GameObject EndPoint;
    public bool SelectingAction;

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
                    if (rhInfo.collider.gameObject.GetComponent<Unit>() != null
                        && rhInfo.collider.gameObject.GetComponent<Unit>().Owner == GetComponent<Players>().CurrentTurn
                        && SelectingAction == false)
                    {

                        Debug.Log(rhInfo.collider.name + " . . " + rhInfo.point);
                        CurrentUnit = rhInfo.collider.GetComponent<Unit>();

                        if (CurrentUnit != null)
                        {
                            CurrentUnit.DisplayOptions();

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


                        }

                        else if(rhInfo.collider.gameObject.tag == "attack button")
                        {

                            //do combat stuff here
                            CurrentUnit.HideOptions();
                           
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
                        }
                        else
                        {
                            CurrentUnit.HideOptions();
                            Clear();
                        }
                    }

                }

            }
        }

    }

    public void Clear()
    {
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
