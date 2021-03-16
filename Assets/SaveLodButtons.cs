using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLodButtons : MonoBehaviour
{
    public GameObject SaveLoadUI;

    public Button LoadButton;
    public InputField inputField;

    public Text SaveReminder;

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

    public void Save()
    {
        SaveReminder.gameObject.SetActive(true);

        Vector2 temp = inputField.GetComponent<RectTransform>().position;

        temp.y = 250;

        inputField.GetComponent<RectTransform>().position = temp;

        LoadButton.gameObject.SetActive(false);

    }

    public void LoadString()
    {
        SaveReminder.gameObject.SetActive(false);

        Vector2 temp = inputField.GetComponent<RectTransform>().position;

        temp.y = 265;

        inputField.GetComponent<RectTransform>().position = temp;

        inputField.text = "";
        LoadButton.gameObject.SetActive(true);
    }

}
