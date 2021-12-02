using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SecondaryInputs : MonoBehaviour
{
    [SerializeField]
    InputActionReference controllerPrimaryButton;
    [SerializeField] GameObject []slates;

    int index=0;

    private void Awake()
    {
        controllerPrimaryButton.action.performed += NavigateSlatesInventory;
    }

    private void NavigateSlatesInventory(InputAction.CallbackContext obj)
    {
        index += 1;
        if(index>slates.Length) index = 0;
        
        
    }

}
