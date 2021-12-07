using System;
using UnityEngine;
using UnityEngine.InputSystem;


//Controls the hide/show action of the slatesbook object, and the toggle action between all available slates in the book. 
public class SlatesManager : MonoBehaviour
{
    #region Variables
    [SerializeField] InputActionReference slateToggleInputAction; //reference to the button mapped to this action.
    [SerializeField] InputActionReference slateDisplayInputAction; //reference to the button mapped to this action.

    [SerializeField] Slate[] slates; //stores all available slates.

    [Tooltip("First slate appearing at game start")]
    [SerializeField] Slate startingSlate;
    
    int slateIndex;
    Slate activeSlate; 
    public Slate ActiveSlate{ get { return activeSlate; } }

    bool toggleAllow = true; //Is the slateToggleInputAction allow?
    #endregion

    #region BuiltInMethods
    void Awake()
    {
        slateDisplayInputAction.action.performed += ToggleEnableDisable; //subscribe the response to the  slateDisplayInputAction event.
        slateToggleInputAction.action.performed += ToggleSlates; //subscribe the response to the  slateToggleInputAction event.
       
        //Fill if slates array is empty.
        if (IsArrayEmpty()) slates = GetComponentsInChildren<Slate>(); //not yet implemented.......
    }

    private void Start()
    {
        //Hide/Disable all slates except for the active one which is the startingSlate.
        DisableSlates();
        activeSlate = startingSlate;
        slateIndex = Array.IndexOf(slates, activeSlate);//get the array index of the active slate.
        activeSlate.gameObject.SetActive(true);
    }
    #endregion

    #region BuiltInMethods
    private void ToggleEnableDisable(InputAction.CallbackContext obj)
    {
        toggleAllow = !toggleAllow;
        activeSlate.gameObject.SetActive(!activeSlate.gameObject.activeInHierarchy);
    }

    private void ToggleSlates(InputAction.CallbackContext obj)
    {
        if(toggleAllow)
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
        
    }

    private void DisableSlates()
    {
        foreach (Slate slate in slates)
        {
            slate.gameObject.SetActive(false);
        }
    }
    
    private bool IsArrayEmpty()
    {
        //TODO
        return false;
    }
    #endregion
}
