using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayExtendedTutorial : MonoBehaviour
{
    public GameObject tag_Panel;
    public GameObject tutorial_Panel;
    public GameObject continueButton;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI textDisplay;
    public AudioSource narration_source;
    public Animator pan_animation;
    public Annotation[] extended_annotations;
    private SortedDictionary<string, Annotation> map_annotations = new SortedDictionary<string, Annotation>();
    //point of the class:
    //play individual extended part and attach it to each button
    // Start is called before the first frame update
    void Start()
    {
        map_annotations.Add("chin_rest", extended_annotations[0]);
        Debug.Log(extended_annotations[0].part_tag);
        Debug.Log(extended_annotations[0].narration_transcript);
        /*map_annotations.Add("tailpiece", extended_annotations[1]);
        map_annotations.Add("fine_tuners", extended_annotations[2]);
        map_annotations.Add("bridge", extended_annotations[3]);
        map_annotations.Add("f_holes", extended_annotations[4]);
        map_annotations.Add("strings", extended_annotations[5]);
        map_annotations.Add("fingerboard", extended_annotations[6]);
        map_annotations.Add("tuning_pegs", extended_annotations[7]);
        map_annotations.Add("scroll", extended_annotations[8]);
        map_annotations.Add("bow", extended_annotations[9]);*/
    }

    public void PlayChinRest()
    {
        /*tag_Panel.SetActive(false);
        tutorial_Panel.SetActive(true);
        continueButton.GetComponentInChildren<TextMeshProUGUI>().text = "Ok";
        map_annotations.Add("chin_rest", extended_annotations[0]);
        Debug.Log(extended_annotations[0]);*/
        continueButton.GetComponentInChildren<TextMeshProUGUI>().text = "Back";
        tag_Panel.SetActive(false);
        tutorial_Panel.SetActive(true);
        Annotation chinrest = map_annotations["chin_rest"];
        //GameObject.FindGameObjectWithTag("chin_rest");
        titleText.text = chinrest.part_tag;
        textDisplay.text = chinrest.narration_transcript;
        narration_source.clip = chinrest.narration;
        pan_animation.SetBool(chinrest.animation.name, true);




    }

}
