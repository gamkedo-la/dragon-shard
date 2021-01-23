using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    public Pathfinding CurrentUnit;
    public GameObject EndPoint;

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
                    if (CurrentUnit == null 
                        && rhInfo.collider.gameObject.GetComponent<Unit>() != null 
                        && rhInfo.collider.gameObject.GetComponent<Unit>().Owner == GameObject.Find("GameManager").GetComponent<Players>().CurrentTurn)
                    {

                        Debug.Log(rhInfo.collider.name + " . . " + rhInfo.point);
                        CurrentUnit = rhInfo.collider.GetComponent<Pathfinding>();

                        if (CurrentUnit != null)
                        {
                            CurrentUnit.GenerateMovementOptions();

                        }
                        else
                        {

                            //Debug.Log("clicked on empty space");
                        }

                    }
                    else
                    {
                        if (rhInfo.collider.GetComponent<Pathnode>() != null 
                            && rhInfo.collider.GetComponent<Pathnode>().GetCurrentOccupant() == -1 
                            && CurrentUnit != null)
                        {
                            Debug.Log(rhInfo.collider.name + " . . " + rhInfo.point);
                            EndPoint = rhInfo.collider.gameObject;
                            CurrentUnit.GetComponent<Pathfinding>().MoveTo(EndPoint);
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

        foreach (Transform TTT in GameObject.Find("Grid").transform)
        {
            TTT.gameObject.GetComponent<Pathnode>().ResetPath();
        }


    }

}
