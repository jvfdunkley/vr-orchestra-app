using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;

    public KeyCode keyToPress;
    public AudioClip clip_to_play; 
    // Start is called before the first frame update
    public AudioSource audioSource;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(keyToPress))
        {
            if(canBePressed)
            {
                audioSource.clip = clip_to_play;
                //this helped when I wanted to play the note then disable the object
                //https://gamedev.stackexchange.com/questions/150202/unity3d-audiosource-is-not-playing-if-gameobject-is-removed-after-play-was-sta
                audioSource.Play();
                GameManager.instance.Move(gameObject);
                GameManager.instance.MoveCamera();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            canBePressed = false;

            //GameManager.instance.NoteMissed();
        }
    }
}
