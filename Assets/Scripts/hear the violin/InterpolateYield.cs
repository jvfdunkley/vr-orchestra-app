using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterpolateYield : MonoBehaviour
{
    public Vector3 point1 = new Vector3(-10,0,0);
    public Vector3 point2 = new Vector3(10,10,0);
    public float duration = 10; //duration
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayAnimation(point1, point2));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlayAnimation(Vector3 p1, Vector3 p2)
    {
        for(float time = 0; time < duration; time += Time.deltaTime)
        {
            float u = time/duration;
            transform.position = Vector3.Lerp(p1, p2, u);
            yield return null;
        }
        transform.position = p2;
    }
}
