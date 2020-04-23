using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public BeatScroller theBS;

    public GameObject button;

    public GameObject camera;

    //only one variable allowed
    //can be used in multiple different scripts but it will get the most recently updated instance to use 
    public static GameManager instance;

    public GameObject endPanel;

    private List<GameObject> noteList = new List<GameObject>(); 

    public GameObject noteHolder;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Transform[] allChildren = noteHolder.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        { 
            noteList.Add(child.gameObject);
        }
        Debug.Log("noteList " + noteList.Count); //44
    }
    
    public void Move(GameObject gameObject)
    {
        var index = noteList.FindIndex(item => item == gameObject);
        Vector3 new_position = new Vector3(noteList[index+1].transform.position.x, button.transform.position.y,button.transform.position.z);
        button.transform.position = Vector3.Slerp(button.transform.position, new_position, 1f);
        if(noteList[index+1].name == "End Note")
        {
            //Debug.Log("it's the end!!");
            StartCoroutine(waitABit());
            //open up the ending panel 
        }

    }

    //make a one second coroutine thing for the endPanel 
    IEnumerator waitABit()
    {
        yield return new WaitForSeconds(1f);
        endPanel.SetActive(true);

    }
    public void MoveCamera()
    {
        Debug.Log("hello!!");
        float previousCam = camera.transform.position.x + 3f;
        Debug.Log(previousCam);
        camera.transform.position = new Vector3(previousCam, 0.0f, 0.0f);
    }

    public void MoveCameraBackToRedoGame()
    {
        camera.transform.position = new Vector3(0f,0f,0f);
        Debug.Log("hello it's here!");
        button.transform.position = Vector3.Slerp(button.transform.position, new Vector3(-4.5f, button.transform.position.y, button.transform.position.z), 1f);
    }

    public void NoteHit()
    {
        Debug.Log("Hit On Time");
    }

    public void NoteMissed()
    {
        Debug.Log("Missed Note");
    }
}
