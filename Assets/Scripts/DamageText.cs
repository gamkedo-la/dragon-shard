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

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMesh>().text = dam.ToString();
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
