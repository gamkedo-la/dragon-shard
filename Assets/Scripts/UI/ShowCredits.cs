using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCredits : MonoBehaviour
{
    public CanvasGroup Credits;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        Credits.alpha = 1;
        Credits.interactable = true;
        Credits.blocksRaycasts = true;

    }

    public void Close()
    {
        Credits.alpha = 0;
        Credits.interactable = false;
        Credits.blocksRaycasts = false;

    }
}
