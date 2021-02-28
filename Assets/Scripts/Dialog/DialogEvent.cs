using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogEvent : MonoBehaviour
{
    public DialogLine[] ThisDialog;

    public DialogUI DialogBox;

    int Bookmark = 0;

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
            Camera.main.GetComponent<Clicker>().ActionInProgress = false;

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


    [System.Serializable]
    public struct DialogLine
    {
        public string SpeakerName;
        public string Content;
        public Sprite Portrait;
        public bool RightSide;

    }
}
