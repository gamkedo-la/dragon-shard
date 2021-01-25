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
        Cam = GetComponent<Unit>().Click.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {

        

        CurrentHP -= damage;
        Vector3 temp = transform.position;
        temp.y = 1.3f;

        GameObject T = Instantiate(DamageText, temp, Quaternion.identity);
        T.GetComponent<DamageText>().dam = damage;
        T.GetComponent<DamageText>().Camera = Cam;

        if(damage < 0)
        {

            T.GetComponent<TextMesh>().color = Color.green;
        }

        if(CurrentHP > MaxHP)
        {
            CurrentHP = MaxHP;
        }
        if(CurrentHP <= 0)
        {

            Death();
        }

    }

    public void Death()
    {
        //pre death stuff
        Destroy(gameObject);

    }
}
