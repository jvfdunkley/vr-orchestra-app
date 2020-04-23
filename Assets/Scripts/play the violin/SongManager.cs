using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    public GameObject noteSprite; 
    //the current position of the song (in seconds)
    public float songPosition;

    //the current position of the song (in beats)
    public float songPosInBeats;

    //the duration of a beat
    public float secPerBeat;

    //how much time (in seconds) has passed since the song started
    public float dsptimesong;
    //beats per minute of a song
    public float bpm;

    //keep all the position-in-beats of notes in the song
    public float[] notes;

    //the index of the next note to be spawned
    public int nextIndex = 0;

    public float beatsShownInAdvance = 4;

    // Start is called before the first frame update
    void Start()
    {
        //calculate how many seconds is one beat
        //we will see the declaration of bpm later
        secPerBeat = 60f / bpm;
        
        //record the time when the song starts
        dsptimesong = (float) AudioSettings.dspTime;

        //start the song
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        //calculate the position in seconds
        songPosition = (float) (AudioSettings.dspTime - dsptimesong);

        //calculate the position in beats
        songPosInBeats = songPosition / secPerBeat;
        if (nextIndex < notes.Length && notes[nextIndex] < songPosInBeats + beatsShownInAdvance)
        {
            Instantiate(noteSprite);

            //initialize the fields of the music note

            nextIndex++;
        }
        //transform.position = Vector2.Lerp(SpawnPos,RemovePos,(BeatsShownInAdvance - (beatOfThisNote - songPosInBeats)) / BeatsShownInAdvance);    
    }
}
