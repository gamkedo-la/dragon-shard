﻿using System.Collections;
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

    private void Start()
    {

        GameObject[] StartingUnits = GameObject.FindGameObjectsWithTag("Unit");

        for (int i = 0; i < ThisGame.Length; i++)
        {
            ThisGame[i].Units = new List<GameObject>();

            foreach (GameObject U in StartingUnits)
            {
                if (U.GetComponent<Unit>().Owner == i)
                {
                    ThisGame[i].Units.Add(U);
                    U.GetComponent<Unit>().Alliance = ThisGame[i].Alliance;

                }
            }
        }
        foreach (GameObject AI in AIPlayers)
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
        if (CurrentTurn >= PlayersInThisGame)
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

    }

    public void SetColor(int P, Material M)
    {
        ThisGame[P].thisMaterial = M;
        ThisGame[P].thisColor = M.color;

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
