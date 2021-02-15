using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    bool Up;
    bool Down;
    bool Left;
    bool Right;

    public float CameraSpeed;

    public Transform CameraRig;

    public Grid grid;

    public float UpperBound;
    public float LowerBound;
    public float LeftBound;
    public float RightBound;


    // Start is called before the first frame update
    void Start()
    {
        if(grid == null)
        {
            Debug.Log("grid not assigned to Camera Mover. Look for the Screen Edges object on the UI canvas and assign it there.");
        }
        if(CameraRig == null)
        {
            Debug.Log("CameraRig not assigned Camera Mover. Look for the Screen Edges object on the UI canvas and assign it there.");
        }
        SetBounds();
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
        if(temp.z > UpperBound)
        {
            temp.z = UpperBound;
        }
        CameraRig.position = temp;
    }

    void MoveDown(float speed)
    {
        Vector3 temp = CameraRig.position;
        temp.z -= (speed * Time.deltaTime);
        if (temp.z < LowerBound)
        {
            temp.z = LowerBound;
        }
        CameraRig.position = temp;
    }

    void MoveLeft(float speed)
    {
        Vector3 temp = CameraRig.position;
        temp.x -= (speed * Time.deltaTime);
        if (temp.x < LeftBound)
        {
            temp.x = LeftBound;
        }
        CameraRig.position = temp;
    }

    void MoveRight(float speed)
    {
        Vector3 temp = CameraRig.position;
        temp.x += (speed * Time.deltaTime);
        if (temp.x > RightBound)
        {
            temp.x = RightBound;
        }
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

    void SetBounds()
    {
        LowerBound = -3.5f;
        LeftBound = 4.0f;
        UpperBound = grid.Rows - 10;
        RightBound = grid.Columns - (5.5f * Mathf.Sqrt(3));
    }

}
