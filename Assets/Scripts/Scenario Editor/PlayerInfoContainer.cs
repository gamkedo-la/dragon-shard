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

    public Transform G;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNumber(int N)
    {
        PlayerRef = N;
        number.text = "Player " + (PlayerRef + 1);
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
        A.GetComponent<Tracker>().Grid = G;
        players.AIPlayers.Add(A);
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

    }

}
