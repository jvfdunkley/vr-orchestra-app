using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using NAudio.Midi;
public class MidiPlayer : MonoBehaviour
{

    //public objects
    public MidiFile midiFile;
    public AudioSource song;

    //private objects
    private Animator pan_animation;
    private float deltaTicks;
    private List<float> noteTime;
    private SortedDictionary<string, string> noteStringTag = new SortedDictionary<string, string>();
    private SortedDictionary<string, Quaternion> stringAngleTag = new SortedDictionary<string, Quaternion>();
    //their base is (90,0,-90) when you do (0,0,0)
    //this is what you need to do to get to (160,0,-90)
    //G angle 0,70,0
    
    void Start()
    {
        midiFile = new MidiFile("Assets/CmajorScaleOneOctave.mid", false);
        
        deltaTicks = midiFile.DeltaTicksPerQuarterNote;

        noteTime = new List<float>();

        //since I attached the script to the bow, I can just access it's Animator directly 
        pan_animation = gameObject.GetComponent<Animator>();

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
        StartCoroutine(StartPlayback());

    }
    //solution for audio latency https://forum.unity.com/threads/audio-has-too-much-latency.499524/
    IEnumerator StartPlayback()
	{
            for(int i = 0; i < midiFile.Events[0].Count()-2; i++)
            //foreach (MidiEvent note in midiFile.Events[0])
            {
                MidiEvent note = midiFile.Events[0][i];
                Debug.Log("note" + note);
                MidiEvent nextNote = midiFile.Events[0][i+2];
                //note velocity 
                if (note.CommandCode == MidiCommandCode.NoteOn && !note.ToString().Contains("(Note Off)"))
                {
                    if(!nextNote.ToString().Contains("EndTrack"))
                    {
                        NoteOnEvent noe = (NoteOnEvent)note;
                        NoteOnEvent nextNoe = (NoteOnEvent)nextNote;
                        float convertedTime = 1;
                        float bpm = 120;
                        float time = (60 * noe.AbsoluteTime) / (bpm * deltaTicks);
                        //Debug.Log("time: " + time);
                        noteTime.Add(time);
                        int indexOfTime = noteTime.FindIndex(element => element == time);
                        if(indexOfTime != 0)
                        {
                            convertedTime = noteTime[indexOfTime] - noteTime[indexOfTime-1];
                        }
                        //Debug.Log("converted time: " + convertedTime);
                        int noteNumber = noe.NoteNumber;
                        string realNote = convertToNoteName(noteNumber);
                        Debug.Log("Playing real note: " + realNote);
                        string realNextNote = convertToNoteName(nextNoe.NoteNumber);
                        //Debug.Log("Playing real next note: " + realNextNote);
                        
                        yield return new WaitForSeconds(convertedTime); //or convertedTime 0.9f
                        IsOnCorrectString(realNote, realNextNote);
                    }
                    //else yield return new WaitForSeconds(0.9f);
                }
            }
            //wait after last note is played
            yield return new WaitForSeconds(0.9f); 
            BowState("stop");
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
        //localRotation - use that 
        //inspector - local 
        //but this is in global 
        this.transform.parent.rotation = Quaternion.Slerp(this.transform.parent.rotation, p2,  0.2f);
    }

    //SAVING THIS METHOD FOR LATER IN CASE I NEED TO USE IT AGAIN BUT RIGHT NOT NOT BEING USED
    //help with Animation Events https://www.youtube.com/watch?v=3_ZFSZr-sJI
    //https://gamedev.stackexchange.com/questions/117423/unity-detect-animations-end
    public void BowState(string message)
    {
        // && pan_animation.GetBool("stop") != true
        if(message == "down")
        {
            //Wait until Up is done Playing the play down
            pan_animation.SetBool("up", false);
            pan_animation.SetBool("down", true);
        }
        // && pan_animation.GetBool("stop") != true
        else if(message == "up")
        {
            pan_animation.SetBool("down", false);
            pan_animation.SetBool("up", true);
        }

        else 
        {
            //https://docs.unity3d.com/ScriptReference/AnimationState-enabled.html
            pan_animation.enabled = false;
        }
    }

}
