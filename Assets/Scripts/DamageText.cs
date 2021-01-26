using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public float speed = .5f;
    public int dam;
    Vector3 temp;
    public GameObject Camera;

    public Color TColor;

    public int FSize;

    public TextMesh T;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(Camera.transform);
        if (dam == 0)
        {
            T.text = "MISS";
        }
        else
        {
            T.text = dam.ToString();
        }
        T.color = TColor;
        T.fontSize = FSize;

        Destroy(gameObject, 0.5f);

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.transform);
        temp = transform.position;
        temp.y += speed * Time.deltaTime;
        transform.position = temp;
    }
}
