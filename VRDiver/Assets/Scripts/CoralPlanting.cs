using System.Collections;
using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource))]
// Simulates the action of planting a coral by forcing the player to hold a coral fragment inside the planting spot for a certain amount of time before the action is considered complete. The action can be interrupted an restore at a later time.
public class CoralPlanting : MonoBehaviour
{
    [Tooltip("Time in seconds that a coral fragment being held must remain inside the trigger for the planting action to be completed")]
    [SerializeField] float secondsToCompleteAction = 5f;

    [Tooltip("The transform for the planted coral")]
    [SerializeField] Transform CoralAttachPoint;

    [Tooltip("Objects with this tag activate the planting action")]
    [SerializeField] [TagSelector] string Actioner;
    
    [Header("Sound & Visual effects")]
    [SerializeField] AudioClip actionComplete;
    [SerializeField] AudioClip plantingAction;
    [Tooltip("A visual cue that indicates how much time is left to complete the action.")]
    [SerializeField] ProgressBar progressBar;

    private IEnumerator executeAction; //defines the coroutine in which the planting action is executed.
    private float coroutineTimeStep = 0.1f; //The time per seconds that the executeAction coroutine runs.
    private float secondsLeftToCompleteAction;//stores the time left to complete the planting action as the action progresses.
    private GameObject coralAtSpot; //a reference to the first coral occupying this coral planting spot.
    private AudioSource audioSource;

    public event Action OnPlantingComplete;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        executeAction = ExecuteActionCoroutine();
        secondsLeftToCompleteAction = secondsToCompleteAction;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Start task timing only if the object colliding is of the correct type and is being held. This invalids dropping the coral without toching the planting spot.
        if (other.CompareTag(Actioner) && coralAtSpot==null)
        {
            Debug.Log("Coral entered");
            coralAtSpot = other.gameObject;
        }
        
        if(coralAtSpot != null && other.CompareTag("Player") )
        {
            Debug.Log("Hand inside with coral in");
            StartCoroutine(executeAction);
        }     
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Actioner))
        {
            coralAtSpot = null;
            //StopCoroutine(executeAction);
        }
        else if(coralAtSpot != null && other.CompareTag("Player"))
        {
           // StopCoroutine(executeAction);
        }
    }

    //The coroutine where the planting action is executed. Runs at -coroutineTimeStep- times per second.
    private IEnumerator ExecuteActionCoroutine()
    {
        while (true)
        {
            //The execution of the planting action is considered to be in progress only when a coral is inside the planting spot and it is being held. The action can be interrupted and restore at a later time.
            if (coralAtSpot != null && coralAtSpot.GetComponent<MyGrabable>().IsHeld)
            {
                secondsLeftToCompleteAction -= 1 * coroutineTimeStep; //update the time required to complete the action. Time decreases as the player keeps grabbing the coral inside  the planting spot.
                Debug.Log(secondsLeftToCompleteAction.ToString("F1"));
                progressBar.FillAmount = Normalize(secondsToCompleteAction,0,secondsLeftToCompleteAction);////Update the progress bar to cue how much time is left to complete the action.

                //play sound FX to cue that a planting action is being executed.
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = plantingAction;
                    audioSource.Play();
                }
            }
            else if (coralAtSpot == null||!coralAtSpot.GetComponent<MyGrabable>().IsHeld)//The planting action is interrupted
            {
                audioSource.Stop(); //stop cuing the planting action.
            }

            //when the player has maintained the coral held inside the plating spot for enough time, the coroutine ends, and the action is complete.
            if (secondsLeftToCompleteAction<0)
            {
                break;
            }
            yield return new WaitForSeconds(coroutineTimeStep);   
        }
        CompleteTask();  
    }

    //Conclude the planting action by playing a sound, positioning the coral in an upward position, disabling the grabbable behavior of the coral, notifying other objects interested in the event, and destroying the planting spot gameObject.
    private void CompleteTask()
    {
        audioSource.Stop();//stop any sound currently being played on this gameobject's audio source.
        audioSource.PlayOneShot(actionComplete);

        coralAtSpot.GetComponent<MyGrabable>().enabled = false;//disable the planted coral as a grabbable object. 

        //Set the transform for the planted coral.
        coralAtSpot.transform.position = CoralAttachPoint.transform.position;
        coralAtSpot.transform.rotation = CoralAttachPoint.transform.rotation;

        //broadcast that the planting action is complete for other objects interested in it to listen.
        OnPlantingComplete?.Invoke();

        Destroy(this.gameObject,1);
    }
    
    //Normalize a set of values with a minimum and maximum values between 0 and 1.
    private float Normalize(float min, float max, float value)
    {
        float value_norm = (value - min) / (max - min);
        return value_norm;
    }
}
