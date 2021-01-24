using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIColors : MonoBehaviour
{
    SpriteRenderer SP;
    Button B;
    Players GM;
    UnityEngine.UI.ColorBlock Butt;
    Color temp;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("Main Camera").GetComponent<Players>();

        if (GetComponent<Button>() != null)
        {
            B = GetComponent<Button>();


        }
        if(GetComponent<SpriteRenderer>() != null)
        {
            SP = GetComponent<SpriteRenderer>();

        }

        SetColors();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetColors()
    {
        if(SP != null)
        {
            SP.color = GM.ThisGame[GM.CurrentTurn].thisColor;

        }
        if (B != null)
        {
            Butt.normalColor = GM.ThisGame[GM.CurrentTurn].thisColor;
            temp = GM.ThisGame[GM.CurrentTurn].thisColor;
            temp.b *= .9f;
            temp.g *= .9f;
            temp.r *= .9f;
            Butt.highlightedColor = temp;
            temp = GM.ThisGame[GM.CurrentTurn].thisColor;
            temp.b *= .8f;
            temp.g *= .8f;
            temp.r *= .8f;
            Butt.pressedColor = temp;
            Butt.selectedColor = GM.ThisGame[GM.CurrentTurn].thisColor;
            Butt.colorMultiplier = 1;
            B.colors = Butt;
        }


    }

}
