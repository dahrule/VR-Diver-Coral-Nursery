using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//Manages how the Dive screen looks by responding to buttons and dive computer functionalities events (e.g. DepthSensor, NoDecoTimer).
public class DiveScreen : Screen
{
    //Screen elements appearing on this screen.
    [SerializeField] TextMeshProUGUI depth_text;
    [SerializeField] TextMeshProUGUI depth_label;
    [SerializeField] TextMeshProUGUI time_text;
    [SerializeField] public TextMeshProUGUI noDecTime_text;
    [SerializeField] TextMeshProUGUI diveTime_text;
    [SerializeField] TextMeshProUGUI temperature_text;
    [SerializeField] TextMeshProUGUI maxDepth_text;
    [SerializeField] TextMeshProUGUI maxDepth_label;
    [SerializeField] Image alarmIcon;

    private bool depthAsMeters = true;//represents wether the display units are in meters or feet.True=meters; False=feet.


    private void Awake()
    {
        SetInitialStates();

        //Register responses to events.
        DepthSensor.OnDepthChange += DisplayDepths;
        TimeDate.OnTimeUpdate += DisplayTime;
        TimeDate.OnTimeUpdate += DisplayDiveTime; //THIS SHOULD BE MOVED TO ITS OWN CLASS -DIVE TIME- IN A LATER IMPLEMENTATION, SINCE they will activate at different times.
        NoDecoTimer.OnAlertStart += EnbaleAlarmIcon;
    }

    //Formats the received depth values and displays it on screen.
    void DisplayDepths(Dictionary<string, float> depthInfo)
    {
        //Get depth values
        float depth = depthInfo["depth"];
        float maxdepth = depthInfo["maxdepth"];

        //Change units & labels.
        if (!depthAsMeters)
        {
            depth = MetersToFeet(depth);
            maxdepth = MetersToFeet(maxdepth);

            depth_label.text = "ft";
            maxDepth_label.text = "ft";
        }
        else
        {
            depth_label.text = "m";
            maxDepth_label.text = "m";
        }

        //Display formatted depth values.
        depth_text.text = depth.ToString("F1");
        maxDepth_text.text = maxdepth.ToString("F1");

    }

    private static float MetersToFeet(float meters)
    {
        float metersFeetRate = 3.2808f;
        return meters* metersFeetRate;
        
    }

    //Displays the received time value on screen.
    void DisplayTime(Dictionary<string, string> dateTime)
    {
        time_text.text = dateTime["time"];
    }

    void DisplayDiveTime(Dictionary<string, string> dateTime) //TODO: IMPROVE TO SHOW HOURS. FOR THIS PROYECT PLAY TIME WOULD BE SHORTER.
    {
        int minutes = (int)Mathf.Floor(Time.timeSinceLevelLoad / 60);
        diveTime_text.text = minutes.ToString();
    }

    void EnbaleAlarmIcon(int timestart)
    {
        alarmIcon.enabled = true;
    }

    //Defines how the screen looks for the first time. Actives and deactivates specific text elements so that they do not overlap.
    protected override void SetInitialStates()
    {
        depth_text.gameObject.SetActive(true);
        time_text.gameObject.SetActive(true);
        noDecTime_text.gameObject.SetActive(true);
        diveTime_text.gameObject.SetActive(false);
        temperature_text.gameObject.SetActive(true);
        maxDepth_text.gameObject.SetActive(false);
        alarmIcon.enabled = false;

    }

    //When the UpButton is pressed, it toggles between showing the diveTime and the temperature.
    public override void HandleUpButtonPress()
    {
        diveTime_text.gameObject.SetActive(!diveTime_text.gameObject.activeInHierarchy);
        temperature_text.gameObject.SetActive(!temperature_text.gameObject.activeInHierarchy);
    }

    //When the DownButton is pressed, it toggles between showing the time and the maximum depth.
    public override void HandleDownButtonPress()
    {
        time_text.gameObject.SetActive(!time_text.gameObject.activeInHierarchy);
        maxDepth_text.gameObject.SetActive(!maxDepth_text.gameObject.activeInHierarchy);
    }

    public override void HandleSelectButtonPress()
    {
        depthAsMeters = !depthAsMeters; //on Select button press toggle between depth units.
    }

}
