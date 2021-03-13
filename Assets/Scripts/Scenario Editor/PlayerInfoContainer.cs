using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoContainer : MonoBehaviour
{
    public Text number;

    public int PlayerRef;

    public Players players;

    public GameObject ThisAIBrain;

    public GameObject AIBrain;

    public AddingPlayers AP;

    public Material MyColor;

    public Image ColorDisplay;

    public int CurrentColor;

    public Dropdown Alliance;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(PlayerRef);
    }

    public void SetNumber(int N)
    {

        PlayerRef = N;
        number.text = "Player " + (PlayerRef + 1);
        //Debug.Log("number set " + PlayerRef);

    }
    public void SetAI(bool on)
    {
        if(on == true)
        {
            ThisAIBrain = CreateAIBrain(PlayerRef);
            Debug.Log("creating AI");
        }
        if(on == false)
        {
            players.AIPlayers.Remove(ThisAIBrain);
            Destroy(ThisAIBrain);
            ThisAIBrain = null;

        }

    }

    public GameObject CreateAIBrain(int P)
    {
        GameObject A = Instantiate(AIBrain);
        A.GetComponent<Tracker>().P = P;
        //A.GetComponent<Tracker>().GM = players;
        players.AIPlayers.Add(A);
        players.ThisGame[P].ControlledByAI = true;
        Debug.Log("created AI");
        return A;

    }

    public void HandleDropDown(int I)
    {
        if(I == 0)
        {
            SetAI(false);
        }
        if(I == 1)
        {
            SetAI(true);
            Debug.Log("creating AI");
        }

    }

    public void SetAlliance(int i)
    {
        players.ThisGame[PlayerRef].Alliance = i;
        Alliance.value = i;
    }

    public void DeleteMe()
    {
        if (AP.NumPlayers > 1)
        {
            AP.DeletePlayer(PlayerRef, gameObject);
            Destroy(gameObject);
        }
    }

    public void OpenColorsMenu()
    {
        AP.OpenColorsMenu(gameObject);
        AP.NewPlayer(false);

    }

    public void SetColor(Material M, int i)
    {
        CurrentColor = i;
        MyColor = M;
        ColorDisplay.color = M.color;
    }


}
