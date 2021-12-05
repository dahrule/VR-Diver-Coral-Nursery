using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;

//This script is in charge of getting and formatting Time-Date of the local system for screens of the Dive Computer to access it.
public class TimeDate : MonoBehaviour
{
    private bool timeFormat=true; //represents either of two time formats: 24 or 12 hrs formats.

    private DateTime date; //date from local system.
    private DateTime time; //time from local system.
   
    private Dictionary<string, string> dateTime = new Dictionary<string, string>();

    private IEnumerator dateTimeUpdate; //variable to store the coroutine in charge of updating the date and time.

    public static event Action<Dictionary<string, string>> OnTimeUpdate;

    public static TimeDate singleton;

    private void Awake()
    {
        singleton = this;
        dateTimeUpdate = DateTimeUpdateRoutine();
    }
    private void Start()
    {
        //ReadSystemDateTime();
        //FormatDateTime();
        //OnTimeUpdate?.Invoke(dateTime);
    }
    private void OnEnable()
    {
        StartCoroutine(dateTimeUpdate);
    }
    private void OnDisable()
    {
        StopCoroutine(dateTimeUpdate);
    }

    // Coroutine that Reads and Displays Date-Time every minute.
    private IEnumerator DateTimeUpdateRoutine()
    {
        while (true)
        {
            ReadSystemDateTime();
            FormatDateTime();
            OnTimeUpdate?.Invoke(dateTime);
            yield return new WaitForSeconds(60f);
        }
       
    }

    //Gets the date and time from the local system.
    private void ReadSystemDateTime()
    {
        time = DateTime.Now;
        date = DateTime.Today.Date;
    }

    //Reference for formating options: https://docs.microsoft.com/en-us/dotnet/api/system.datetime.tostring?view=net-6.0 
    private void FormatDateTime()
    {
        String time_string;
        String date_string;

        //Format time display.
        if(timeFormat) time_string = time.ToString("HH:mm"); //24 hrs format.
        else time_string = time.ToString("t"); //12 hrs format.

        //Format date display
        date_string = date.ToString("D");

        //Encapsulate date and time strings inside the dictionary.
        dateTime["date"] = date_string;
        dateTime["time"]=time_string;
    }

    public static void ToggleHourFormat()
    {
         singleton.timeFormat =!singleton.timeFormat;
         singleton.FormatDateTime();
         OnTimeUpdate?.Invoke(singleton.dateTime);
    }

}
