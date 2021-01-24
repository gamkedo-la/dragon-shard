using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ArmorColor : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        GetComponent<SpriteRenderer>().color = 
            GameObject.Find("Main Camera").GetComponent<Players>().
            ThisGame[transform.parent.gameObject.GetComponent<Unit>().Owner].thisColor;
#endif
    }
}
