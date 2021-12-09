using System;
using System.Collections;
using UnityEngine;


//This script represents the diver's no-decompression time which indicates the time remaining to be safe underwater. Currently, it is implemented as a fixed countdown timer that runs at the start of the game. When the timer reaches zero the game is over.

public class NoDecoTimer : MonoBehaviour
{
    [SerializeField] DiveScreen screen; //reference to the dive screen to update the timer display.

    public int TimeLeftMin; //The time in minutes left for the game to be over.  //set by the game manager.

    public static event Action<int> OnAlertStart; //event that indicates when the sound alert should start.

    const int updateInterval = 60; //Time interval in seconds for the timer update coroutine. default every minute.

    int alertStartMin = 5; //Time in minutes at which the sound alert starts.


    //The coroutine -TimeCountDown- starts and stops runnnig when the object containing this script becomes enabled or disabled, respectively. 
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

            //update timer display on the computer screen.
            screen.noDecTime_text.text = TimeLeftMin.ToString();

            //Check if its time to send a sound alert.
            if (TimeLeftMin == alertStartMin) OnAlertStart?.Invoke(alertStartMin);

            //stop coroutine when time reaches zero.
            if (TimeLeftMin <= 0) StopCoroutine(TimeCountDownRoutine(updateInterval));

            //Update time left by one minute and clamp to 0.
            TimeLeftMin -= updateInterval / updateInterval; 
            TimeLeftMin = Mathf.Clamp(TimeLeftMin, 0, TimeLeftMin);

            yield return new WaitForSeconds(updateInterval);
           
        }
        
    }
}
