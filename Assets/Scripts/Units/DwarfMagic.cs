using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfMagic : MonoBehaviour
{

    public GameObject Origin;

    public List<GameObject> AoE = new List<GameObject>();

    public int FireWaveDamage;
    public int FireBallDamage;


    float fireballAnimLength = 0;

    float timer = 0;

    GameObject FTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                if (FTarget != null)
                {
                    GetComponent<Attack>().Att(FTarget.GetComponent<HitPoints>(), FireBallDamage, 0);
                    GetComponent<Unit>().Click.Clear();
                    FTarget = null;
                }
            }
        }
    }

    public void TargetFireWave(GameObject G)
    {

        if (G != Origin)
        {

            AoE.Clear();
            //Debug.Log("targeting firewave");

            if (GetComponent<Pathfinding>().CurrentLocation.GetComponent<Tile>().Adjacent.Contains(G) == true)
            {
                Origin = G;
                AoE.Add(G);
                foreach (GameObject A in G.GetComponent<Tile>().Adjacent)
                {
                    if (GetComponent<Pathfinding>().CurrentLocation.GetComponent<Tile>().Adjacent.Contains(A) == false)
                    {

                        AoE.Add(A);
                    }

                }
            }
            else
            {
                return;
            }
            AoE.Remove(GetComponent<Pathfinding>().CurrentLocation);
        }
        foreach (Transform T in GetComponent<Pathfinding>().Grid)
        {
            T.GetComponent<Pathnode>().Fade();
            
        }
        foreach(GameObject A in AoE)
        {
            A.GetComponent<Pathnode>().Unfade();
        }

        GetComponent<Pathfinding>().CurrentLocation.GetComponent<Pathnode>().Unfade();    

    }
    
    public void CastFireWave(GameObject G)
    {

        if(G == Origin)
        {
            GetComponent<Unit>().ActedThisTurn = true;
            foreach (GameObject A in AoE)
            {
                if (A.GetComponent<Pathnode>().CurrentOccupant != null)
                {
                    GetComponent<Attack>().Att(A.GetComponent<Pathnode>().CurrentOccupant.GetComponent<HitPoints>(), FireWaveDamage, 0);
                }
            }
        }
        GetComponent<Unit>().Click.Clear();
        return;
    }


    public void CastFireball(GameObject G)
    {
        GetComponent<Unit>().ActedThisTurn = true;
        FTarget = G;   
        timer = fireballAnimLength;
    }  

}
