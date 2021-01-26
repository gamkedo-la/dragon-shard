using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public int MeleeAttacks;
    public int MeleeDamage;
    public int RangedAttacks;
    public int RangedDamage;

    public HitPoints Target;

    public int thisAttack;
    public int thisDamage;

    public float timer = 0;
    public bool turn = true;
    public int counter = 0;
    public int enemyCounter;

    public float ChanceToGetHit;
    public float HitChance;


    public int GrassDef;
    public int ForestDef;
    public int WaterDef;

    public int CurrentDef;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Target != null)
        {
            if(timer <= 0)
            {
                if(turn == true)
                {
                    if(counter >= thisAttack)
                    {
                        if(enemyCounter >= Target.GetComponent<Attack>().thisAttack)
                        {
                            Target = null;
                            return;

                        }
                            turn = false;
                        return;

                    }
                    Att(Target, thisDamage, HitChance);
                    turn = false;
                    counter ++;

                    timer = 1.0f;


                }
                if(turn == false)
                {
                    if (enemyCounter >= Target.GetComponent<Attack>().thisAttack)
                    {
                        turn = true;
                        return;

                    }
                    Target.GetComponent<Attack>().Att(GetComponent<HitPoints>(), Target.GetComponent<Attack>().thisDamage, ChanceToGetHit);
                    turn = true;
                    enemyCounter ++;

                    timer = 1.0f;


                }




            }


        }


    }


    public void Att(HitPoints Enemy, int D, float Chance)
    {
        Enemy.TakeDamage(D);

    }

    public void SetDef()
    {
        if(GetComponent<Pathfinding>().CurrentLocation.GetComponent<Tile>().thisTile == TileType.forest)
        {
            CurrentDef = ForestDef;

        }
        if (GetComponent<Pathfinding>().CurrentLocation.GetComponent<Tile>().thisTile == TileType.grass)
        {
            CurrentDef = GrassDef;

        }
        if (GetComponent<Pathfinding>().CurrentLocation.GetComponent<Tile>().thisTile == TileType.water)
        {
            CurrentDef = WaterDef;

        }


    }




}
