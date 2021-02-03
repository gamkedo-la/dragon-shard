using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    public int P;
    public Players GM;

    public List<GameObject> Enemies = new List<GameObject>();
    public List<GameObject> MyUnits = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i<GM.ThisGame.Length; i++)
        {
            if(P != i)
            {

                foreach(GameObject U in GM.ThisGame[i].Units)
                {

                    Enemies.Add(U);

                }
            }
            else
            {

                foreach (GameObject U in GM.ThisGame[i].Units)
                {

                    MyUnits.Add(U);

                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnStart()
    {




    }

}
