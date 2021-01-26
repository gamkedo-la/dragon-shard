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
    public int enemyCounter = 0;

    public float EnemyDef;


    public int GrassDef;
    public int ForestDef;
    public int WaterDef;

    public int CurrentDef;

    public string MeleeName;
    public string RangedName;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
        if(Target != null)
        {

            timer -= Time.deltaTime;
            if (timer <= 0)
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
                    Att(Target, thisDamage, EnemyDef);
                    turn = false;
                    counter ++;

                    timer = 1.0f;


                }
                else if(turn == false)
                {
                    if (enemyCounter >= Target.GetComponent<Attack>().thisAttack)
                    {
                        turn = true;
                        return;

                    }
                    Target.GetComponent<Attack>().Att(GetComponent<HitPoints>(), Target.GetComponent<Attack>().thisDamage, CurrentDef);
                    turn = true;
                    enemyCounter ++;

                    timer = 1.0f;


                }




            }


        }


    }


    public void Att(HitPoints Enemy, int D, float Chance)
    {
        int R = Random.Range(1, 101);

        if (R < Chance)
        {
            Enemy.TakeDamage(0);
        }
        else
        {

            Enemy.TakeDamage(D);
        }


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
