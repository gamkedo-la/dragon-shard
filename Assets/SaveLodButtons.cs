using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLodButtons : MonoBehaviour
{
    public GameObject SaveLoadUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenUI()
    {
        SaveLoadUI.GetComponent<CanvasGroup>().alpha = 1;
        SaveLoadUI.GetComponent<CanvasGroup>().interactable = true;
        SaveLoadUI.GetComponent<CanvasGroup>().blocksRaycasts = true;

    }

    public void CloseUI()
    {

        SaveLoadUI.GetComponent<CanvasGroup>().alpha = 0;
        SaveLoadUI.GetComponent<CanvasGroup>().interactable = false;
        SaveLoadUI.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

}
