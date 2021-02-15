using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public bool Up;
    public bool Down;
    public bool Left;
    public bool Right;

    public float CameraSpeed;

    public Transform CameraRig;

    // Start is called before the first frame update
    void Start()
    {
        if(CameraRig == null)
        {
            Debug.Log("CameraRig not assigned. Look for the Screen Edges object on the UI canvas and assign it there.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Up == true || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            MoveUp(CameraSpeed);
        }
        if (Down == true || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            MoveDown(CameraSpeed);
        }
        if (Left == true || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft(CameraSpeed);
        }
        if (Right == true || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight(CameraSpeed);
        }
    }

    void MoveUp(float speed)
    {
        Vector3 temp = CameraRig.position;
        temp.z += (speed * Time.deltaTime);
        CameraRig.position = temp;
    }

    void MoveDown(float speed)
    {
        Vector3 temp = CameraRig.position;
        temp.z -= (speed * Time.deltaTime);
        CameraRig.position = temp;
    }

    void MoveLeft(float speed)
    {
        Vector3 temp = CameraRig.position;
        temp.x -= (speed * Time.deltaTime);
        CameraRig.position = temp;
    }

    void MoveRight(float speed)
    {
        Vector3 temp = CameraRig.position;
        temp.x += (speed * Time.deltaTime);
        CameraRig.position = temp;
    }

    public void SetUp(bool U)
    {
        Up = U;
    }

    public void SetDown(bool U)
    {
        Down = U;
    }

    public void SetLeft(bool U)
    {
        Left = U;
    }

    public void SetRight(bool U)
    {
        Right = U;
    }

}
