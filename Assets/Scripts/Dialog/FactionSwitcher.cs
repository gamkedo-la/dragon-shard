using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionSwitcher : MonoBehaviour
{

    public DialogEvent thisDialog;
    public Unit U;

    public int NewFaction;

    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwitchUnit()
    {
        U.Owner = NewFaction;

        U.Alliance = U.GM.ThisGame[NewFaction].Alliance;

        U.SetColor();

        foreach(GameObject AI in U.GM.AIPlayers)
        {
            if(U.Alliance != AI.GetComponent<Tracker>().Team)
            {
                AI.GetComponent<Tracker>().Enemies.Add(U.gameObject);
            }
        }

        U.TurnStart();

    }
}
