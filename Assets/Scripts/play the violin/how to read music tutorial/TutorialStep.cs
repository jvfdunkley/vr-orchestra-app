using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TutorialStep
{
    public string sentence;
    public GameObject[] gameobjects_to_activate;
    public AnimationClip anim_clip;
    public string anim_clip_condition;

    public void Activate()
    {
        foreach(GameObject gameobject in gameobjects_to_activate)
        {
            gameobject.SetActive(true);
        }
    }
    public void Finished()
    {
        foreach(GameObject gameobject in gameobjects_to_activate)
        {
            gameobject.SetActive(false);
        }
    }
    public bool isFinished()
    {
         foreach(GameObject gameobject in gameobjects_to_activate)
        {
            if(gameobject.activeSelf != false)
            {
                return false;
            }
        }
        return true;
    }

}
