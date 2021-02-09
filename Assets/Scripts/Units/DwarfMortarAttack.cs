using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfMortarAttack : MonoBehaviour
{

    private Pathfinding pathfinding;

    public List<GameObject> Range = new List<GameObject>();
    public List<GameObject> Targets = new List<GameObject>();
    public GameObject FinalTarget;

    public Transform Grid;

    private float timer = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        pathfinding = GetComponent<Pathfinding>();
    }

    // Update is called once per frame
    void Update()
    {
        if (FinalTarget != null)
        {
            timer -= Time.deltaTime;

            GetComponent<Attack>().Att(FinalTarget.GetComponent<HitPoints>(), 
                GetComponent<Attack>().RangedDamage, 
                FinalTarget.GetComponent<Attack>().CurrentDef);

            FinalTarget = null;

            GetComponent<Unit>().Click.Clear();
            //if (timer <= 0)
            //{


            //}
      
        }

    }

    public void TurnStart()
    {


    }

    public void FindTargets()
    {
        if (Grid == null)
        {
            Grid = GetComponent<Pathfinding>().Grid;

        }

        Range.Clear();
        Targets.Clear();

        foreach (GameObject A in pathfinding.CurrentLocation.GetComponent<Tile>().Adjacent)
        {
            if (Range.Contains(A) == false)
            {
                Range.Add(A);
            }

            foreach (GameObject B in A.GetComponent<Tile>().Adjacent)
            {
                if (Range.Contains(B) == false)
                {
                    Range.Add(B);
                }
            }
        }

        foreach(GameObject R in Range)
        {

            if(R.GetComponent<Pathnode>().CurrentOccupant != null)
            {
                if(R.GetComponent<Pathnode>().CurrentOccupant.GetComponent<Unit>().Alliance != GetComponent<Unit>().Alliance)
                {
                    Targets.Add(R.GetComponent<Pathnode>().CurrentOccupant);
                }
            }

        }

        foreach(Transform F in Grid)
        {
            if (Targets.Contains(F.GetComponent<Pathnode>().CurrentOccupant) == false)
            {
                if (F.GetComponent<Tile>().Current.GetComponent<Fader>() != null)
                {
                    F.GetComponent<Tile>().Current.GetComponent<Fader>().Fade();
                }
                else if (F.GetComponent<Tile>().Current.GetComponent<FaderControler>() != null)
                {

                    F.GetComponent<Tile>().Current.GetComponent<FaderControler>().Fade();
                }
            }
        }

    }

}
