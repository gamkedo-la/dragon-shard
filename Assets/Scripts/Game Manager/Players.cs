using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{

    public Player[] ThisGame;

    public int CurrentTurn = 0;

    public Clicker thisClicker;

    public List<GameObject> AIPlayers = new List<GameObject>();

    public GameObject EndTurnButton;

    bool AIturn;

    private void Start()
    {
        thisClicker.GetComponent<Clicker>();
        GameObject[] StartingUnits = GameObject.FindGameObjectsWithTag("Unit");

        for (int i = 0; i < ThisGame.Length; i++)
        {
            ThisGame[i].Units = new List<GameObject>();

            foreach(GameObject U in StartingUnits)
            {
                if(U.GetComponent<Unit>().Owner == i)
                {
                    ThisGame[i].Units.Add(U);
                    U.GetComponent<Unit>().Alliance = ThisGame[i].Alliance;

                }
            }            
        }
        foreach(GameObject AI in AIPlayers)
        {
            ThisGame[AI.GetComponent<Tracker>().P].ControlledByAI = true;
        }
    }

    public void ETB()
    {


        if (AIturn == false)
        {
            EndCurrentTurn();
        }

    }

    public void EndCurrentTurn()
    {

            thisClicker.Clear();



            CurrentTurn += 1;
            if (CurrentTurn >= ThisGame.Length)
            {
                CurrentTurn = 0;

            }
            foreach (GameObject U in ThisGame[CurrentTurn].Units)
            {
                if (U != null)
                {
                    U.GetComponent<Unit>().TurnStart();
                }
            }
            foreach (GameObject AI in AIPlayers)
            {

                AI.GetComponent<Tracker>().TurnStart();
            }
            EndTurnButton.GetComponent<UIColors>().SetColors();

            if(ThisGame[CurrentTurn].ControlledByAI == true)
            {
                AIturn = true;
                thisClicker.AIturn = true;
            }
            else
            {
                AIturn = false;
                thisClicker.AIturn = false;
            }

        
    }


    [System.Serializable]
    public struct Player
    {
        
        public Color thisColor;

        public int Alliance;

        public bool ControlledByAI;

        public List<GameObject> Units;

        public Material thisMaterial;

    }

}
