using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnitPlacer : MonoBehaviour
{

    public GameObject[] Units;

    public GameObject Active;

    public GameObject SelectedPlayer;

    public int ActivePlayer = 0;

    public AddingPlayers AP;

    public Players players;

    public GameObject SelectionRing;

    public GameObject Selected;

    public GameObject SelectedUI;

    public Text SelectedName;

    public Dropdown SelectedOwner;

    public bool placing;

    // Start is called before the first frame update
    void Start()
    {
        players = Camera.main.GetComponent<Players>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (Input.GetMouseButtonDown(0) && placing == true)
        {
            Ray toMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rhInfo;
            bool didHit = Physics.Raycast(toMouse, out rhInfo, 50.0f);

            if (didHit)
            {
                if (rhInfo.collider.gameObject.GetComponent<Pathnode>() != null)
                {
                    if (Active != null)
                    {
                        Place(rhInfo.collider.gameObject);
                    }
                }
                else
                {

                    Select(rhInfo.collider.gameObject);
                    Active = null;

                }
            }
        }
    }
    public void PickUp(int u)
    {
        Active = Units[u];
        Deselect();
    }

    public void Place(GameObject T)
    {
        GameObject G = Instantiate(Active);
        G.GetComponent<Pathfinding>().PlaceUnit(T);
        G.GetComponent<Unit>().Owner = ActivePlayer;
        players.ThisGame[ActivePlayer].Units.Add(G);
        Select(G);
        //Active = null;
    }

    public void SetActivePlayer(int A)
    {

        ActivePlayer = A;
    }

    public void Select(GameObject G)
    {
        SelectedUI.GetComponent<CanvasGroup>().alpha = 1;
        SelectedUI.GetComponent<CanvasGroup>().interactable = true;
        SelectedUI.GetComponent<CanvasGroup>().blocksRaycasts = true;

        Selected = G;
        SelectionRing.transform.position = G.transform.position;
        SelectedName.text = G.GetComponent<Unit>().UnitName;

        SelectedOwner.value = Selected.GetComponent<Unit>().Owner;

    }

    public void Deselect()
    {
        SelectedUI.GetComponent<CanvasGroup>().alpha = 0;
        SelectedUI.GetComponent<CanvasGroup>().interactable = false;
        SelectedUI.GetComponent<CanvasGroup>().blocksRaycasts = false;

        Selected = null;

        SelectionRing.transform.position = new Vector3(0, -.1f, .5f);
    }

    public void ChangeOwner(int P)
    {
        players.ThisGame[Selected.GetComponent<Unit>().Owner].Units.Remove(Selected);

        Selected.GetComponent<Unit>().Owner = P;

        players.ThisGame[P].Units.Add(Selected);

        Selected.GetComponent<Unit>().SetColor();

    }

    public void DeleteUnit()
    {

        players.ThisGame[Selected.GetComponent<Unit>().Owner].Units.Remove(Selected);
        Selected.GetComponent<Pathfinding>().CurrentLocation.GetComponent<Pathnode>().CurrentOccupant = null;

        Destroy(Selected);
        Deselect();
    }

    public void StartPlacing()
    {

        placing = true;
    }

    public void StopPlacing()
    {
        placing = false;
    }

}
