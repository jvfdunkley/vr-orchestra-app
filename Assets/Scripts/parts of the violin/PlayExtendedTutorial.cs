using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayExtendedTutorial : MonoBehaviour
{
    public GameObject violin;
    public GameObject tag_Panel;
    public GameObject tutorial_Panel;
    public GameObject  backButton;
    public GameObject backToMenuButton;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI textDisplay;
    public AudioSource narration_source;
    public Animator pan_animation;
    public Annotation[] extended_annotations;
    private SortedDictionary<string, Annotation> map_annotations = new SortedDictionary<string, Annotation>();
    private string part_name;
    //point of the class:
    //play individual extended part and attach it to each button
    // Start is called before the first frame update
    void Start()
    {
        map_annotations.Add("chin_rest", extended_annotations[0]);
        map_annotations.Add("tailpiece", extended_annotations[1]);
        map_annotations.Add("fine_tuners", extended_annotations[2]);
        map_annotations.Add("bridge", extended_annotations[3]);
        map_annotations.Add("f_holes", extended_annotations[4]);
        map_annotations.Add("strings", extended_annotations[5]);
        map_annotations.Add("fingerboard", extended_annotations[6]);
        map_annotations.Add("tuning_pegs", extended_annotations[7]);
        map_annotations.Add("scroll", extended_annotations[8]);
        map_annotations.Add("bow", extended_annotations[9]);
    }

    IEnumerator TypeTranscript1(Annotation thing)
    {
        textDisplay.SetText("");
        foreach (char letter in thing.narration_transcript.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(0.04f);
        }
    }


    public void PlayChinRest()
    {
        Prepare();
        Annotation chinrest = map_annotations["chin_rest"];
        titleText.text = chinrest.part_tag;
        //textDisplay.text = chinrest.narration_transcript;
        narration_source.clip = chinrest.narration;
        narration_source.Play();
        pan_animation.SetBool(chinrest.animation.name, true);
        part_name = "chin_rest";
        StopAllCoroutines();
        StartCoroutine(TypeTranscript1(chinrest));
    }

    public void PlayTailpiece()
    {
        Prepare();
        Annotation tailpiece = map_annotations["tailpiece"];
        titleText.text = tailpiece.part_tag;
        textDisplay.text = tailpiece.narration_transcript;
        narration_source.clip = tailpiece.narration;
        narration_source.Play();
        pan_animation.SetBool(tailpiece.animation.name, true);
        part_name = "tailpiece";
        StopAllCoroutines();
        StartCoroutine(TypeTranscript1(tailpiece));
    }

    public void PlayFineTuners()
    {
        Prepare();
        Annotation fine_tuners = map_annotations["fine_tuners"];
        titleText.text = fine_tuners.part_tag;
        textDisplay.text = fine_tuners.narration_transcript;
        narration_source.clip = fine_tuners.narration;
        narration_source.Play();
        pan_animation.SetBool(fine_tuners.animation.name, true);
        part_name = "fine_tuners";
        StopAllCoroutines();
        StartCoroutine(TypeTranscript1(fine_tuners));
    }

    public void PlayBridge()
    {
        Prepare();
        Annotation bridge = map_annotations["bridge"];
        titleText.text = bridge.part_tag;
        textDisplay.text = bridge.narration_transcript;
        narration_source.clip = bridge.narration;
        narration_source.Play();
        pan_animation.SetBool(bridge.animation.name, true);
        part_name = "bridge";
        StopAllCoroutines();
        StartCoroutine(TypeTranscript1(bridge));
    }

    public void PlayFHoles()
    {
        Prepare();
        Annotation f_holes = map_annotations["f_holes"];
        titleText.text = f_holes.part_tag;
        textDisplay.text = f_holes.narration_transcript;
        narration_source.clip = f_holes.narration;
        narration_source.Play();
        pan_animation.SetBool(f_holes.animation.name, true);
        part_name = "f_holes";
        StopAllCoroutines();
        StartCoroutine(TypeTranscript1(f_holes));
    }

    public void PlayStrings()
    {
        Prepare();
        Annotation strings = map_annotations["strings"];
        titleText.text = strings.part_tag;
        textDisplay.text = strings.narration_transcript;
        narration_source.clip = strings.narration;
        narration_source.Play();
        pan_animation.SetBool(strings.animation.name, true);
        part_name = "strings";
        StopAllCoroutines();
        StartCoroutine(TypeTranscript1(strings));
    }

    public void PlayFingerboard()
    {
        Prepare();
        Annotation fingerboard = map_annotations["fingerboard"];
        titleText.text = fingerboard.part_tag;
        textDisplay.text = fingerboard.narration_transcript;
        narration_source.clip = fingerboard.narration;
        narration_source.Play();
        pan_animation.SetBool(fingerboard.animation.name, true);
        part_name = "fingerboard";
        StopAllCoroutines();
        StartCoroutine(TypeTranscript1(fingerboard));
    }

    public void PlayTuningPegs()
    {
        Prepare();
        Annotation tuning_pegs = map_annotations["tuning_pegs"];
        titleText.text = tuning_pegs.part_tag;
        textDisplay.text = tuning_pegs.narration_transcript;
        narration_source.clip = tuning_pegs.narration;
        narration_source.Play();
        pan_animation.SetBool(tuning_pegs.animation.name, true);
        part_name = "tuning_pegs";
        StopAllCoroutines();
        StartCoroutine(TypeTranscript1(tuning_pegs));
    }

    public void PlayScroll()
    {
        Prepare();
        Annotation scroll = map_annotations["scroll"];
        titleText.text = scroll.part_tag;
        textDisplay.text = scroll.narration_transcript;
        narration_source.clip = scroll.narration;
        narration_source.Play();
        pan_animation.SetBool(scroll.animation.name, true);
        part_name = "scroll";
        StopAllCoroutines();
        StartCoroutine(TypeTranscript1(scroll));
    }

    public void PlayBow()
    {
        Prepare();
        violin.SetActive(false);
        Annotation bow = map_annotations["bow"];
        titleText.text = bow.part_tag;
        textDisplay.text = bow.narration_transcript;
        narration_source.clip = bow.narration;
        narration_source.Play();
        pan_animation.SetBool(bow.animation.name, true);
        part_name = "bow";
        StopAllCoroutines();
        StartCoroutine(TypeTranscript1(bow));
    }

    private void Prepare()
    {
        backToMenuButton.SetActive(false);
        pan_animation.SetBool("end", false);
        backButton.GetComponentInChildren<TextMeshProUGUI>().text = "Back";
        tag_Panel.SetActive(false);
        tutorial_Panel.SetActive(true);
    }

    public void goBack()
    {
        if(titleText.text == "Bow")
        {
            violin.SetActive(true);
        }
        backToMenuButton.SetActive(true);
        pan_animation.SetBool(part_name, false);
        pan_animation.SetBool("end", true);
        narration_source.Stop();
        part_name = "";
    }

}
