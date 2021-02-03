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

                }

            }
            
        }
    }


    public void EndCurrentTurn()
    {
        thisClicker.Clear();



        CurrentTurn += 1;
        if(CurrentTurn >= ThisGame.Length)
        {
            CurrentTurn = 0;

        }
        foreach(GameObject U in ThisGame[CurrentTurn].Units)
        {
            U.GetComponent<Unit>().TurnStart();

        }
        foreach(GameObject AI in AIPlayers)
        {

            AI.GetComponent<Tracker>().TurnStart();
        }
        EndTurnButton.GetComponent<UIColors>().SetColors();

    }


    [System.Serializable]
    public struct Player
    {
        
        public Color thisColor;

        public int Gold;

        public List<GameObject> Units;

    }

}
