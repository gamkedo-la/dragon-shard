using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlacer : MonoBehaviour
{

    public GameObject[] Units;

    public GameObject Active;

    public GameObject SelectedPlayer;

    int ActivePlayer = 0;

    public AddingPlayers AP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Active != null) {
            if (Input.GetMouseButtonDown(0))
            {

                Ray toMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit rhInfo;
                bool didHit = Physics.Raycast(toMouse, out rhInfo, 50.0f);
                if (didHit)
                {
                    if (rhInfo.collider.gameObject.GetComponent<Pathnode>() != null)
                    {
                        Place(rhInfo.collider.gameObject);
                    }

                }

            }
        }
    }
    public void PickUp(int u)
    {
        Active = Units[u];

    }

    public void Place(GameObject T)
    {
        GameObject G = Instantiate(Active);
        G.GetComponent<Pathfinding>().PlaceUnit(T);
        G.GetComponent<Unit>().Owner = ActivePlayer;
        Active = null;
    }

    public void SetActivePlayer(int A)
    {

        ActivePlayer = A;
    }
}
