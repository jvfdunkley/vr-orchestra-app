using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public GameObject arrowKey;
    public GameObject quarterNote;
    public GameObject staffArrows;
    public GameObject NoteLetters;
    public GameObject BtoGNotes;
    public GameObject violinStrings;
    public GameObject violin;
    public GameObject violinStringNotes;
    public GameObject violinStringNotesTransition;
    public GameObject teachingViolinFingers;
    public GameObject tvfNotes;
    public GameObject fingerTapeQuads;
    public GameObject controlsPanel;
    public GameObject tutorialPanel;
    public GameObject tutorial;
    public Animator arrowkey_animator;
    public Animator camera_animator;
    public TextMeshProUGUI tutorialText;
    public Instruction instruction;

    private Queue<string> sentences;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        /*sentences = new Queue<string>();
        foreach (string sentence in instruction.sentences)
        {
            sentences.Enqueue(sentence);
        }*/
    }
    public void DisplayNextSentence()
    {

        if(count == instruction.sentences.Length)
        {
            EndTutorial();
            return;
        }
        //string sentence = sentences.Dequeue();
        if(count == 0)
        {
            arrowKey.SetActive(true);
        }
        if(count == 2)
        {
            arrowkey_animator.SetBool("isNote", true);
        }
        if(count == 4)
        {
            arrowKey.SetActive(false);
            staffArrows.SetActive(true);
        }
        if(count == 5)
        {
            staffArrows.SetActive(false);
            
            NoteLetters.SetActive(true);
        }
        if(count == 6)
        {
            BtoGNotes.SetActive(true);
        }
        //before 7 shows the text do the animation transition then
        //on 7 show the violin with the string labels 
        if(count == 7)
        {
            BtoGNotes.SetActive(false);
            quarterNote.SetActive(false);
            NoteLetters.SetActive(false);
            camera_animator.SetBool("onPan", true);
            violin.SetActive(true);
            violinStrings.SetActive(true);
            
        }
        SpriteRenderer[] allChildren = violinStringNotes.GetComponentsInChildren<SpriteRenderer>(true);
        List<GameObject> childObjects = new List<GameObject>();
        foreach (SpriteRenderer child in allChildren)
        { 
            childObjects.Add(child.gameObject);
        }
        Transform[] violinStringsChildren = violinStrings.GetComponentsInChildren<Transform>(true);
        List<GameObject> childObjectsStrings = new List<GameObject>();
        foreach (Transform child in violinStringsChildren)
        { 
            childObjectsStrings.Add(child.gameObject);
        }
        if(count == 8)
        {
            violinStringNotes.SetActive(true);
            violinStrings.SetActive(true);
            for(int i = 0; i < childObjects.Count; i++)
            {
                if(childObjects[i].name != "E string note")
                    childObjects[i].SetActive(false);
                
            }
            for(int j = 1; j < childObjectsStrings.Count; j++)
            {
                if(childObjectsStrings[j].name != "E string letter")
                    childObjectsStrings[j].SetActive(false);
                
            }
           // Debug.Log(childObjects[0].name);
            //childObjects[0].SetActive(true);
        }
        if(count == 9)
        {
            childObjects[0].SetActive(false);
            childObjects[1].SetActive(true);

            childObjectsStrings[1].SetActive(false);
            childObjectsStrings[2].SetActive(true);

        }
        if(count == 10)
        {
            childObjects[1].SetActive(false);
            childObjects[2].SetActive(true);

            childObjectsStrings[2].SetActive(false);
            childObjectsStrings[3].SetActive(true);
        }
        if(count == 11)
        {
            childObjects[2].SetActive(false);
            childObjects[3].SetActive(true);

            childObjectsStrings[3].SetActive(false);
            childObjectsStrings[4].SetActive(true);
        }
        if(count == 12)
        {
            childObjects[3].SetActive(false);
            camera_animator.SetBool("onPan", false);
            violin.SetActive(false);
            violinStrings.SetActive(false);
            violinStringNotesTransition.SetActive(true);
            
        }
        Transform[] ftq = fingerTapeQuads.GetComponentsInChildren<Transform>(true);
        List<GameObject> childObjects2 = new List<GameObject>();
        foreach (Transform child in ftq)
        { 
            childObjects2.Add(child.gameObject);
        }
        SpriteRenderer[] tvf = tvfNotes.GetComponentsInChildren<SpriteRenderer>(true);
        List<GameObject> childObjects1 = new List<GameObject>();
        foreach (SpriteRenderer child in tvf)
        { 
            childObjects1.Add(child.gameObject);
        }
        if(count == 15)
        {
            tvfNotes.SetActive(true);
            fingerTapeQuads.SetActive(true);
            for(int i = 0; i < childObjects1.Count; i++)
            {
                if(childObjects1[i].name != "quarter-note B")
                    childObjects1[i].SetActive(false);
            }
            
            for(int j = 1; j < childObjects2.Count; j++)
            {   
                Debug.Log(childObjects2[j].name);
                if(childObjects2[j].name != "A1")
                    childObjects2[j].SetActive(false);
            }
            violinStringNotesTransition.SetActive(false);
            teachingViolinFingers.SetActive(true);
            camera_animator.SetBool("Teach Violin Fingers", true);
        }
        if(count == 16)
        {
            childObjects1[1].SetActive(true);
            childObjects2[1].SetActive(false);
            childObjects2[2].SetActive(true);
        }
        if(count == 17)
        {
            childObjects1[2].SetActive(true);
            childObjects2[2].SetActive(false);
            childObjects2[3].SetActive(true);
        }
        if(count == 18)
        {
            childObjects2[1].SetActive(true);
            childObjects2[2].SetActive(true);
        }
        if(count == 18)
        {
            fingerTapeQuads.SetActive(false);
            tvfNotes.SetActive(false);
        }
        if(count == 20)
        {
            fingerTapeQuads.SetActive(true);
            tvfNotes.SetActive(true);
            for(int j = 1; j < childObjects2.Count; j++)
            {   
                if(childObjects2[j].name != "E1")
                    childObjects2[j].SetActive(false);
            }
            childObjects2[4].SetActive(true);
            for(int i = 0; i < childObjects1.Count; i++)
            {
                if(childObjects1[i].name != "quarter-note F")
                    childObjects1[i].SetActive(false);
            }
            childObjects1[3].SetActive(true);
        }
        if(count == 21)
        {
            childObjects2[4].SetActive(false);
            childObjects2[5].SetActive(true);
            childObjects1[4].SetActive(true);
        }
        if(count == 22)
        {
            fingerTapeQuads.SetActive(false);
            tvfNotes.SetActive(false);
            camera_animator.SetBool("Teach Violin Fingers", false);

        }
        StopAllCoroutines();
        StartCoroutine(TypeSentence(instruction.sentences[count]));
        count++;
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(2.5f);
    }
    IEnumerator TypeSentence(string sentence)
    {
        
        tutorialText.text = "";
        if(count == 7)
        {
            yield return new WaitForSeconds(2.5f);
        } 
        else if(count == 12 || count == 15)
        {
            yield return new WaitForSeconds(1.5f);
        }
        
        
        foreach(char letter in sentence.ToCharArray())
        {
            tutorialText.text += letter;
            yield return null;
        }
    }
    void EndTutorial()
    {
        controlsPanel.SetActive(true);
        tutorialPanel.SetActive(false);
        teachingViolinFingers.SetActive(false);
        tutorial.SetActive(false);

        Debug.Log("end of tutorial");
    }

}
