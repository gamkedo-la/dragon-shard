using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameMenu : MonoBehaviour
{
    public string NextLevel;

    public Text W;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayEndGameMenu(List<int> winners)
    {
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().interactable = true;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        string winnertxt = " ";

        foreach(int i in winners)
        {
            winnertxt += "Player " + i;
        }

        Camera.main.GetComponent<Clicker>().ActionInProgress = true;

        if (winners.Count > 1)
        {
            W.text = "Winners:" + winnertxt;
        }
        else if(winners.Count ==1)
        {
            W.text = "Winner:" + winnertxt;
        }

    }

    public void DisplayEndGameMenu(bool HumanVictory)
    {
        if(HumanVictory == true)
        {
            W.text = "You are victorious";
        }
        else
        {
            W.text = "You have been defeated";
        }

    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(NextLevel);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
