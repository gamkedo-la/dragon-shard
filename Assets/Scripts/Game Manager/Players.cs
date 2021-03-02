using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{

    public Player[] ThisGame;

    public int PlayersInThisGame = 1;

    public int CurrentTurn = 0;

    public Clicker thisClicker;

    public List<GameObject> AIPlayers = new List<GameObject>();

    public GameObject EndTurnButton;

    bool AIturn;

    public List<int> ActiveAlliances = new List<int>();

    int WinningAlliance;

    public EndGameMenu EGM;

    private void Start()
    {

        GameObject[] StartingUnits = GameObject.FindGameObjectsWithTag("Unit");

        for (int i = 0; i < ThisGame.Length; i++)
        {
            ThisGame[i].Units = new List<GameObject>();
            ThisGame[i].Eliminated = false;

            if(ActiveAlliances.Contains(ThisGame[i].Alliance) == false)
            {
                ActiveAlliances.Add(ThisGame[i].Alliance);
            }

            foreach (GameObject U in StartingUnits)
            {
                if (U.GetComponent<Unit>().Owner == i)
                {
                    ThisGame[i].Units.Add(U);
                    U.GetComponent<Unit>().Alliance = ThisGame[i].Alliance;

                }
            }

            ThisGame[i].thisColor = ThisGame[i].thisMaterial.color;
        }
        foreach (GameObject AI in AIPlayers)
        {
            ThisGame[AI.GetComponent<Tracker>().P].ControlledByAI = true;
        }

        PlayersInThisGame = ThisGame.Length;
    }

    public void ETB()
    {


        if (AIturn == false)
        {
            EndCurrentTurn();
        }
        else
        {
            Debug.Log("ai turn");
        }

    }

    public void EndCurrentTurn()
    {
        thisClicker.Clear();
               
        CurrentTurn += 1;

        if (CurrentTurn >= PlayersInThisGame)
        {
            CurrentTurn = 0;
        }

        if(ThisGame[CurrentTurn].Eliminated == true)
        {
            CurrentTurn += 1;
            if (CurrentTurn >= PlayersInThisGame)
            {
                CurrentTurn = 0;
            }
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

        if (ThisGame[CurrentTurn].ControlledByAI == true)
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

    public void AddPlayer()
    {
        Player[] FinalArray = new Player[ThisGame.Length + 1];
        for(int i = 0; i < ThisGame.Length; i++)
        {
            FinalArray[i] = ThisGame[i];
        }
        ThisGame = FinalArray;
        ThisGame[ThisGame.Length - 1].Units = new List<GameObject>();

        PlayersInThisGame++;

    }

    public void DeletePlayer(int P)
    {
        Player[] FinalArray = new Player[ThisGame.Length - 1];
        int i = 0;
        int j = 0;

        while(i < FinalArray.Length)
        {
            if(i +j == P)
            {
                j = 1;
            }
            else
            {
                FinalArray[i] = ThisGame[i + j];
                i++;
            }
        }
        ThisGame = FinalArray;
        PlayersInThisGame--;
    }

    public void UnitDeath(Unit U)
    {
        bool AllianceDefeat = false;

        ThisGame[U.Owner].Units.Remove(U.gameObject);

        if (ThisGame[U.Owner].Units.Count <= 0)
        {
            ThisGame[U.Owner].Eliminated = true;

            AllianceDefeat = true;

            for(int i = 0; i < ThisGame.Length; i++)
            {
                if(ThisGame[i].Eliminated == false)
                {
                    if(ThisGame[i].Alliance == U.Alliance)
                    {
                        AllianceDefeat = false;
                    }

                }

            }
            
        }

        if(AllianceDefeat == true)
        {
            ActiveAlliances.Remove(U.Alliance);

            if(ActiveAlliances.Count == 1)
            {
                foreach(int j in ActiveAlliances)
                {
                    WinningAlliance = j;
                }
                GameEnd(WinningAlliance);
            }
        }

        foreach (GameObject AI in AIPlayers)
        {
            if (U.Owner == AI.GetComponent<Tracker>().P)
            {
                AI.GetComponent<Tracker>().MyUnits.Remove(U.gameObject);
            }
            else if(U.Alliance != AI.GetComponent<Tracker>().Team)
            {
                AI.GetComponent<Tracker>().Enemies.Remove(U.gameObject);
            }
        }

    }


    public void SetColor(int P, Material M)
    {
        ThisGame[P].thisMaterial = M;
        ThisGame[P].thisColor = M.color;

        foreach (GameObject U in ThisGame[P].Units)
        {
            U.GetComponent<Unit>().SetColor();
        }    

    }

    public void GameEnd(int winners)
    {
        List<int> WinningPlayers = new List<int>();

        for (int i = 0; i < ThisGame.Length; i++)
        {
            if (ThisGame[i].Alliance == winners)
            {
                WinningPlayers.Add(i);
            }
        }

        EGM.DisplayEndGameMenu(WinningPlayers);

    }


    [System.Serializable]
    public struct Player
    {
        
        public Color thisColor;

        public int Alliance;

        public bool ControlledByAI;

        public List<GameObject> Units;

        public Material thisMaterial;

        public bool Eliminated;

    }

}
