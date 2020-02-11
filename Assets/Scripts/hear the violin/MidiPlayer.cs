using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Melanchall.DryWetMidi.Core;
//using Melanchall.DryWetMidi.Interaction;
//using Melanchall.DryWetMidi.Devices;
using System.Linq;
using NAudio.Midi;
//thank you to https://gist.github.com/CustomPhase/033829c5c30f872d250a79e3d35b7048#file-midireader-cs-L20
public class MidiPlayer : MonoBehaviour
{
    public MidiFile midiFile;
    private float deltaTicks;
    public GameObject violin;


    void Start()
    {
        /*
        The formula is 6000/ (BPM * PPQ)
        BPM = 80
        PPQ = 960 pretty sure 

        */
        midiFile = new MidiFile("Assets/CmajorScaleOneOctave.mid", false);
        
        deltaTicks = midiFile.DeltaTicksPerQuarterNote;
        StartPlayback();
       
    }

    public void StartPlayback()
	{

    		//9 is the number of the track we are reading notes from
    		//you'll have to experiment with that, i cant remember why i chose 9 here
            foreach (MidiEvent note in midiFile.Events[0])
            {
                //If its the start of the note event
                if (note.CommandCode == MidiCommandCode.NoteOn)
                {
                        //Cast to note event and process it
                    //Debug.Log("note " + note);
                    NoteOnEvent noe = (NoteOnEvent)note;
                    NoteEvent(noe);
                }
            }
    }
	public void NoteEvent(NoteOnEvent noe)
	{
    		//The bpm(tempo) of the track
    		float bpm = 80;
    
    		//Time until the start of the note in seconds
		float time = (60 * noe.AbsoluteTime) / (bpm * deltaTicks);
        Debug.Log("time " + time);
    
		//The number (key) of the note. Heres a useful chart of number-to-note translation:
		//http://www.electronics.dit.ie/staff/tscarff/Music_technology/midi/midi_note_numbers_for_octaves.htm
		int noteNumber = noe.NoteNumber;

		//Start coroutine for each note at the start of the playback
		//Really awful way to do stuff, but its simple
		StartCoroutine(CreateAction(time, noteNumber));
	}

	IEnumerator CreateAction(float t, int noteNumber)
	{
        string[] noteString = new string[] { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
        int octave = (noteNumber / 12) - 1;
        int noteIndex = (noteNumber % 12);
        string note = noteString[noteIndex];
    		//Wait for the start of the note
		yield return new WaitForSeconds(t);
		//The note is about to play, do your stuff here
    		Debug.Log("Playing Midi note: "+noteNumber);
            Debug.Log("Playing real note: " + note);
            if(noteNumber == 62)
            {
                violin.transform.position = new Vector3(0,32,0);
            }
            else
            {
                violin.transform.position = new Vector3(0,0,0);
            }
	} 
}
