using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager3 : MonoBehaviour
{
    public TextMeshProUGUI tutorial_text;
    public TutorialStep1[] pop_ups;
    
    private int pop_up_index = 0;

    // Update is called once per frame
    public void DisplayNextPart()
    {
            if(pop_up_index == pop_ups.Length)
            {
                EndTutorial();
                return;
            }
            else
            {
                TutorialStep1 pop_up_at_index =  pop_ups[pop_up_index];
                tutorial_text.SetText(pop_up_at_index.sentence);
                //activate all gameobjects, if gameobjects_to_activate.Length is 0 that means there are no new ones to activate
                //and the currently activated GameObjects are being reused. 
                foreach(GameObject gameobj in pop_up_at_index.gameobjects_to_activate)
                {
                    gameobj.SetActive(true);
                }
                //start the animation clip if there is one
                if(pop_up_at_index.animation.animator != null)
                {
                    AnimationToggle temp = pop_up_at_index.animation;
                    temp.animator.SetBool(temp.anim_clip_condition, temp.trueFalse);
                }
                foreach(GameObject gameobj_to_deactivate in pop_up_at_index.gameobjects_to_deactivate)
                {
                    gameobj_to_deactivate.SetActive(false);
                }
                pop_up_index++;
            }

    }
    void EndTutorial()
    {
        Debug.Log("End of tutorial!");
    }
}
