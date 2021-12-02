using System;
using UnityEngine;
using UnityEngine.InputSystem;

//Controls the toggling between all available slates by listening to the slateToggleInputAction. The SlateDisplayer class enables or disables the object containing this script.
public class SlatesManager : MonoBehaviour
{
    [SerializeField] InputActionReference slateToggleInputAction; 
  
    [SerializeField] Slate[] slates; //stores all available slates.

    [Tooltip("First slate shown")]
    [SerializeField] Slate startingSlate;
    
    int slateIndex;

    Slate activeSlate;
    public Slate ActiveSlate{ get { return activeSlate; } }

    void Awake()
    {
        slateToggleInputAction.action.performed += ToggleSlates; //resgister the response to the  slateToggleInputAction event.

        //Fill if array is empty.
        if (IsArrayEmpty()) slates = GetComponentsInChildren<Slate>(); //not yet implemented.......

        //Hide/Disable all slates except for the active one, set as the startingSlate.
        DisableSlates();
        activeSlate = startingSlate;
        slateIndex = Array.IndexOf(slates, activeSlate);
        activeSlate.gameObject.SetActive(true);
    }

    private bool IsArrayEmpty()
    {
        //TODO
        return false;
    }

    private void ToggleSlates(InputAction.CallbackContext obj)
    {

        if (obj.ReadValue<Vector2>().x < -0.5) slateIndex -= 1; //Move to the left of the slates array.
        else if (obj.ReadValue<Vector2>().x > 0.5) slateIndex += 1;//Move to the right of the slates array.

        //Ensures that an out of index exception does not occur by navigating the array in circles.
        if (slateIndex > slates.Length - 1) slateIndex = 0;
        if (slateIndex < 0) slateIndex = slates.Length - 1;

        //Hide/Disable all slates except for the new active one.
        DisableSlates();
        activeSlate = slates[slateIndex]; //sets the new active slate
        activeSlate.gameObject.SetActive(true);//enables new active screen.
    }

    private void DisableSlates()
    {
        foreach (Slate slate in slates)
        {
            slate.gameObject.SetActive(false);
        }
    }
}
