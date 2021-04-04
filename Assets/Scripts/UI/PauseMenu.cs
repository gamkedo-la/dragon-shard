using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public CanvasGroup PM;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Open();
        }

    }
    
    public void Open()
    {
        PM.alpha = 1;
        PM.blocksRaycasts = true;
        PM.interactable = true;

    }

    public void Close()
    {
        PM.alpha = 0;
        PM.blocksRaycasts = false;
        PM.interactable = false;


    }



}
