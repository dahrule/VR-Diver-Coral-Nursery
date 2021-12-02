using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource))]
// Represents the action of planting a coral by forcing the player to hold a coral fragment inside the planting spot for a certain amount of time before the action is considered complete. The action can be interrupted and restored at a later time.
public class CoralPlanting : MonoBehaviour
{
   
    public static Dictionary<CoralTypes, int> coralsCollection = InitDict(); 
    
    public static event Action<Dictionary<CoralTypes,int>> OnPlantingActionComplete;

    [Tooltip("Time in seconds that a coral fragment being held must remain inside the trigger for the planting action to be completed")]
    [SerializeField] float secondsToCompleteAction = 5f;

    [Tooltip("The transform for the planted coral")]
    [SerializeField] Transform CoralAttachPoint;

    [Tooltip("Objects with this tag activate the planting action")]
    [SerializeField] [TagSelector] string Actioner;
    
    [Header("Sound & Visual effects")]
    [SerializeField] AudioClip actionCompleteSFX;
    [SerializeField] AudioClip plantingSFX;

    [Tooltip("A visual cue that indicates how much time is left to complete the action.")]
    [SerializeField] ProgressBar progressBar;

    private IEnumerator plantingActionCheck; //defines the coroutine in which the planting action is checked for execution.
    private float coroutineTimeStep = 0.1f; //The time rate at which the executeAction coroutine runs.
    private float secondsLeftToCompleteAction;//stores the time left to complete the planting action as the action progresses.
    private GameObject coralAtSpot; //a reference to the first coral occupying this coral planting spot.
    private AudioSource audioSource;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = plantingSFX; //assign an audioclip to avoid null reference errors later.
        plantingActionCheck = PlantingActionCheckCoroutine();
        secondsLeftToCompleteAction = secondsToCompleteAction;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Start planting-action check only if the object colliding the planting spot trigger is of the correct type and is being held.
        if (other.CompareTag(Actioner) && coralAtSpot == null)
        {
            coralAtSpot = other.gameObject;

            if (coralAtSpot.GetComponent<MyGrabable>().IsHeld)
            {
                StopCoroutine(plantingActionCheck);
                StartCoroutine(plantingActionCheck);
            }
        }
        //Alternatively, start the planting-action check when there is a coral already within the trigger, and the player enters. Accounts for the case when the player has left without concluding the planting action.
        else if (coralAtSpot != null && other.CompareTag("Player"))
        {
            StopCoroutine(plantingActionCheck);
            StartCoroutine(plantingActionCheck);
        }     
    }
    
    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag(Actioner))//coral leaves the trigger.
        {
            coralAtSpot = null;
            InterruptAction(); //stops sounds
            StopCoroutine(plantingActionCheck);
        }
        else if(coralAtSpot != null && other.CompareTag("Player"))//player leaves the trigger while a coral remains there.
        {
            InterruptAction();
            StopCoroutine(plantingActionCheck);
            
        }
    }

    //The coroutine where the planting-action check is executed. Runs at -coroutineTimeStep- times per second.
    private IEnumerator PlantingActionCheckCoroutine()
    {
        while (true)
        {
            //The execution of the planting action is considered to be in progress only when a coral is inside the planting spot and it is being held. The action can be interrupted and restore at a later time.
            if (coralAtSpot != null && coralAtSpot.GetComponent<MyGrabable>().IsHeld)
            {
                ExecuteAction();
            }
            else if (coralAtSpot == null||!coralAtSpot.GetComponent<MyGrabable>().IsHeld)//The planting action is interrupted, if the player stops holding the coral.
            {
                InterruptAction();
            }

            //when the player has maintained the coral held inside the plating spot for enough time, the coroutine ends, and the action is complete.
            if (secondsLeftToCompleteAction<0)
            {
                CompleteTask();
                break;
            }
            yield return new WaitForSeconds(coroutineTimeStep);   
        }

    }

    private void InterruptAction()
    {
        if (audioSource.clip.Equals(plantingSFX)) audioSource.Stop(); //stop audio cuing for the planting action.
        else return;
    }

    private void ExecuteAction()
    {
        secondsLeftToCompleteAction -= 1 * coroutineTimeStep; //update the time as the player is grabbing the coral inside the planting spot.
        
        //Debug.Log(secondsLeftToCompleteAction.ToString("F1"));
        
        progressBar.FillAmount = Normalize(secondsToCompleteAction, 0, secondsLeftToCompleteAction);////Update the progress bar to cue how much time is left to complete the action.

        //play sound FX to cue that a planting action is being executed.
        if (!audioSource.isPlaying)
        {
            audioSource.clip = plantingSFX;
            audioSource.Play();
        }
    }

    //Conclude the planting action.
    private void CompleteTask()
    {
        audioSource.clip = actionCompleteSFX;
        audioSource.PlayOneShot(actionCompleteSFX);

        coralAtSpot.GetComponent<MyGrabable>().enabled = false;//disable the planted coral as a grabbable object. 

        //Set the transform for the planted coral.
        coralAtSpot.GetComponent<Rigidbody>().isKinematic = true;
        coralAtSpot.transform.position = CoralAttachPoint.transform.position;
        coralAtSpot.transform.rotation = CoralAttachPoint.transform.rotation;

        //Invoke the OnComplete event.
        Coral coral = coralAtSpot.GetComponent<Coral>();
        CoralPlanting.coralsCollection[coral.coralType]= CoralPlanting.coralsCollection[coral.coralType]+1;
        OnPlantingActionComplete?.Invoke(CoralPlanting.coralsCollection);
        
        Destroy(this.gameObject, actionCompleteSFX.length);
    }

        //Normalize a set of values with a minimum and maximum values between 0 and 1.
        private float Normalize(float min, float max, float value)
    {
        float value_norm = (value - min) / (max - min);
        return value_norm;
    }

    public static Dictionary<CoralTypes,int> InitDict()
    {
        Dictionary<CoralTypes, int> temp_coralsCollection = new Dictionary<CoralTypes, int>();

        foreach (CoralTypes val in Enum.GetValues(typeof(CoralTypes)))
        {
            temp_coralsCollection.Add(val, 0);
        }
        return temp_coralsCollection;
    }
}
