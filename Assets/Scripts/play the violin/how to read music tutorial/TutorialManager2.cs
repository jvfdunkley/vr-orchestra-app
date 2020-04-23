using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager2 : MonoBehaviour
{
    public TutorialStep[] _tutorialSteps;
    public TextMeshProUGUI tutorialText;
    int current = 0; 
    void Start()
    {
        CheckState();
    }
    void Update()
    {
        /*if (current < _tutorialSteps.Length)
        {
                _tutorialSteps[current].Activate();
                tutorialText.SetText(_tutorialSteps[current].sentence);
                //The TutorialComponent just sets itself to finished when the user pressed the button.
                if (_tutorialSteps[current].isFinished())
                {
                    current++;
                }
        }*/
    }
    void CheckState()
    {
        if (current < _tutorialSteps.Length)
        {
                _tutorialSteps[current].Activate();
                tutorialText.SetText(_tutorialSteps[current].sentence);
                //The TutorialComponent just sets itself to finished when the user pressed the button.
                if (_tutorialSteps[current].isFinished())
                {
                    current++;
                }
        }
    }
    public void GoToFinish()
    {
        _tutorialSteps[current].Finished();
    }
}
