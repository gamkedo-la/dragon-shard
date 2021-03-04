using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnitInfo : MonoBehaviour
{
    public Image Color;

    public Text Name;
    public Text HP;
    public Text CurrentDef;
    public Text melee;
    public Text ranged;

    public GameObject SelectedUnit;

    public GameObject SelectionRing;

    public CanvasGroup DefenseInfo;
    public CanvasGroup BasicInfo;

    public Text D;

    public Text Grass;
    public Text Forest;
    public Text Water;
    public Text castle;
    public Text Sand;
    public Text Hills;

    // Start is called before the first frame update
    void Start()
    {
        DeselectUnit();
    }

    // Update is called once per frame
    void Update()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {

            Ray toMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rhInfo;
            bool didHit = Physics.Raycast(toMouse, out rhInfo, 50.0f);
            if (didHit)
            {
                if(rhInfo.collider.gameObject.tag == "Unit")
                {
                    SelectUnit(rhInfo.collider.gameObject);
                }

            }
        }            
        
    }

    public void SelectUnit(GameObject U)
    {

        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().interactable = true;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        SelectedUnit = U;

        Color.color = Camera.main.GetComponent<Players>().ThisGame[U.GetComponent<Unit>().Owner].thisColor;

        Name.text = U.GetComponent<Unit>().UnitName;
        HP.text = "HP: " + U.GetComponent<HitPoints>().CurrentHP + "/" + U.GetComponent<HitPoints>().MaxHP;
        CurrentDef.text = "Current Defense: " + U.GetComponent<Attack>().CurrentDef + "%";

        if(U.GetComponent<Attack>().MeleeAttacks > 0)
        {
            melee.text = "Melee: " + U.GetComponent<Attack>().MeleeName + ": " + U.GetComponent<Attack>().MeleeDamage + " Damage, " + U.GetComponent<Attack>().MeleeAttacks + " Attacks";

            if(U.GetComponent<Attack>().RangedAttacks > 0)
            {
                ranged.text = "Ranged: " + U.GetComponent<Attack>().RangedName + ": " + U.GetComponent<Attack>().RangedDamage + " Damage, " + U.GetComponent<Attack>().RangedAttacks + " Attacks";

            }
            else
            {
                ranged.text = " ";
            }
        }
        else
        {
            melee.text = "Ranged: " + U.GetComponent<Attack>().RangedName + ": " + U.GetComponent<Attack>().RangedDamage + " Damage, " + U.GetComponent<Attack>().RangedAttacks + " Attacks";

            ranged.text = " ";
        }

        D.text = U.GetComponent<Unit>().UnitName + " Defense Info";

        Grass.text = "Grass: " + U.GetComponent<Attack>().GrassDef + "%";
        Forest.text = "Forest: " + U.GetComponent<Attack>().ForestDef + "%";
        Water.text = "Water: " + U.GetComponent<Attack>().WaterDef + "%";
        castle.text = "Castle: " + U.GetComponent<Attack>().CastleDef + "%";
        Hills.text = "Hills: " + U.GetComponent<Attack>().HillsDef + "%";
        Sand.text = "Sand: " + U.GetComponent<Attack>().SandDef + "%";

        SelectionRing.transform.position = SelectedUnit.GetComponent<Pathfinding>().CurrentLocation.transform.position;
        SelectionRing.transform.parent = SelectedUnit.transform;

        ToggleDefenseInfo(false);

    }

    public void DeselectUnit()
    {
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().interactable = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;

        SelectedUnit = null;

        SelectionRing.transform.parent = null;
        SelectionRing.transform.position = new Vector3(0, -.1f, .5f);

    }

    public void ToggleDefenseInfo(bool on)
    {
        if(on == true)
        {
            DefenseInfo.alpha = 1;
            DefenseInfo.blocksRaycasts = true;
            DefenseInfo.interactable = true;

            BasicInfo.alpha = 0;
            BasicInfo.interactable = false;
            BasicInfo.blocksRaycasts = false;

        }

        if(on == false)
        {
            DefenseInfo.alpha = 0;
            DefenseInfo.blocksRaycasts = false;
            DefenseInfo.interactable = false;

            BasicInfo.alpha = 1;
            BasicInfo.interactable = true;
            BasicInfo.blocksRaycasts = true;

        }

    }
}
