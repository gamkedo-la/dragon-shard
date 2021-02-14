using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{

    public Transform Close;
    public Transform Far;

    public float position = 0.5f;

    public float ScrollSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.mouseScrollDelta.y > 0)
        {
            ChangePosition(true);
        }
        if(Input.mouseScrollDelta.y < 0)
        {
            ChangePosition(false);
        }


    }

    void ChangePosition(bool closer)
    {
        if(closer == true)
        {
            if (position < 1)
            {
                position += ScrollSpeed;
            }
        }
        else
        {
            if (position > 0)
            {
                position -= ScrollSpeed;
            }
        }

        transform.position = Vector3.Lerp(Far.position, Close.position, position);


    }



}
