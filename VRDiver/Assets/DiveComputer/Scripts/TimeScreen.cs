using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Manages how the Time screen looks by responding to buttons and TimeDate events.
public class TimeScreen : Screen
{
    //Screen elements appearing on this screen.
    public TextMeshProUGUI time_text;
    public TextMeshProUGUI date_text;

    private void Awake()
    {
        TimeDate.OnTimeUpdate += DisplayDateTime;//registers the DisplayDateTime method into the OnTimeUpdate event.
    }

    //Displays the received date-time values on screen.
    void DisplayDateTime(Dictionary<string, string> dateTime)
    {
        time_text.text = dateTime["time"];
        date_text.text = dateTime["date"];
    }

    //When the DownButton or UpBotton is pressed, it toggles between 24hr or 12hr formats.
    public override void HandleDownButtonPress()
    {
        TimeDate.ToggleHourFormat();
    }
    public override void HandleUpButtonPress()
    {
        TimeDate.ToggleHourFormat();
    }

}
