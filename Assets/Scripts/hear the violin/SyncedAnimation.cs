using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.gamasutra.com/blogs/GrahamTattersall/20190515/342454/Coding_to_the_Beat__Under_the_Hood_of_a_Rhythm_Game_in_Unity.php

/*
Purpose of class:
Sync up animation frames to song position in its current loop so there is no delay in the animation after a loop ends
*/
public class SyncedAnimation : MonoBehaviour
{
    //The animator controller attached to this GameObject
    public Animator animator;

    //Records the animation state or animation that the Animator is currently in
    private AnimatorStateInfo animatorStateInfo;

    //Used to address the current state within the Animator using the Play() function
    private int currentState = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Load the animator attached to this object
        animator = GetComponent<Animator>();

        //Get the info about the current animator state
        animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        //Convert the current state name to an integer hash for identification
        currentState = animatorStateInfo.fullPathHash;
    }

    // Update is called once per frame
    void Update()
    {
        //Start playing the current animation from wherever the current conductor loop is
        //This will position the animation at the exact frame of the animation relative to one complete loop.
        animator.Play(currentState, -1, (TrackSongPosition.instance.loopPositionInAnalog));
        //Set the speed to 0 so it will only change frames when you next update it
        animator.speed = 0;
    }
}
