using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatMenu : MonoBehaviour
{
    public Text AggName;
    public Text AggDef;
    public Text DefName;
    public Text DefDef;

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

        DefName.text = Defender.GetComponent<Unit>().UnitName;
        DefDef.text = "Defense: " + Defender.GetComponent<Attack>().CurrentDef.ToString() + "%";

        AggColor.color = Aggressor.GetComponent<Unit>().GM.ThisGame[Aggressor.GetComponent<Unit>().Owner].thisColor;

        DefColor.color = Defender.GetComponent<Unit>().GM.ThisGame[Defender.GetComponent<Unit>().Owner].thisColor;

        if(Aggressor.GetComponent<Attack>().MeleeAttacks <= 0)
        {

            noMelee = true;
        }

        if(noMelee == true)
        {
            AttackButton1.GetComponent<AttackButton>().Populate(Aggressor.GetComponent<Attack>().RangedDamage,
                Aggressor.GetComponent<Attack>().RangedAttacks,
                Defender.GetComponent<Attack>().RangedDamage,
                Defender.GetComponent<Attack>().RangedAttacks);
            

        }
        else
        {
            AttackButton1.GetComponent<AttackButton>().Populate(Aggressor.GetComponent<Attack>().MeleeDamage,
                Aggressor.GetComponent<Attack>().MeleeAttacks,
                Defender.GetComponent<Attack>().MeleeDamage,
                Defender.GetComponent<Attack>().MeleeAttacks);

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
                    Defender.GetComponent<Attack>().RangedAttacks);

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
}
