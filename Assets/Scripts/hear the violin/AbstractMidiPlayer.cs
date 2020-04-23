using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using NAudio.Midi;
public class AbstractMidiPlayer : MonoBehaviour
{
    //now I'm making it abstract to any midi song 
    //The vivaldi concerto has 8 midi Events 

    //public objects
    public MidiFile midiFile;
    public TrackSongPosition songPosition;
    public float bpm;

    //private objects
    private Animator bowAnimator;
    private float deltaTicks;
    private List<float> noteTime;
    private List<float> noteTimeNext;
    private SortedDictionary<string, string> noteStringTag = new SortedDictionary<string, string>();
    private SortedDictionary<string, Quaternion> stringAngleTag = new SortedDictionary<string, Quaternion>();
    //their base is (90,0,-90) when you do (0,0,0)
    //this is what you need to do to get to (160,0,-90)
    //G angle 0,70,0
    
    void Start()
    {
        midiFile = new MidiFile("Assets/vivconct.mid", false);
        
        deltaTicks = midiFile.DeltaTicksPerQuarterNote;

        noteTime = new List<float>();
    
        noteTimeNext = new List<float>();

        //since I attached the script to the bow, I can just access it's Animator directly 
        bowAnimator = gameObject.GetComponent<Animator>();

        noteStringTag.Add("G3", "G");
        noteStringTag.Add("A3", "G");
        noteStringTag.Add("B3", "G");
        noteStringTag.Add("C4", "G");
        noteStringTag.Add("D4", "D");
        noteStringTag.Add("E4", "D");
        noteStringTag.Add("F4", "D");
        noteStringTag.Add("G4", "D");
        noteStringTag.Add("A4", "A");
        noteStringTag.Add("B4", "A");
        noteStringTag.Add("C5", "A");
        noteStringTag.Add("D5", "A");
        noteStringTag.Add("E5", "E");
        noteStringTag.Add("F5", "E");
        noteStringTag.Add("G5", "E");
        noteStringTag.Add("A5", "E");
        noteStringTag.Add("B5", "E");
        noteStringTag.Add("C6", "E");


        //My issue is that I am trying to put normal Vector3 XYZ coordinates into a Quarternion when they don't do the same things 
        //TRANFORM.ROTATION IS W X Y Z NOT X Y Z W 
        //also when I put in rotation 130, 0, -90 the transform.rotation was 0.0, 0.3, 0.0, 0.9)

        
        Quaternion g = Quaternion.Euler(0,70,0);
        //Debug.Log("G" + g);
        //G to D
        Quaternion d = Quaternion.Euler(0,-70,0);
        //Debug.Log("D" + d);
        //D to A
        Quaternion a = Quaternion.Euler(0,-50,0); 
        //Debug.Log("A" + a);   
        //A to E 
        //90
        Quaternion e = Quaternion.Euler(0,0,0);
        //Debug.Log("E" + e);
        //110 to 130 127 201
        Quaternion ad = Quaternion.Euler(0,203,0);
        //currentAngle = targetAngle_0;
        //140 need to be at 160
        Quaternion dg = Quaternion.Euler(0,212,0);
        
        stringAngleTag.Add("G", g);
        stringAngleTag.Add("D", d);
        stringAngleTag.Add("A", a);
        stringAngleTag.Add("E", e);
        stringAngleTag.Add("AD", ad);
        stringAngleTag.Add("DG", dg);
        Debug.Log("midiFile.Events length " + midiFile.Events.Count());
        //check if First Beat Offset != 0 
        //new WaitForSeconds(3f);
        StartCoroutine(StartPlayback());

    }
    //solution for audio latency https://forum.unity.com/threads/audio-has-too-much-latency.499524/
    IEnumerator StartPlayback()
	{
        //check if First Beat Offset != 0 
        Debug.Log("hello!");
        yield return new WaitForSeconds(3f);
        for(int j = 0; j < midiFile.Events.Count(); j++)
        { 
            //Debug.Log("Midi Event #" + j);
           for(int i = 0; i < midiFile.Events[j].Count()-2; i++)
            //foreach (MidiEvent note in midiFile.Events[0])
            {
                MidiEvent note = midiFile.Events[j][i];
                MidiEvent nextNote = midiFile.Events[j][i+2];
                if (note.CommandCode == MidiCommandCode.NoteOn && !note.ToString().Contains("(Note Off)"))
                {
                    //Debug.Log("note" + note);
                    //Debug.Log("i " + i);
                    NoteOnEvent noe = (NoteOnEvent)note;
                   /* if(i == 5) //change this later, be able to tell what the first note is 
                    {
                        Debug.Log("hello1");
                        StartingBowPosition(noe);
                    }*/
                    NoteOnEvent nextNoe = (NoteOnEvent)nextNote;
                    float convertedTimeCurrentNote = 0.5f;
                    float convertedTimeNextNote = 0.5f;
                    float timeCurrentNote = calculateTime(noe);
                    float timeNextNote = calculateTime(nextNoe);
                    //Debug.Log("time: " + time);

                    //convertedTimeCurrentNote
                    noteTime.Add(timeCurrentNote);
                    int indexOfTime = noteTime.FindIndex(element => element == timeCurrentNote);
                    if(indexOfTime != 0)
                    {
                        convertedTimeCurrentNote = noteTime[indexOfTime] - noteTime[indexOfTime-1];
                    }

                    noteTimeNext.Add(timeNextNote);
                    int indexOfTimeNext = noteTimeNext.FindIndex(element => element == timeNextNote);
                    if(indexOfTimeNext != 0)
                    {
                        convertedTimeNextNote = noteTimeNext[indexOfTimeNext] - noteTimeNext[indexOfTimeNext-1];
                    }

                    //Debug.Log("converted time current: " + convertedTimeCurrentNote);
                    //Debug.Log("converted time next: " + convertedTimeNextNote);
                    
                    float roundedTime = (float)(System.Math.Truncate((double)convertedTimeCurrentNote * 100.0)/100.0);
                    float roundedTimeNextNote = (float)(System.Math.Truncate((double)convertedTimeNextNote * 100.0)/100.0);
                   // Debug.Log("roundedTime current note" + roundedTime);
                    //Debug.Log("roundedTime next note" + roundedTimeNextNote);

                    //8th note  roundedTime >= 0.50f && roundedTime <= 0.48f || && roundedTimeNextNote >= 0.48f && roundedTimeNextNote <= 0.50f
                    if(songPosition.beatsPerLoop != 1f && roundedTime >= 0.48f && roundedTime <= 0.50f)
                    {
                        songPosition.beatsPerLoop = 1;
                        //bowAnimator.SetFloat("speed", 1.0f);
                    }
                    //16th note songPosition.beatsPerLoop != 0.5f && 
                    else if(songPosition.beatsPerLoop != 0.5f && roundedTimeNextNote >= 0.22f && roundedTimeNextNote <= 0.26f)
                    {
                        Debug.Log("hello!");
                        songPosition.beatsPerLoop = 0.5f;
                        //bowAnimator.SetFloat("speed", 0.5f);
                    }
                    /*
                    //if you have an eighth note and the next note is a 16th note 
                    else if(roundedTime == 0.50f && roundedTimeNextNote >= 0.22f && roundedTimeNextNote <= 0.26f)
                    {
                        Debug.Log("hello!!");
                        songPosition.beatsPerLoop = 0.5f;
                    }*/
                    //songPosition.beatsPerLoop = convertedTime * 4;
                    int noteNumber = noe.NoteNumber;
                    string realNote = convertToNoteName(noteNumber);
                    //Debug.Log("Playing real note: " + realNote + "converted Time current note " + convertedTimeCurrentNote + "roundedTime" + roundedTime); 
                    Debug.Log("Playing real note: " + realNote);
                    string realNextNote = convertToNoteName(nextNoe.NoteNumber);
                    //Debug.Log("Playing real next note: " + realNextNote + "converted Time next note " + convertedTimeNextNote + "roundedTime next note" + roundedTimeNextNote);
                    //if converted time rounds to 0.25 double then change BeatsPerLoop to 0.5
                
                    //if you have an eighth note and the next note is a 16th note 
                    //if (roundedTime != 0.50f || roundedTime != 0.49f)
                    //{
                    //yield return new WaitForSeconds(roundedTime); //or convertedTime 0.9f
                    //}
                    //if you have an eighth note and the next note is a 16th note 
                   /* if (roundedTime == 0.50f && roundedTimeNextNote >= 0.22f && roundedTimeNextNote <= 0.26f)
                    {
                        yield return new WaitForSeconds(roundedTimeNextNote);
                    }*/
                    yield return new WaitForSeconds(roundedTimeNextNote);
                    
                    //IsOnCorrectString(realNote, realNextNote);

                }
                    //else yield return new WaitForSeconds(0.9f);
            }
        }
        //yield return new WaitForSeconds(0.9f);
    }
    private float calculateTime(NoteOnEvent noe)
    {
        float time = (60 * noe.AbsoluteTime) / (bpm * deltaTicks);
        return time;
    }



    private void StartingBowPosition(NoteOnEvent noe)
    {
        int noteNumber = noe.NoteNumber;
        string realNote = convertToNoteName(noteNumber);
        string noteString = noteStringTag[realNote];
        this.transform.parent.rotation = stringAngleTag[noteString];
        
    }


    private string convertToNoteName(int noteNumber)
    {
        string[] noteString = new string[] { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
        int octave = (noteNumber / 12) - 1;
        int noteIndex = (noteNumber % 12);
        string note1 = noteString[noteIndex];
        return note1 + octave;
    }
    private void IsOnCorrectString(string currentNote, string nextNote)
    {
        //use SortedDictionary instead of Dictionary from now on, idk what the fuck is Dictionary's problem
        string currentNoteString = noteStringTag[currentNote];
        string nextNoteString = noteStringTag[nextNote];
        if(currentNoteString != nextNoteString)
        {
            if(currentNoteString == "A" && nextNoteString == "D") 
            {
                Debug.Log("AD");
                Debug.Log("currentNote" + currentNote);
                Debug.Log("nextNote " + nextNote);
                PlayAnimation(stringAngleTag["AD"]);
            }
            else if(currentNoteString == "D" && nextNoteString == "G")
            {
                Debug.Log("DG");
                PlayAnimation(stringAngleTag["DG"]);
            }
            PlayAnimation(stringAngleTag[nextNoteString]);
        }
    }

    private void PlayAnimation(Quaternion p2)
    {
        //Debug.Log("quaternion p2 euler " + p2.eulerAngles);
        this.transform.parent.rotation = Quaternion.Slerp(this.transform.parent.rotation, p2,  0.2f);
    }

}
