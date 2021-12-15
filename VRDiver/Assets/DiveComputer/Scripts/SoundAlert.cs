using System.Collections;
using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource))]

//Plays a sound alarm at an increasing rate when the timeLeft defined in the NoDecoTimer script reaches the alertstart time.
public class SoundAlert : MonoBehaviour
{
    [SerializeField] AudioClip alertShortSFX;
    [SerializeField] AudioClip alertLongSFX;
    [SerializeField] AudioSource audiosource;

    private float repeatInterval;//the rate at which the sound alert plays. It increases as the time limit gets closer to zero.
    private float timeSeconds;//time that starts when the event Limit Reached from NoDecoTimer is received.

    public static event Action OnTimeOver; //event that indicates when the time reaches zero.

    private void Awake()
    {
        if (audiosource == null) audiosource = GetComponent<AudioSource>();

        NoDecoTimer.OnAlertStart += StartAlertRoutine;//subscribe a response to the OnAlertStart event;
    }

    private void StartAlertRoutine(int timeLimitMin)
    {
        timeSeconds = timeLimitMin*60;//converts the timelimit from minutes to seconds.
        audiosource.clip = alertShortSFX;//prepare clip to play.
        StartCoroutine(AlertRoutine());
    }

    IEnumerator AlertRoutine()
    {
         while(timeSeconds > 0)
        {
            
            audiosource.Play();

            //Update repeat interval
            if (timeSeconds <= 60) repeatInterval=1/3f; // At one minute left repeat 3 time per second
            else if(timeSeconds <= 60*2) repeatInterval = 1f; //At 3 minutes left repeat every 1 second.
            else if(timeSeconds <= 60*3)repeatInterval = 2f;//At 5 minutes left repeat every 30 seconds. 
            else if (timeSeconds <= 60 * 5) repeatInterval = 5f;//At 5 minutes left repeat every 30 seconds. 

            //Update time.
            timeSeconds -= 1 * repeatInterval;

            //Send Time Over event when time reaches zero and play a sfx.
            if (timeSeconds <= 0)
            {
                //Play sound that indicates time is over.
                audiosource.PlayOneShot(alertLongSFX);
                OnTimeOver?.Invoke();
            }

            yield return new WaitForSeconds(repeatInterval);
        }
           
    }
}

