using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Manages how the Dive screen looks by responding to buttons and dive computer functionalities events (e.g. DepthSensor, NoDecoTimer).
public class DiveScreen : Screen
{
    //Screen elements appearing on this screen.
    [SerializeField] TextMeshProUGUI depth_text;
    [SerializeField] TextMeshProUGUI time_text;
    [SerializeField] public TextMeshProUGUI noDecTime_text;
    [SerializeField] TextMeshProUGUI diveTime_text;
    [SerializeField] TextMeshProUGUI temperature_text;
    //[SerializeField] TextMeshProUGUI maxDepth_text; //Not yet implemeted


    private void Awake()
    {
        SetInitialState();
        DepthSensor.OnDepthChange += DisplayDepth; //registers the DisplayDepth method into the OnDepthChange event.
        TimeDate.OnTimeUpdate += DisplayTime; //registers the DisplayTime method into the OnTimeUpdate event.
    }

    //Formats the received depth value and displays it on screen.
    void DisplayDepth(float depth)
    {
        depth_text.text = depth.ToString("F1");
        
    }

    //Displays the received time value on screen.
    void DisplayTime(Dictionary<string, string> dateTime)
    {
        time_text.text = dateTime["time"];
    }

    //Defines how the screen looks for the first time. Actives and deactivates specific text elements so that they do not overlap.
    protected override void SetInitialState()
    {
        depth_text.gameObject.SetActive(true);
        time_text.gameObject.SetActive(true);
        noDecTime_text.gameObject.SetActive(true);
        diveTime_text.gameObject.SetActive(false);
        temperature_text.gameObject.SetActive(true);
        //maxDepth_text.gameObject.SetActive(false);//Not yet implemeted

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
        //maxDepth_text.gameObject.SetActive(!maxDepth_text.gameObject.activeInHierarchy);//Not yet implemeted
    }

}
