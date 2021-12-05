using UnityEngine;
using System;

[RequireComponent(typeof(DepthSensor),typeof(NoDecoTimer), typeof(TimeDate))]

//Class in charge of managing screens and executing routines related to the different computer functionalities (defined as required components).
public class ComputerUIManager : MonoBehaviour
{
    [Tooltip("Collection of all screens available on the computer")]
    [SerializeField] Screen[] screens;

    [Tooltip("First screen shown which is the active screen at start")]
    [SerializeField] Screen startingScreen;

    private int screenIndex;
    private Screen activeScreen;
    public Screen ActiveScreen { get { return activeScreen; } }

    void Awake()
    {
        //Fill the screens array if empty with type Screens.
        if (IsArrayEmpty()) screens = GetComponentsInChildren<Screen>();

        SetStartingScreen();
    }

    private void SetStartingScreen()
    {
        foreach (Screen screen in screens)
        {
            screen.gameObject.SetActive(false);
        }
        activeScreen = startingScreen;
        screenIndex = Array.IndexOf(screens, activeScreen);
        activeScreen.gameObject.SetActive(true);
    }

    private bool IsArrayEmpty()
    {
        //TODO
        return false;
    }
    

    //Called on the OnClick unity event of the "MODE" Button.
    public void HandleModeBottonPress()
    {
        ToggleScreens();
    }

    private void ToggleScreens()
    {
        activeScreen.gameObject.SetActive(false);//disables current active screen.

        screenIndex++;
        if (screenIndex > screens.Length - 1) screenIndex = 0; //cycles the array.
        activeScreen = screens[screenIndex]; //sets the new active screen

        activeScreen.gameObject.SetActive(true);//enables new active screen.
    }

    //Called on the OnClick unity event of the "UP" Button.
    public void HandleUpBottonPress()
    {
        activeScreen.HandleUpButtonPress();
    }

    //Called on the OnClick unity event of the "DOWN" Button.
    public void HandleDownBottonPress()
    {
        activeScreen.HandleDownButtonPress();
    }

    //Enables routines on the computer related to diving  when a dive starts.
    /*public void EnterDiveMode()
    {
        //Enables:DepthSensor, NoDecoTimer.
        //Sets computer to diveScreen.
    }*/
}
