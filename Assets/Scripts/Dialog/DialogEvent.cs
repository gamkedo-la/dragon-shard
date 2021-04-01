using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogEvent : MonoBehaviour
{
    public DialogLine[] ThisDialog;

    public DialogUI DialogBox;

    [HideInInspector]
    public int Bookmark = 0;

    public bool finished = false;

    public bool EndOfLevel = false;

    [Header("Only for if this dialog is at the end of the level")]
    public bool PlayerVictory;
    public EndGameMenu EGM;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextLine()
    {
        if(Bookmark == 0)
        {
            DialogBox.ToggleVisible(true);
            DialogBox.dialogEvent = this;

            Camera.main.GetComponent<Clicker>().ActionInProgress = true;
        }

        if(Bookmark >= ThisDialog.Length)
        {
            DialogBox.ToggleVisible(false);
            DialogBox.dialogEvent = null;
            finished = true;
            Camera.main.GetComponent<Clicker>().ActionInProgress = false;

            if(EndOfLevel == true)
            {
                EGM.DisplayEndGameMenu(PlayerVictory);
            }

        }

        if(Bookmark < ThisDialog.Length)
        {
            DialogBox.Populate(ThisDialog[Bookmark].Portrait, 
                ThisDialog[Bookmark].SpeakerName, 
                ThisDialog[Bookmark].Content, 
                ThisDialog[Bookmark].RightSide);
            Bookmark++;
        }
    }

    public void ResetDialog()
    {
        finished = false;
        Bookmark = 0;
    }


    [System.Serializable]
    public struct DialogLine
    {
        public string SpeakerName;
        public string Content;
        public Sprite Portrait;
        public bool RightSide;

    }
}
