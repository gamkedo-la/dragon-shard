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

    // Start is called before the first frame update
    void Start()
    {
        
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
        Vector3 temp = transform.position;
        temp.z += (speed * Time.deltaTime);
        transform.position = temp;
    }

    void MoveDown(float speed)
    {
        Vector3 temp = transform.position;
        temp.z -= (speed * Time.deltaTime);
        transform.position = temp;
    }

    void MoveLeft(float speed)
    {
        Vector3 temp = transform.position;
        temp.x -= (speed * Time.deltaTime);
        transform.position = temp;
    }

    void MoveRight(float speed)
    {
        Vector3 temp = transform.position;
        temp.x += (speed * Time.deltaTime);
        transform.position = temp;
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
