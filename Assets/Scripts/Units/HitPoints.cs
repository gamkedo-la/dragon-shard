using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitPoints : MonoBehaviour
{

    public int MaxHP;
    public int CurrentHP;

    public GameObject DamageText;

    GameObject Cam;

    // Start is called before the first frame update
    void Start()
    {
        Cam = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage, Experience attacker)
    {



        CurrentHP -= damage;
        Vector3 temp = transform.position;
        temp.y = .3f;

        GameObject T = Instantiate(DamageText, temp, Quaternion.identity);
        T.GetComponent<DamageText>().dam = damage;
        T.GetComponent<DamageText>().Camera = Cam;
        T.GetComponent<DamageText>().FSize = 500;

        T.GetComponent<DamageText>().TColor = Color.red;

        if (damage < -0)
        {

            T.GetComponent<DamageText>().TColor = Color.green;

        }
        if (damage == 0)
        {

            T.GetComponent<DamageText>().TColor = Color.white;
            T.GetComponent<DamageText>().FSize = 300;
        }



        if (CurrentHP > MaxHP)
        {
            CurrentHP = MaxHP;
        }
        if (CurrentHP <= 0)
        {
            Death(attacker);
        }

    }

    public void Death(Experience otherUnit)
    {
        //pre death stuff

        GetComponent<Unit>().GM.ThisGame[GetComponent<Unit>().Owner].Units.Remove(gameObject);

        GetComponent<Pathfinding>().CurrentLocation.GetComponent<Pathnode>().CurrentOccupant = null;

        if(GetComponent<Unit>().AIOverlord != null && GetComponent<Unit>().Owner == GetComponent<Unit>().GM.CurrentTurn)
        {
            GetComponent<Unit>().AIOverlord.GetComponent<Tracker>().NextUnit();
        }
        if(otherUnit.GetComponent<Unit>().AIOverlord != null && otherUnit.GetComponent<Unit>().Owner == otherUnit.GetComponent<Unit>().GM.CurrentTurn)
        {
            otherUnit.GetComponent<Unit>().AIOverlord.GetComponent<Tracker>().NextUnit();
        }

        var experience = GetComponent<Experience>().GetExpBonus();

        if (otherUnit != null)
        {
            otherUnit.RewardExp(experience);
        }

        GetComponent<Unit>().GM.UnitDeath(GetComponent<Unit>());

        Destroy(gameObject);
    }
}
