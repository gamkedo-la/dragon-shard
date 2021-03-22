using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidLevelSpawn : MonoBehaviour
{

    public List<Pod> pods = new List<Pod>();

    public Players players;

    public int faction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        foreach(Pod P in pods)
        {
            bool s = false;
            GameObject U = Instantiate(P.Unit);

            if(P.SpawnPoint.GetComponent<Pathnode>().CurrentOccupant == null)
            {
                U.GetComponent<Pathfinding>().PlaceUnit(P.SpawnPoint);
                s = true;
            }
            else
            {
                GameObject f = null;

                List<GameObject> alt = new List<GameObject>();

                foreach(GameObject t in P.SpawnPoint.GetComponent<Tile>().Adjacent)
                {
                    if(t.GetComponent<Pathnode>().CurrentOccupant == null)
                    {
                        alt.Add(t);
                    }
                }

                if(alt.Count > 0)
                {
                    int n = Random.Range(0, alt.Count);
                    f = alt[n];

                    U.GetComponent<Pathfinding>().PlaceUnit(f);
                    s = true;
                }              

            }

            if(s == true)
            {
                U.GetComponent<Unit>().Owner = faction;
                U.GetComponent<Unit>().SetColor();

                U.GetComponent<Unit>().Alliance = players.ThisGame[faction].Alliance;

                players.ThisGame[faction].Units.Add(U);

                foreach(GameObject ai in players.AIPlayers)
                {
                    if(U.GetComponent<Unit>().Alliance != ai.GetComponent<Tracker>().Team)
                    {
                        ai.GetComponent<Tracker>().Enemies.Add(U);
                    }
                    else if(U.GetComponent<Unit>().Alliance == ai.GetComponent<Tracker>().P)
                    {
                        ai.GetComponent<Tracker>().MyUnits.Add(U);
                        U.GetComponent<Unit>().AIOverlord = ai;
                    }
                }
            }
        }

    }

    public struct Pod
    {
        public GameObject Unit;
        public GameObject SpawnPoint;
    }

}
