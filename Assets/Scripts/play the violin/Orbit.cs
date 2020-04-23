using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform target;
    public float radius = 4.0f;
    public float theta = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            theta += 0.1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            theta -= 0.1f;
        }

        float x = radius * Mathf.Sin(theta);
        float z = radius * Mathf.Cos(theta);
        transform.position = new Vector3(x,0,z);
        transform.rotation = Quaternion.LookRotation(-transform.position);
    }
}
