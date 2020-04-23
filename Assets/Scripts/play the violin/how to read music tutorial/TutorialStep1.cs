using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TutorialStep1
{
    [TextArea(3,10)]
    public string sentence;
    public GameObject[] gameobjects_to_activate;
    public GameObject[] gameobjects_to_deactivate;
    public AnimationToggle animation;
    

}
