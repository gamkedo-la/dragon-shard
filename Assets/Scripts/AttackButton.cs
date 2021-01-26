﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{

    public Text AggDam;
    public Text AggAtt;
    public Text DefDam;
    public Text DefAtt;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Populate(int AD, int AA, int DD, int DA)
    {

        AggDam.text = AD.ToString() + " damage";
        AggAtt.text = AA.ToString() + " attacks";
        DefDam.text = DD.ToString() + " damage";
        DefAtt.text = DA.ToString() + " attacks";


    }
}
