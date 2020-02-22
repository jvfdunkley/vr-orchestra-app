using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=FGiCejM743g
public class QuaternionRotateExample : MonoBehaviour
{
    //It goes to (120,-90,-180)
    //D angle
    //130 (0,40,0)
    Quaternion targetAngle_90 = Quaternion.Euler(0,40,0);
    //their base is (90,0,-90) when you do (0,0,0)
    //this is what you need to do to get to (160,0,-90)
    //G angle 0,70,0
    Quaternion targetAngle_0 = Quaternion.Euler(0,70,0);
    //Quaternion targetAngle_a = Quaternion.Euler(0,20,0);
    //A is 110
    //E is 90 (0,0,0)
    public Quaternion currentAngle;
    // Start is called before the first frame update
    void Start()
    {
        currentAngle = targetAngle_0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ChangeCurrentAngle();
        }
        this.transform.parent.rotation = Quaternion.Slerp(this.transform.parent.rotation, currentAngle, 0.2f);
    }

    void ChangeCurrentAngle()
    {
        if(currentAngle.eulerAngles.z == targetAngle_0.eulerAngles.z)
        {
            currentAngle = targetAngle_90;
        }
        else
        {
            currentAngle = targetAngle_0;
        }
    }
}
