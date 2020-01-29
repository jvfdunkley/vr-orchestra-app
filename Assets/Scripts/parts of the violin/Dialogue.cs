using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public GameObject continueButton;
    public GameObject backToMenuButton;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI textDisplay;
    public AudioSource narration_source;
    public Animator pan_animation;
	public Annotation[] annotations;
    public GameObject tag_Panel;
    public GameObject tutorial_Panel;
    public GameObject violin;
	private int index;
    // Start is called before the first frame update
    void Start()
    {
		StopAllCoroutines();
        narration_source.clip = annotations[index].narration;
		StartCoroutine(TypeTranscript());
	}
    void Update()
    {
        if(textDisplay.text == annotations[index].narration_transcript)
        {
            continueButton.SetActive(true);
        }
    }
    IEnumerator TypeTranscript()
	{
        if(index == annotations.Length-1)
        {
            continueButton.GetComponentInChildren<TextMeshProUGUI>().text = "End tutorial";
        }
        /*if(annotations[index].animation.name != "Idle")
        {
            pan_animation.SetBool(annotations[index].animation.name, true);
        }*/
        if(annotations[index].animation.name == "bow")
        {
            violin.SetActive(false);
        }
        titleText.text = annotations[index].part_tag;
        narration_source.Play();
        textDisplay.SetText("");
		foreach (char letter in annotations[index].narration_transcript.ToCharArray())
		{
			textDisplay.text += letter;
            yield return new WaitForSeconds(0.03f);
		}
        continueButton.SetActive(true);

	}

    public void DisplayNextSentence()
    {
        backToMenuButton.SetActive(false);
        if (index < annotations.Length - 1)
        {
            if (annotations[index].animation.name != "Idle")
            {
                pan_animation.SetBool(annotations[index].animation.name, false);
            }
            
            continueButton.SetActive(false);
            index++;
            textDisplay.text = "";
            narration_source.clip = annotations[index].narration;
            if (annotations[index].animation.name == "summary")
            {
                violin.SetActive(true);
            }
            pan_animation.SetBool(annotations[index].animation.name, true);
            StopAllCoroutines();
            StartCoroutine(TypeTranscript());

        }
        else
        {
            //textDisplay.text = "";
            pan_animation.SetBool("summary", false);
            pan_animation.SetBool("end", true);
            EndAnnotation();
            return;
        }
    }


    void EndAnnotation()
    {
        backToMenuButton.SetActive(true);
        tutorial_Panel.SetActive(false);
        tag_Panel.SetActive(true);
        narration_source.Stop();
        Debug.Log("End of annotations");
    }

}
