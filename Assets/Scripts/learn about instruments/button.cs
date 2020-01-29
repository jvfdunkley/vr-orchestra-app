using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class button : MonoBehaviour
{
    private TextMeshProUGUI Text;
    private TextMeshProUGUI parts_text;
    private TextMeshProUGUI hold_text;
    private TextMeshProUGUI hear_text;
    private TextMeshProUGUI learn_text;
    private TextMeshProUGUI play_text;
    private GameObject instrument;
    // Start is called before the first frame update
    void Start()
    {
        Text = gameObject.transform.Find("Panel/menu_options/instrument_name_text").GetComponent<TextMeshProUGUI>();
        parts_text = gameObject.transform.Find("Panel/menu_options/parts_of_instrument/example1").GetComponent<TextMeshProUGUI>();
        hold_text = gameObject.transform.Find("Panel/menu_options/how_to_hold/example2").GetComponent<TextMeshProUGUI>();
        hear_text = gameObject.transform.Find("Panel/menu_options/hear_the_instrument/example3").GetComponent<TextMeshProUGUI>();
        learn_text = gameObject.transform.Find("Panel/menu_options/learn_about_instrument/example4").GetComponent<TextMeshProUGUI>();
        play_text = gameObject.transform.Find("Panel/menu_options/play_the_instrument/example5").GetComponent<TextMeshProUGUI>();

    }

    public void Reset()
    {
        instrument = GameObject.FindWithTag("Clone");
        Destroy(instrument);
        Text.SetText("");
        parts_text.SetText("parts of the ");
        hold_text.SetText("how to hold the ");
        hear_text.SetText("hear the ");
        learn_text.SetText("learn about the ");
        play_text.SetText("play the ");
        
    }
}
