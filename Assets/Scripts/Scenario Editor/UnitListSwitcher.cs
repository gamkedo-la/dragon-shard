using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitListSwitcher : MonoBehaviour
{

    public GameObject[] UnitLists;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Switcher(int A)
    {
        foreach(GameObject G in UnitLists)
        {
            G.SetActive(false);
        }
        UnitLists[A].SetActive(true);

    }
}
