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
        Cam = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage, Experience attacker)
    {



        CurrentHP -= damage;
        Vector3 temp = transform.position;
        temp.y = .5f;

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
        var experience = GetComponent<Experience>().GetExpBonus();

        if (otherUnit != null)
        {
            otherUnit.RewardExp(experience);
        }

        Destroy(gameObject);

    }
}
