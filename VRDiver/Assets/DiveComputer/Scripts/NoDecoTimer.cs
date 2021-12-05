using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

//This script represents the diver's no-decompression time. It indicates the time remaining to be safe underwater. Currently, it is implemented as a fixed countdown timer that starts at the time of each dive. The game ends when the timer reaches the No deco time limit.

public class NoDecoTimer : MonoBehaviour
{
    [Tooltip("Time update interval for coroutine (s)")]
    [SerializeField] int updateInterval = 60;
    [Tooltip("No deco starting time (min)")]
    [SerializeField] int startTime = 5;
    [Tooltip("Safety margin for No-deco time. Time when diver should start surfacing (min)")]
    [SerializeField] int timeLimit = 1;

    [SerializeField] DiveScreen screen;

     public static event Action OnNoDecoTimeOver; //event that indicates when the no-deco time limit has reached the limit, and its time to end the dive.


    //When the object containing this script becomes enabled and active, the coroutine -depth sense- starts runnnig.
    private void OnEnable()
    {
        StartCoroutine(TimeCountDownRoutine(updateInterval));
    }
    private void OnDisable()
    {
        StopCoroutine(TimeCountDownRoutine(updateInterval));
    }

    //Coroutine that runs at an interval to update the timer.
    private IEnumerator TimeCountDownRoutine(float interval)
    {
        while (true)
        {
           screen.noDecTime_text.text = startTime.ToString(); //display timer on computer screen.
            if (startTime == timeLimit) OnNoDecoTimeOver?.Invoke(); //Notify that the safety margin has been reached.
            yield return new WaitForSeconds(updateInterval);
            startTime -= 1;
            if (startTime < 1) startTime = 0;   //clamps start time value to zero.
        }
        
    }
}
