using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyNoteObject : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode keyToPress;

    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(keyToPress))
        {
            if(canBePressed)
            {
                gameObject.SetActive(false);
                canBePressed = false;
                float position_abs = Mathf.Abs(transform.position.x);
                //less than 3.2 or greater than 5 .2
                if(position_abs <= 3.2f || position_abs >= 4.8f)
                {
                    AccuracyGameManager.instance.NormalHit();
                    Instantiate(hitEffect, hitEffect.transform.position, hitEffect.transform.rotation);
                }
                //greater than 3.2 and less than or equal to 3.5 
                //and greater than or equal to 4.6 but less than 5.2
                else if(position_abs > 3.2f && position_abs <= 3.6f || position_abs >= 4.2f && position_abs < 4.8f)
                {
                    AccuracyGameManager.instance.GoodHit();
                    Instantiate(goodEffect, goodEffect.transform.position, goodEffect.transform.rotation);
                }
                else
                {
                    AccuracyGameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, perfectEffect.transform.position, perfectEffect.transform.rotation);
                } 
               
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Activator" && gameObject.activeSelf)
        {
            canBePressed = false;

            AccuracyGameManager.instance.NoteMissed();
            Instantiate(missEffect, missEffect.transform.position, missEffect.transform.rotation);
        }
    }

    
}
