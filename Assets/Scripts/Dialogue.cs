using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public GameObject continueButton;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI textDisplay;
    public AudioSource narration_source;
    public Animator pan_animation;
	public Annotation[] annotations;
    public GameObject tag_Panel;
    public GameObject tutorial_Panel;
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
          
        pan_animation.SetBool(annotations[index].animation.name, true);
        titleText.text = annotations[index].part_tag;
        narration_source.Play();
        textDisplay.SetText("");
		foreach (char letter in annotations[index].narration_transcript.ToCharArray())
		{
			textDisplay.text += letter;
            yield return new WaitForSeconds(0.04f);
		}

	}

    public void DisplayNextSentence()
    {

        if (index < annotations.Length - 1)
        {
            //continueButton.SetActive(false);
            index++;
            textDisplay.text = "";
            narration_source.clip = annotations[index].narration;
            StopAllCoroutines();
            StartCoroutine(TypeTranscript());

        }
        else
        {
            //textDisplay.text = "";
            EndAnnotation();
            return;
        }
    }


    void EndAnnotation()
    {
        tutorial_Panel.SetActive(false);
        tag_Panel.SetActive(true);
        Debug.Log("End of annotations");
    }

}
