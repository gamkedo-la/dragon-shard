using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelThreeFinale : MonoBehaviour
{

    public DialogEvent Both;

    public DialogEvent One;
    public DialogEvent Two;

    bool Triggered = false;

    public MidLevelSpawn Surprise;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Triggered == false)
        {
            if (One.finished == true && Two.finished == true)
            {

                Surprise.Spawn();
                Both.NextLine();
                Triggered = true;

            }
        }

    }
}
