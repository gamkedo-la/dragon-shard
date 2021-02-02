using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMagic : MonoBehaviour
{

    public int DamBuffLength;
    public float DamBuffStrength;

    public int DefuffLength;
    public float DefBuffStrength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuffAttack()
    {
        //INCLUDE SOME KIND OF ANIMATION CALL HERE

        GetComponent<Attack>().ModDamage(DamBuffStrength, DamBuffLength);

        foreach(GameObject t in GetComponent<Pathfinding>().CurrentLocation.GetComponent<Tile>().Adjacent)
        {

            if(t.GetComponent<Pathnode>().CurrentOccupant != null)
            {

                if(t.GetComponent<Pathnode>().CurrentOccupant.GetComponent<Unit>().Owner == GetComponent<Unit>().Owner)
                {
                    t.GetComponent<Pathnode>().CurrentOccupant.GetComponent<Attack>().ModDamage(DamBuffStrength, DamBuffLength);
                }
            }
        }
    }

    public void BuffDeffense()
    {
        //INCLUDE SOME KIND OF ANIMATION CALL HERE

        GetComponent<Attack>().ModDef(DefBuffStrength, DefuffLength);

        foreach (GameObject t in GetComponent<Pathfinding>().CurrentLocation.GetComponent<Tile>().Adjacent)
        {

            if (t.GetComponent<Pathnode>().CurrentOccupant != null)
            {

                if (t.GetComponent<Pathnode>().CurrentOccupant.GetComponent<Unit>().Owner == GetComponent<Unit>().Owner)
                {
                    t.GetComponent<Pathnode>().CurrentOccupant.GetComponent<Attack>().ModDef(DefBuffStrength, DefuffLength);
                }
            }
        }

    }

}
