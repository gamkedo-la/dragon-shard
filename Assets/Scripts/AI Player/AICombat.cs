using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICombat : MonoBehaviour
{

    public List<GameObject> PotentialTargets = new List<GameObject>();

    public GameObject FinalTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LocateTarget(GameObject U)
    {
        PotentialTargets.Clear();
        FinalTarget = null;
        foreach (GameObject G in U.GetComponent<Pathfinding>().CurrentLocation.GetComponent<Tile>().Adjacent)
        {

            if (G.GetComponent<Pathnode>().CurrentOccupant != null)
            {
                if (G.GetComponent<Pathnode>().CurrentOccupant.GetComponent<Unit>().Owner != U.GetComponent<Unit>().Owner)
                {
                    PotentialTargets.Add(G.GetComponent<Pathnode>().CurrentOccupant);
                }
            }
        }

        if (PotentialTargets.Count == 0)
        {
            GetComponent<Tracker>().i++;
            GetComponent<Tracker>().NextUnit();
            return;
        }
        if (PotentialTargets.Count == 1)
        {
            GameObject[] PTArray = PotentialTargets.ToArray();
            FinalTarget = PTArray[0];

        }
        else
        {

            List<GameObject> temp = new List<GameObject>();

            int tempHP = 10000000;

            foreach (GameObject PT in PotentialTargets)
            {
                if (PT.GetComponent<HitPoints>().CurrentHP < tempHP)
                {
                    tempHP = PT.GetComponent<HitPoints>().CurrentHP;
                }
            }

            foreach (GameObject PT in PotentialTargets)
            {
                if (PT.GetComponent<HitPoints>().CurrentHP > tempHP)
                {
                    temp.Add(PT);
                }
            }

            foreach (GameObject PT in PotentialTargets)
            {
                if (temp.Contains(PT) == true)
                {
                    PotentialTargets.Remove(PT);
                }
            }

            temp.Clear();

            if (PotentialTargets.Count == 1)
            {
                GameObject[] PTArray = PotentialTargets.ToArray();
                FinalTarget = PTArray[0];

            }
            else
            {

                foreach (GameObject PT in PotentialTargets)
                {
                    if (PT.GetComponent<Attack>().MeleePrimary != U.GetComponent<Attack>().MeleePrimary)
                    {
                        temp.Add(PT);
                    }
                }

                if (temp.Count < PotentialTargets.Count)
                {
                    foreach (GameObject PT in PotentialTargets)
                    {
                        if (temp.Contains(PT) == true)
                        {
                            PotentialTargets.Remove(PT);
                        }
                    }

                    if (PotentialTargets.Count == 1)
                    {
                        GameObject[] PTArray = PotentialTargets.ToArray();
                        FinalTarget = PTArray[0];
                    }
                    else
                    {
                        GameObject[] PTArray = PotentialTargets.ToArray();
                        FinalTarget = PTArray[Random.Range(0, PTArray.Length)];
                    }

                }
                else
                {
                    GameObject[] PTArray = PotentialTargets.ToArray();
                    FinalTarget = PTArray[Random.Range(0, PTArray.Length)];

                }
            }

        }

        if(U.GetComponent<Attack>().MeleePrimary == true)
        {

            float DF = U.GetComponent<Attack>().MeleeDamage * U.GetComponent<Attack>().DamageMod;
            U.GetComponent<Attack>().thisDamage = (int)DF;
            U.GetComponent<Attack>().thisAttack = U.GetComponent<Attack>().MeleeAttacks;

            DF = FinalTarget.GetComponent<Attack>().MeleeDamage * FinalTarget.GetComponent<Attack>().DamageMod;
            FinalTarget.GetComponent<Attack>().thisDamage = (int)DF;
            FinalTarget.GetComponent<Attack>().thisAttack = FinalTarget.GetComponent<Attack>().MeleeAttacks;

            U.GetComponent<Attack>().EnemyDef = FinalTarget.GetComponent<Attack>().CurrentDef;

            U.GetComponent<Attack>().Target = FinalTarget.GetComponent<HitPoints>();

        }
        else
        {
            float DF = U.GetComponent<Attack>().RangedDamage * U.GetComponent<Attack>().DamageMod;
            U.GetComponent<Attack>().thisDamage = (int)DF;
            U.GetComponent<Attack>().thisAttack = U.GetComponent<Attack>().RangedAttacks;

            DF = FinalTarget.GetComponent<Attack>().RangedDamage * FinalTarget.GetComponent<Attack>().DamageMod;
            FinalTarget.GetComponent<Attack>().thisDamage = (int)DF;
            FinalTarget.GetComponent<Attack>().thisAttack = FinalTarget.GetComponent<Attack>().RangedAttacks;



            U.GetComponent<Attack>().EnemyDef = FinalTarget.GetComponent<Attack>().CurrentDef;

            U.GetComponent<Attack>().Target = FinalTarget.GetComponent<HitPoints>();

        }



    }
}
