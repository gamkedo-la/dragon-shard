using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    public int HumanPlayersCount;

    public bool SinglePlayer = false;

    int Human;

    public Grid grid;

    public Material blue;
    public Material red; 
    public Material green;
    public Material purple;
    public Material orange;
    public Material yellow;

    public GameObject AIBrainPrefab;

    public UnitPlacer unitPlacer;
    public AddingPlayers addingPlayers;

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

        HumanPlayersCount = ThisGame.Length - AIPlayers.Count;

        if(HumanPlayersCount == 1)
        {
            SinglePlayer = true;
            for (int i = 0; i < ThisGame.Length; i++)
            {
                if(ThisGame[i].ControlledByAI == false)
                {
                    Human = i;
                }
            }

        }

        PlayersInThisGame = ThisGame.Length;
    }

    //public string SaveString()
    //{

    //}

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

        Debug.Log("Starting turn for player " + CurrentTurn);

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

        GameObject D = null;

        foreach(GameObject A in AIPlayers)
        {
            if(A.GetComponent<Tracker>().P == P)
            {
                D = A;
            }
            if(A.GetComponent<Tracker>().P > P)
            {
                A.GetComponent<Tracker>().P--;
            }
        }

        if (D != null)
        {
            AIPlayers.Remove(D);
            Destroy(D);
        }

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
        if(SinglePlayer == true)
        {
            if(ThisGame[Human].Eliminated == false)
            {
                EGM.DisplayEndGameMenu(true);
            }
            else
            {
                EGM.DisplayEndGameMenu(false);
            }
            return;
        }


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

    public string SavePlayers()
    {
        string Result = "";

        for(int z = 0; z < ThisGame.Length; z++)
        {
            Result += ThisGame[z].thisMaterial.name[11] + ",";
            Result += ThisGame[z].Alliance + ",";

            if(ThisGame[z].ControlledByAI == true)
            {
                Result += "a,";
            }
            else
            {
                Result += "h,";
            }

            if (unitPlacer == null)
            {
                Debug.Log("unit placer not found. BIG PROBLEM");
            }
            else {

                foreach (GameObject U in ThisGame[z].Units)
                {

                    switch (U.GetComponent<Unit>().UnitName)
                    {
                        case "Human Archer": Result += "HA_"; break;
                        case "Human Cavalry": Result += "HC_"; break;
                        case "Human Mage": Result += "HM_"; break;
                        case "Human Spearman": Result += "HS_"; break;
                        case "Dwarf Axe Thrower": Result += "DA_"; break;
                        case "Dwarf Infantry": Result += "DI_"; break;
                        case "Dwarf Mage": Result += "DMa_"; break;
                        case "Dwarf Mortar": Result += "DMo_"; break;
                        case "Elf Archer": Result += "EA_"; break;
                        case "Elf Scout": Result += "ESc_"; break;
                        case "Elf Swordsman": Result += "ESw_"; break;
                        case "Elf Mage": Result += "EM_"; break;

                    }
                    Result+= U.GetComponent<Pathfinding>().CurrentLocation.GetComponent<Tile>().Row + "_" + U.GetComponent<Pathfinding>().CurrentLocation.GetComponent<Tile>().Column + "_,";
                }
            }

            Result += ";";

        }



        return Result;
    }

    public void LoadPlayers(string p)
    {
        string[] splitString = p.Split(';');

        List<Unit> units = new List<Unit>();

        int PG = splitString.Length - 4;

        Debug.Log(PlayersInThisGame);


        if (addingPlayers != null)
        {
            for (int z = 1; z < PG; z++)
            {
                addingPlayers.AddPlayer();

            }
        }
        else
        {
            ThisGame = new Player[PG];
        }

        for(int z = 0; z < ThisGame.Length; z++)
        {
            //Debug.Log(splitString[(z + 3)]);

            string[] PlayerString = splitString[z +3].Split(',');

            ThisGame[z].Units = new List<GameObject>();

            //Debug.Log(PlayerString[0]);
            switch (PlayerString[0])
            {
                case "B": SetColor((z),blue); break;
                case "G": SetColor((z), green); break;
                case "R": SetColor((z), red); break;
                case "P": SetColor((z), purple); break;
                case "O": SetColor((z), orange); break;
                case "Y": SetColor((z), yellow); break;

            }

            //Debug.Log(PlayerString.Length);

            Debug.Log(PlayersInThisGame);
            ThisGame[z].Alliance = Convert.ToInt32(PlayerString[1]);

            GameObject AI = null;

            if (PlayerString[2] == "a")
            {
                ThisGame[z].ControlledByAI = true;
                AI = Instantiate(AIBrainPrefab);
                AI.GetComponent<Tracker>().P = z;
                AI.GetComponent<Tracker>().Grid = GameObject.Find("Grid").transform;
                AI.GetComponent<Tracker>().GM = this;
                AI.GetComponent<Tracker>().Team = ThisGame[z].Alliance;

            }

            unitPlacer.ActivePlayer = z;

            //Debug.Log("unit " + PlayerString[3]);
            for (int g = 3; g < PlayerString.Length - 1; g++)
            {
                    //Debug.Log("unit loop");

                    string[] UnitString = PlayerString[g].Split('_');

                   // Debug.Log("unit " + UnitString[0]);



                    switch (UnitString[0])
                    {
                        case "HA": unitPlacer.Active = unitPlacer.Units[1]; break;
                        case "HC": unitPlacer.Active = unitPlacer.Units[2]; break;
                        case "HM": unitPlacer.Active = unitPlacer.Units[3]; break;
                        case "HS": unitPlacer.Active = unitPlacer.Units[0]; break;
                        case "DA": unitPlacer.Active = unitPlacer.Units[5]; break;
                        case "DI": unitPlacer.Active = unitPlacer.Units[4]; break;
                        case "DMa": unitPlacer.Active = unitPlacer.Units[7]; break;
                        case "DMo": unitPlacer.Active = unitPlacer.Units[6]; break;
                        case "EA": unitPlacer.Active = unitPlacer.Units[8]; break;
                        case "ESc": unitPlacer.Active = unitPlacer.Units[9]; break;
                        case "ESw": unitPlacer.Active = unitPlacer.Units[10]; break;
                        case "EM": unitPlacer.Active = unitPlacer.Units[11]; break;
                    }

                    GameObject DropPoint = null;

                    foreach(Tile T in grid.GridList)
                    {
                        if(T.Row == Convert.ToInt32(UnitString[1]) && T.Column == Convert.ToInt32(UnitString[2]))
                        {
                            DropPoint = T.gameObject;
                        }
                    }

                if (AI != null)
                {
                    Debug.Log("ai" + AI.name);
                }

                units.Add(unitPlacer.Place(DropPoint, AI).GetComponent<Unit>());
            }

            // Debug.Log("post unit loop");
            if (PlayerString[2] == "a")
            {


                AI.GetComponent<Tracker>().FindUnits();
            }
        }
        foreach(Unit U in units)
        {
            U.SetColor();
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

        public bool Eliminated;

    }

}
