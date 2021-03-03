using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPLevelDialogTrigger : MonoBehaviour
{
    public DialogEvent thisDialog;
    public int HPTriggerLevel;

    public bool DialogStarted;

    HitPoints hit;
    // Start is called before the first frame update
    void Start()
    {
        hit = GetComponent<HitPoints>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
