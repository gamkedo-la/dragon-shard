using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public string MeleeName;
    public int MeleeAttacks;
    public int MeleeDamage;

    public string RangedName;
    public int RangedAttacks;
    public int RangedDamage;

    //[HideInInspector]
    public HitPoints Target;

    [HideInInspector]
    public int thisAttack;
    [HideInInspector]
    public int thisDamage;
    [HideInInspector]
    public bool moddedDamage = false;

    [HideInInspector]
    public float DamageMod = 1;
    [HideInInspector]
    public int ModLength;

    private float timer = 0;
    private bool turn = true;
    private int counter = 0;
    private int enemyCounter = 0;

    [HideInInspector]
    public float EnemyDef;

    public int GrassDef;
    public int ForestDef;
    public int WaterDef;
    public int SandDef;
    public int HillsDef;
    public int CastleDef;

    [HideInInspector]
    public int CurrentDef;

    [HideInInspector]
    public float DefMod = 1;
    [HideInInspector]
    public int DModLength;

    [HideInInspector]
    public bool moddedDef = false;

    private Experience attacker;

    [HideInInspector]
    public bool MeleePrimary = false;

    public float DefenseLevelBuff = 1;

    public float AttackLevelBuff = 1;

    public bool AttackedThisTurn = false;

    // Start is called before the first frame update
    void Start()
    {
        if (attacker == null)
            attacker = GetComponent<Experience>();

        if((MeleeAttacks * MeleeDamage) > (RangedAttacks * RangedDamage))
        {
            MeleePrimary = true;
        }
    }

    public void TurnStart()
    {
        AttackedThisTurn = false;
        timer = 0;
        turn = true;
        counter = 0;
        enemyCounter = 0;

        if (DamageMod != AttackLevelBuff)
        {
            ModLength -= 1;
            if (ModLength <= 0)
            {
                DamageMod = AttackLevelBuff;
                moddedDamage = false;
            }
        }

        if (DefMod != DefenseLevelBuff)
        {
            DModLength -= 1;
            if (DModLength <= 0)
            {
                DefMod = DefenseLevelBuff;
                moddedDef = false;
                SetDef();
            }
        }
    }

    public void ModDamage(float mod, int length)
    {
        DamageMod += mod;
        ModLength = length;
        moddedDamage = true;

    }

    public void ModDef(float mod, int length)
    {
        DefMod += mod;
        DModLength = length;
        moddedDef = true;
        SetDef();

    }

    // Update is called once per frame
    void Update()
    {
        if (Target != null)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (turn == true)
                {
                    if (counter >= thisAttack)
                    {
                        if (enemyCounter >= Target.GetComponent<Attack>().thisAttack)
                        {
                            Target = null;

                            if (GetComponent<Unit>().controlledByAI == true)
                            {

                                GetComponent<Unit>().AIOverlord.GetComponent<Tracker>().NextUnit();

                            }

                                return;

                        }
                        turn = false;
                        return;

                    }
                    Att(Target, thisDamage, EnemyDef);
                    turn = false;
                    counter++;
                    timer = 1.0f;
                }
                else if (turn == false)
                {
                    if (enemyCounter >= Target.GetComponent<Attack>().thisAttack)
                    {
                        turn = true;
                        return;

                    }
                    Target.GetComponent<Attack>().Att(GetComponent<HitPoints>(), Target.GetComponent<Attack>().thisDamage, CurrentDef);
                    turn = true;
                    enemyCounter++;

                    timer = 1.0f;
                }
            }
        }
    }
    
    public void Att(HitPoints Enemy, int D, float Def)
    {
        int R = Random.Range(1, 101);
        AttackedThisTurn = true;
        GetComponent<Unit>().ActedThisTurn = true;

        if (R < Def)
        {
            Enemy.TakeDamage(0, attacker);
        }
        else
        {
            Enemy.TakeDamage(D, attacker);
        }
    }

    public void SetDef()
    {
        if (GetComponent<Pathfinding>().CurrentLocation.GetComponent<Tile>().GetTile() == TileType.forest)
        {
            CurrentDef = ForestDef;

        }
        if (GetComponent<Pathfinding>().CurrentLocation.GetComponent<Tile>().GetTile() == TileType.grass)
        {
            CurrentDef = GrassDef;

        }
        if (GetComponent<Pathfinding>().CurrentLocation.GetComponent<Tile>().GetTile() == TileType.water)
        {
            CurrentDef = WaterDef;

        }
        if (GetComponent<Pathfinding>().CurrentLocation.GetComponent<Tile>().GetTile() == TileType.sand)
        {
            CurrentDef = SandDef;

        }
        if (GetComponent<Pathfinding>().CurrentLocation.GetComponent<Tile>().GetTile() == TileType.castle)
        {
            CurrentDef = CastleDef;

        }
        if (GetComponent<Pathfinding>().CurrentLocation.GetComponent<Tile>().GetTile() == TileType.hills)
        {
            CurrentDef = HillsDef;

        }

        float CD = CurrentDef * DefMod;
        CurrentDef = (int)CD;

    }

}
