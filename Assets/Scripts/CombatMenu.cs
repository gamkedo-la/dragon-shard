using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatMenu : MonoBehaviour
{
    public Text AggName;
    public Text AggDef;
    public Text DefName;
    public Text DefDef;

    public Image AggColor;
    public Image DefColor;

    public GameObject Aggressor;
    public GameObject Defender;

    public Clicker click;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Populate()
    {

        AggName.text = Aggressor.GetComponent<Unit>().UnitName;
        AggDef.text = "Defense: " + Aggressor.GetComponent<Attack>().CurrentDef.ToString() + "%";

        DefName.text = Defender.GetComponent<Unit>().UnitName;
        DefDef.text = "Defense: " + Defender.GetComponent<Attack>().CurrentDef.ToString() + "%";

        AggColor.color = Aggressor.GetComponent<Unit>().GM.ThisGame[Aggressor.GetComponent<Unit>().Owner].thisColor;

        DefColor.color = Defender.GetComponent<Unit>().GM.ThisGame[Defender.GetComponent<Unit>().Owner].thisColor;

    }


    public void Cancel()
    {

        click.Clear();
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().interactable = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;

    }
}
