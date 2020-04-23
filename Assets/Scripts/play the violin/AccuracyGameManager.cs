using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AccuracyGameManager : MonoBehaviour
{
    public AudioSource theMusic;

    public bool startPlaying;

    public AccuracyBeatScroller theBS;

    public GameObject button;
    
    public GameObject instructionPanel;

    public GameObject startGamePromptPanel;

    public GameObject endPanel;

    public bool replayAgain = false;


    //only one variable allowed
    //can be used in multiple different scripts but it will get the most recently updated instance to use 
    public static AccuracyGameManager instance;

    public int currentScore;

    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds; 

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiText;

    public float totalNotes;
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missedHits;

    public GameObject resultsScreen;
    public TextMeshProUGUI normalNotesHitText, goodNotesHitText, perfectNotesHitText, missedNotesHitText, percentageNotesHitText, scoreResultText, finalResultText;


    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        scoreText.GetComponent<TextMeshProUGUI>();
        multiText.GetComponent<TextMeshProUGUI>();
        scoreText.SetText("Score: 0");
        currentMultiplier = 1;
        totalNotes = 42;
        //totalNotes = FindObjectsOfType<AccuracyNoteObject>().Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(!startPlaying || replayAgain)
        {
            if(Input.GetKey(KeyCode.Space) && instructionPanel.activeSelf == false)
            {
                startPlaying = true;
                theBS.hasStarted = true;
                startGamePromptPanel.SetActive(false);
                

                theMusic.Play();
            }
           /* else if(replayAgain == true)
            {
                if(Input.GetKey(KeyCode.Space))
                {
                    startPlaying = true;
                theBS.hasStarted = true;
                startGamePromptPanel.SetActive(false);
                

                theMusic.Play();
                }
                
            }*/
        }
        else
        {
            if(!theMusic.isPlaying && !resultsScreen.activeInHierarchy)
            {
                resultsScreen.SetActive(true);
                normalNotesHitText.SetText("" + normalHits);
                goodNotesHitText.SetText("" + goodHits);
                perfectNotesHitText.SetText("" + perfectHits);
                missedNotesHitText.SetText("" + missedHits);

                float totalHit = normalHits + goodHits + perfectHits;
                float percentHit = ((totalHit - missedHits)/ totalNotes) * 100f;
                //show the float value to 1 decimal place 
                percentageNotesHitText.SetText(percentHit.ToString("F1") + "%");
                scoreResultText.SetText(currentScore.ToString());
                if(percentHit >= 80)
                {
                    finalResultText.SetText("Amazing!");
                }
                else if(percentHit < 80 && percentHit >= 60)
                {
                    finalResultText.SetText("Good");
                }
                else if(percentHit < 60 && percentHit >= 40)
                {
                    finalResultText.SetText("Bad");
                }
                else if(percentHit < 40)
                {
                    finalResultText.SetText("Awful");
                }
            }
        }
    }
    public void NoteHit()
    {
        if(currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;
            if(multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }
        
        multiText.SetText("Multiplier: x" + currentMultiplier);
        scoreText.SetText("Score : " + currentScore);
    }

    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
        normalHits++;
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
        goodHits++;
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
        perfectHits++;
    }

    public void NoteMissed()
    {
        currentMultiplier = 1;
        multiplierTracker = 0;
        multiText.SetText("Multiplier: x" + currentMultiplier);
        missedHits++;
    }

    public void ResetPlayAgain()
    {
        startPlaying = false;
        //replayAgain = true;
        theBS.reset();
        
    }




}
