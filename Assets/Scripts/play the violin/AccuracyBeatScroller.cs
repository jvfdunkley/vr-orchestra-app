using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyBeatScroller : MonoBehaviour
{
    public float beatTempo;
    public bool hasStarted;
    // Start is called before the first frame update
    /*
    tempo should be 185 
    The accuracy note holder should be at x = 2
    Notes should be 3.12 apart and then half notes should be 6 away from the next quarter note 

    */
    void Start()
    {
        beatTempo = beatTempo/ 60f;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(!hasStarted)
        {
            if(Input.anyKeyDown && instructionPanel.activeSelf == false)
            {
                hasStarted = true;
            }
        }*/
        if (hasStarted)
        {
            transform.position -= new Vector3(beatTempo * Time.deltaTime,0f, 0f);
        }
    }
    public void reset()
    {
        transform.position = new Vector3(2f, -1.57f, 0f);


        AccuracyNoteObject[] childScripts = gameObject.GetComponentsInChildren<AccuracyNoteObject>(true);
        for (int i = 0; i < childScripts.Length; i++)
        {
            AccuracyNoteObject myChildScript = childScripts[i];
            Debug.Log(myChildScript.gameObject.name);
            childScripts[i].gameObject.SetActive(true);
        }
        hasStarted = false;
    }
}
