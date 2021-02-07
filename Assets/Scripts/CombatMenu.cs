using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatMenu : MonoBehaviour
{
    public Text AggName;
    public Text AggDef;
    public Text AggHP;
    public Text DefName;
    public Text DefDef;
    public Text DefHP;

    public Image AggColor;
    public Image DefColor;

    public GameObject Aggressor;
    public GameObject Defender;

    public Clicker click;

    public GameObject AttackButton1;
    public GameObject AttackButton2;

    bool noMelee = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Populate()
    {
        AttackButton1.GetComponent<CanvasGroup>().alpha = 1;
        AttackButton1.GetComponent<CanvasGroup>().interactable = true;
        AttackButton1.GetComponent<CanvasGroup>().blocksRaycasts = true;

        AttackButton2.GetComponent<CanvasGroup>().alpha = 1;
        AttackButton2.GetComponent<CanvasGroup>().interactable = true;
        AttackButton2.GetComponent<CanvasGroup>().blocksRaycasts = true;


        AggName.text = Aggressor.GetComponent<Unit>().UnitName;
        AggDef.text = "Defense: " + Aggressor.GetComponent<Attack>().CurrentDef.ToString() + "%";
        AggHP.text = "HP: " + Aggressor.GetComponent<HitPoints>().CurrentHP.ToString() + "/" + Aggressor.GetComponent<HitPoints>().MaxHP.ToString();

        DefName.text = Defender.GetComponent<Unit>().UnitName;
        DefDef.text = "Defense: " + Defender.GetComponent<Attack>().CurrentDef.ToString() + "%";
        DefHP.text = "HP: " + Defender.GetComponent<HitPoints>().CurrentHP.ToString() + "/" + Defender.GetComponent<HitPoints>().MaxHP.ToString();

        AggColor.color = Aggressor.GetComponent<Unit>().GM.ThisGame[Aggressor.GetComponent<Unit>().Owner].thisColor;

        DefColor.color = Defender.GetComponent<Unit>().GM.ThisGame[Defender.GetComponent<Unit>().Owner].thisColor;

        if(Aggressor.GetComponent<Attack>().MeleeAttacks <= 0)
        {

            noMelee = true;
        }

        if(noMelee == true)
        {
            float DF = Aggressor.GetComponent<Attack>().RangedDamage * Aggressor.GetComponent<Attack>().DamageMod;
            float DFD = Defender.GetComponent<Attack>().RangedDamage * Defender.GetComponent<Attack>().DamageMod;

            AttackButton1.GetComponent<AttackButton>().Populate((int)DF,
                Aggressor.GetComponent<Attack>().RangedAttacks,
                (int)DFD,
                Defender.GetComponent<Attack>().RangedAttacks, 
                "Ranged",
                Aggressor.GetComponent<Attack>().RangedName, 
                Defender.GetComponent<Attack>().RangedName);
            

        }
        else
        {
            float DF = Aggressor.GetComponent<Attack>().MeleeDamage * Aggressor.GetComponent<Attack>().DamageMod;
            float DFD = Defender.GetComponent<Attack>().MeleeDamage * Defender.GetComponent<Attack>().DamageMod;

            AttackButton1.GetComponent<AttackButton>().Populate((int)DF,
                Aggressor.GetComponent<Attack>().MeleeAttacks,
                (int)DFD,
                Defender.GetComponent<Attack>().MeleeAttacks,                
                "Melee",
                Aggressor.GetComponent<Attack>().MeleeName,
                Defender.GetComponent<Attack>().MeleeName);

            if(Aggressor.GetComponent<Attack>().RangedAttacks <= 0)
            {

                AttackButton2.GetComponent<CanvasGroup>().alpha = 0;
                AttackButton2.GetComponent<CanvasGroup>().interactable = false;
                AttackButton2.GetComponent<CanvasGroup>().blocksRaycasts = false;

            }
            else
            {

                AttackButton2.GetComponent<AttackButton>().Populate(Aggressor.GetComponent<Attack>().RangedDamage,
                    Aggressor.GetComponent<Attack>().RangedAttacks,
                    Defender.GetComponent<Attack>().RangedDamage,
                    Defender.GetComponent<Attack>().RangedAttacks,
                    "Ranged",
                    Aggressor.GetComponent<Attack>().RangedName,
                    Defender.GetComponent<Attack>().RangedName);

            }



        }

    }


    public void Cancel()
    {

        click.Clear();
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().interactable = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;

    }


    public void AB1()
    {

        

        if(noMelee == true)
        {
            float DF = Aggressor.GetComponent<Attack>().RangedDamage * Aggressor.GetComponent<Attack>().DamageMod;
            Aggressor.GetComponent<Attack>().thisDamage = (int)DF;
            Aggressor.GetComponent<Attack>().thisAttack = Aggressor.GetComponent<Attack>().RangedAttacks;

            DF = Defender.GetComponent<Attack>().RangedDamage * Defender.GetComponent<Attack>().DamageMod;
            Defender.GetComponent<Attack>().thisDamage = (int)DF;
            Defender.GetComponent<Attack>().thisAttack = Defender.GetComponent<Attack>().RangedAttacks;

            Aggressor.GetComponent<Attack>().Target = Defender.GetComponent<HitPoints>();


        }
        else
        {

            float DF = Aggressor.GetComponent<Attack>().MeleeDamage * Aggressor.GetComponent<Attack>().DamageMod;
            Aggressor.GetComponent<Attack>().thisDamage = (int)DF;
            Aggressor.GetComponent<Attack>().thisAttack = Aggressor.GetComponent<Attack>().MeleeAttacks;

            DF = Defender.GetComponent<Attack>().MeleeDamage * Defender.GetComponent<Attack>().DamageMod;
            Defender.GetComponent<Attack>().thisDamage = (int)DF;
            Defender.GetComponent<Attack>().thisAttack = Defender.GetComponent<Attack>().MeleeAttacks;



            Aggressor.GetComponent<Attack>().Target = Defender.GetComponent<HitPoints>();


        }

        Aggressor.GetComponent<Attack>().EnemyDef = Defender.GetComponent<Attack>().CurrentDef;
        Aggressor.GetComponent<Pathfinding>().MovePoints = 0;

        Aggressor.transform.LookAt(Defender.transform);
        Defender.transform.LookAt(Aggressor.transform);

        Cancel();

    }

    public void AB2()
    {

        float DF = Aggressor.GetComponent<Attack>().RangedDamage * Aggressor.GetComponent<Attack>().DamageMod;
        Aggressor.GetComponent<Attack>().thisDamage = (int)DF;
        Aggressor.GetComponent<Attack>().thisAttack = Aggressor.GetComponent<Attack>().RangedAttacks;

        DF = Defender.GetComponent<Attack>().RangedDamage * Defender.GetComponent<Attack>().DamageMod;
        Defender.GetComponent<Attack>().thisDamage = (int)DF;
        Defender.GetComponent<Attack>().thisAttack = Defender.GetComponent<Attack>().RangedAttacks;

        Aggressor.GetComponent<Attack>().Target = Defender.GetComponent<HitPoints>();

        Aggressor.GetComponent<Attack>().EnemyDef = Defender.GetComponent<Attack>().CurrentDef;

        Aggressor.GetComponent<Pathfinding>().MovePoints = 0;
        Aggressor.transform.LookAt(Defender.transform);

        Defender.transform.LookAt(Aggressor.transform);

        Cancel();
    }
}
