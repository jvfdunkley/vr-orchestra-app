using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Click : MonoBehaviour
{
    //this code was helped by https://www.youtube.com/watch?v=-0eqAUkKQpI
    //mainCamera
    public GameObject cameraOne;
    //InstrumentMenuCamera
    public GameObject cameraTwo;
    public GameObject menuCanvas;
    public GameObject instrumentCanvas;

    private GameObject new_instr;

    AudioListener cameraOneAudioLis;
    AudioListener cameraTwoAudioLis;

    private TextMeshProUGUI Text;
    private TextMeshProUGUI parts_text;
    private TextMeshProUGUI hold_text;
    private TextMeshProUGUI hear_text;
    private TextMeshProUGUI learn_text;
    private TextMeshProUGUI play_text;

    // Start is called before the first frame update
    void Start()
    {
        //Get Camera Listeners
        cameraOneAudioLis = cameraOne.GetComponent<AudioListener>();
        cameraTwoAudioLis = cameraTwo.GetComponent<AudioListener>();
        //new_instr = Instantiate(instrument) as GameObject;
        //new_instr.SetActive(false);
        //This helped: https://answers.unity.com/questions/183649/how-to-find-a-child-gameobject-by-name.html
        Text = instrumentCanvas.transform.Find("Panel/menu_options/instrument_name_text").GetComponent<TextMeshProUGUI>();
        parts_text = instrumentCanvas.transform.Find("Panel/menu_options/parts_of_instrument/example1").GetComponent<TextMeshProUGUI>();
        hold_text = instrumentCanvas.transform.Find("Panel/menu_options/how_to_hold/example2").GetComponent<TextMeshProUGUI>();
        hear_text = instrumentCanvas.transform.Find("Panel/menu_options/hear_the_instrument/example3").GetComponent<TextMeshProUGUI>();
        learn_text = instrumentCanvas.transform.Find("Panel/menu_options/learn_about_instrument/example4").GetComponent<TextMeshProUGUI>();
        play_text = instrumentCanvas.transform.Find("Panel/menu_options/play_the_instrument/example5").GetComponent<TextMeshProUGUI>();
        //instrumentCanvas = instrumentCanvas.GetComponent<Canvas>();

    }
    private void OnMouseDown()
    {
        cameraTwo.SetActive(true);
        cameraTwoAudioLis.enabled = true;
        // new_instr.SetActive(true);
        Vector3 default_position = new Vector3(0, 0, 0);
        Quaternion default_rotation = new Quaternion(0, 0, 0, 1);
        new_instr = Instantiate(gameObject, default_position, default_rotation) as GameObject;
        new_instr.tag = "Clone";
        Vector3 position = new Vector3(-342, -100, -20);
        new_instr.transform.position = position;
        new_instr.transform.localScale = new Vector3(800, 800, 800);
        new_instr.transform.SetParent(instrumentCanvas.transform, false);
        //set position and rotation to zero first 
        Text.SetText(gameObject.tag.ToUpper());
        parts_text.SetText(parts_text.text + gameObject.tag);
        hold_text.SetText(hold_text.text + gameObject.tag);
        hear_text.SetText(hear_text.text + gameObject.tag);
        learn_text.SetText(learn_text.text + gameObject.tag);
        play_text.SetText(play_text.text + gameObject.tag);

        menuCanvas.SetActive(false);
        cameraOneAudioLis.enabled = false;
        cameraOne.SetActive(false);
        
    }
}
