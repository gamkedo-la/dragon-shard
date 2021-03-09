using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clicker : MonoBehaviour
{
    public Unit CurrentUnit;
    public GameObject EndPoint;
    public bool SelectingAction = false;

    public bool ActionInProgress = false;

    public bool AIturn;

    public bool DwarfFireWaveTargeting = false;
    public bool DwarfFireballTargeting = false;

    public bool ElfSlowTargeting = false;
    public bool ElfTeleportTargeting = false;

    public bool ElfTeleportTargetingTwo = false;

    public UnitInfo unitInfo;

    public GameObject grid;

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (DwarfFireWaveTargeting == true)
        {
            Ray toMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rhInfo;
            bool didHit = Physics.Raycast(toMouse, out rhInfo, 50.0f);
            if (didHit)
            {

                if (rhInfo.collider.gameObject.tag == "map")
                {
                    CurrentUnit.GetComponent<DwarfMagic>().TargetFireWave(rhInfo.collider.gameObject);
                }
                else if (rhInfo.collider.gameObject.tag == "Unit")
                {
                    CurrentUnit.GetComponent<DwarfMagic>().TargetFireWave(rhInfo.collider.gameObject.GetComponent<Pathfinding>().CurrentLocation);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    DwarfFireWaveTargeting = false;
                    if (rhInfo.collider.gameObject.tag == "map")
                    {
                        CurrentUnit.GetComponent<DwarfMagic>().CastFireWave(rhInfo.collider.gameObject);
                    }
                    else if (rhInfo.collider.gameObject.tag == "Unit")
                    {
                        CurrentUnit.GetComponent<DwarfMagic>().CastFireWave(rhInfo.collider.gameObject.GetComponent<Pathfinding>().CurrentLocation);
                    }
                }
            }
            return;

        }
        if (ElfSlowTargeting == true)
        {
            Ray toMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rhInfo;
            bool didHit = Physics.Raycast(toMouse, out rhInfo, 50.0f);
            if (didHit)
            {

                if (rhInfo.collider.gameObject.tag == "map")
                {
                    CurrentUnit.GetComponent<ElfMagic>().SlowTargeting(rhInfo.collider.gameObject);
                }
                else if (rhInfo.collider.gameObject.tag == "Unit")
                {
                    CurrentUnit.GetComponent<ElfMagic>().SlowTargeting(rhInfo.collider.gameObject.GetComponent<Pathfinding>().CurrentLocation);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    ElfSlowTargeting = false;
                    if (rhInfo.collider.gameObject.tag == "map")
                    {
                        CurrentUnit.GetComponent<ElfMagic>().CastSlow(rhInfo.collider.gameObject);
                        return;
                    }
                    else if (rhInfo.collider.gameObject.tag == "Unit")
                    {
                        CurrentUnit.GetComponent<ElfMagic>().CastSlow(rhInfo.collider.gameObject.GetComponent<Pathfinding>().CurrentLocation);
                        return;
                    }
                    else
                    {
                        Clear();
                        return;
                    }
                }
            }
            return;
        }

        if (ElfTeleportTargeting == true)
        {
            if (ElfTeleportTargetingTwo == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray toMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit rhInfo;
                    bool didHit = Physics.Raycast(toMouse, out rhInfo, 50.0f);
                    if (didHit)
                    {
                        CurrentUnit.GetComponent<ElfMagic>().TeleportUnit(rhInfo.collider.gameObject);
                    }
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray toMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit rhInfo;
                    bool didHit = Physics.Raycast(toMouse, out rhInfo, 50.0f);
                    if (didHit)
                    {
                        CurrentUnit.GetComponent<ElfMagic>().TeleportTargetSelected(rhInfo.collider.gameObject);
                    }
                }
            }
        }


        else if (AIturn == false)
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
                            if (rhInfo.collider.gameObject.tag == "move button")
                            {
                                if (CurrentUnit.ActedThisTurn == false)
                                {
                                    CurrentUnit.HideOptions();
                                    CurrentUnit.GetComponent<Pathfinding>().GenerateMovementOptions();
                                    SelectingAction = true;
                                }
                                else
                                {
                                    Clear();
                                }
                                return;

                            }

                            else if (rhInfo.collider.gameObject.tag == "attack button")
                            {

                                if (CurrentUnit == null)
                                {
                                    Debug.LogError($"CurrentUnit null when it should not be! {nameof(Clicker)}");
                                }

                                if (CurrentUnit.GetComponent<Attack>().AttackedThisTurn == false)
                                {
                                    CurrentUnit.GetComponent<SelectAttack>().FindTargets();
                                    CurrentUnit.HideOptions();
                                    SelectingAction = true;
                                }

                                else
                                {
                                    Clear();
                                }

                                return;
                            }

                            else if (rhInfo.collider.gameObject.tag == "DamageBuffButton")
                            {
                                if (CurrentUnit.ActedThisTurn == false)
                                {
                                    CurrentUnit.GetComponent<HumanMagic>().BuffAttack();
                                    CurrentUnit.HideOptions();

                                }
                                else
                                {
                                    Clear();
                                }
                                return;
                            }

                            else if (rhInfo.collider.gameObject.tag == "DefenseBuffButton")
                            {
                                if (CurrentUnit.ActedThisTurn == false)
                                {
                                    CurrentUnit.GetComponent<HumanMagic>().BuffDeffense();
                                    CurrentUnit.HideOptions();

                                }
                                else
                                {
                                    Clear();
                                }
                                return;
                            }

                            else if (rhInfo.collider.gameObject.tag == "FireWaveButton")
                            {
                                if (CurrentUnit.ActedThisTurn == false)
                                {
                                    DwarfFireWaveTargeting = true;
                                    CurrentUnit.HideOptions();
                                    SelectingAction = true;
                                }
                                else
                                {
                                    Clear();
                                }
                                return;
                            }

                            else if (rhInfo.collider.gameObject.tag == "FireballButton")
                            {
                                if (CurrentUnit.ActedThisTurn == false)
                                {
                                    DwarfFireballTargeting = true;
                                    CurrentUnit.GetComponent<SelectAttack>().FindTargets();
                                    CurrentUnit.HideOptions();
                                    SelectingAction = true;
                                }
                                else
                                {
                                    Clear();
                                }
                                return;
                            }

                            else if (rhInfo.collider.gameObject.tag == "DwarfMortarButton")
                            {
                                if (CurrentUnit.GetComponent<Attack>().AttackedThisTurn == false)
                                {
                                    CurrentUnit.GetComponent<DwarfMortarAttack>().FindTargets();
                                    CurrentUnit.HideOptions();
                                    SelectingAction = true;
                                }

                                else
                                {
                                    Clear();
                                }


                                return;
                            }

                            else if (rhInfo.collider.gameObject.tag == "SlowButton")
                            {
                                if (CurrentUnit.ActedThisTurn == false)
                                {
                                    ElfSlowTargeting = true;
                                    CurrentUnit.HideOptions();
                                    SelectingAction = true;
                                }
                                else
                                {
                                    Clear();
                                }
                                return;
                            }

                            else if (rhInfo.collider.gameObject.tag == "TeleportButton")
                            {
                                if (CurrentUnit.ActedThisTurn == false)
                                {
                                    CurrentUnit.HideOptions();
                                    CurrentUnit.GetComponent<ElfMagic>().TeleportTargetSelect();
                                    ElfTeleportTargeting = true;
                                    SelectingAction = true;

                                }
                                else
                                {
                                    Clear();
                                }


                            }

                            else if (rhInfo.collider.GetComponent<Pathnode>() != null
                                && rhInfo.collider.GetComponent<Pathnode>().CurrentOccupant == null
                                && CurrentUnit != null
                                && SelectingAction == true
                                && CurrentUnit.GetComponent<Pathfinding>().CanMoveTo.Contains(rhInfo.collider.gameObject))
                            {
                                Debug.Log("moving to" + rhInfo.collider.name + " . . " + rhInfo.point);
                                EndPoint = rhInfo.collider.gameObject;
                                CurrentUnit.GetComponent<Pathfinding>().MoveTo(EndPoint);

                                SelectingAction = false;
                                ActionInProgress = true;
                                return;
                            }

                            else if (CurrentUnit != null
                                && CurrentUnit.GetComponent<SelectAttack>() != null
                                && CurrentUnit.GetComponent<SelectAttack>().ValidTargets.Contains(rhInfo.collider.gameObject) == true
                                && SelectingAction == true
                                && rhInfo.collider.gameObject.GetComponent<HitPoints>() != null
                                && DwarfFireballTargeting == false)
                            {

                                Debug.Log(CurrentUnit.name + " is fighting " + rhInfo.collider.name);
                                CurrentUnit.GetComponent<SelectAttack>().InitiateCombat(rhInfo.collider.gameObject);
                                SelectingAction = false;
                                ActionInProgress = true;

                                //return;

                            }

                            else if (CurrentUnit != null
                                && CurrentUnit.GetComponent<DwarfMortarAttack>() != null
                                && CurrentUnit.GetComponent<DwarfMortarAttack>().Targets.Contains(rhInfo.collider.gameObject) == true
                                && SelectingAction == true
                                && rhInfo.collider.gameObject.GetComponent<HitPoints>() != null)
                            {

                                Debug.Log(CurrentUnit.name + " is fighting " + rhInfo.collider.name);
                                CurrentUnit.GetComponent<DwarfMortarAttack>().FinalTarget = rhInfo.collider.gameObject;
                                SelectingAction = false;
                                ActionInProgress = true;

                                //return;

                            }

                            else if (CurrentUnit != null
                                && CurrentUnit.GetComponent<DwarfMagic>() != null
                                && CurrentUnit.GetComponent<SelectAttack>().ValidTargets.Contains(rhInfo.collider.gameObject) == true
                                && SelectingAction == true
                                && rhInfo.collider.gameObject.GetComponent<HitPoints>() != null
                                && DwarfFireballTargeting == true)
                            {

                                Debug.Log(CurrentUnit.name + " cast fireball at " + rhInfo.collider.name);
                                CurrentUnit.GetComponent<DwarfMagic>().CastFireball(rhInfo.collider.gameObject);
                                SelectingAction = false;
                                DwarfFireWaveTargeting = false;


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
    }

    public void Clear()
    {
        //Debug.Log("clear");
        if (CurrentUnit != null)
        {
            CurrentUnit.HideOptions();
        }

        if (unitInfo != null)
            unitInfo.DeselectUnit();

        CurrentUnit = null;
        EndPoint = null;
        ActionInProgress = false;
        SelectingAction = false;

        ElfSlowTargeting = false;
        ElfTeleportTargeting = false;
        ElfTeleportTargetingTwo = false;

        DwarfFireballTargeting = false;
        DwarfFireWaveTargeting = false;

        foreach (Transform TTT in grid.transform)
        {
            TTT.gameObject.GetComponent<Pathnode>().ResetPath();
        }
    }
}
