using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//https://www.gamasutra.com/blogs/GrahamTattersall/20190515/342454/Coding_to_the_Beat__Under_the_Hood_of_a_Rhythm_Game_in_Unity.php
public class TrackSongPosition : MonoBehaviour
{
    //Song beats per minute
    //The offset to the first beat of the song in seconds
    public float firstBeatOffset;
    //This is determined by the song you're trying to sync up to
    public float songBpm;
    //The number of seconds for each song beat
    public float secPerBeat;
    //Current song position, in seconds
    public float songPosition;
    //Current song position, in beats
    public float songPositionInBeats;
    //How many seconds have passed since the song started
    public float dspSongTime;
    //The current relative position of the song within the loop measured between 0 and 1.
    public float loopPositionInAnalog;
    //the number of beats in each loop
    public float beatsPerLoop;
    //the total number of loops completed since the looping clip first started
    public int completedLoops = 0;

    //The current position of the song within the loop in beats.
    public float loopPositionInBeats;

    //Conductor instance
    public static TrackSongPosition instance;


    void Awake()
    {
    instance = this;
    }
    void Start()
    {
        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;
    }

    // Update is called once per frame
    void Update()
    {
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);

        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;

            //calculate the loop position
        if (songPositionInBeats >= (completedLoops + 1) * beatsPerLoop)
        {
            completedLoops++;
        }
        //else Debug.Log("beatsPerLoop" + beatsPerLoop);
        //Debug.Log("beatsPerLoop " + beatsPerLoop);
        //This whole thing makes sure that the timing is in line with the song when the animation loops because there is
        //some offset between animation loops I think so this normalizes it 

        //these things are messed up 
        //I *DONT* WANT COMPLETED LOOPS TO BE FASTER THAN SONG BEATS
        //so maybe this isn't the best way to do looping, or maybe make animation 0.25 seconds only
            loopPositionInBeats = songPositionInBeats - completedLoops * beatsPerLoop;

            loopPositionInAnalog = loopPositionInBeats / beatsPerLoop;
    
    
    }
}
