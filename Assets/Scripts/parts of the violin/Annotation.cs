using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Annotation
{
    public string part_tag;
    public AudioClip narration;
    public AnimationClip animation;
    [TextArea(3,10)]
    public string narration_transcript;
}
