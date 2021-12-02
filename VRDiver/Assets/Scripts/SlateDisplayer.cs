using UnityEngine;
using UnityEngine.InputSystem;

//Enables/Disables the SlatesManager which contains all slates available by listening to the button mapped to the slateDisplayInputAction.
public class SlateDisplayer : MonoBehaviour
{
    [SerializeField] InputActionReference slateDisplayInputAction; 
    [SerializeField] SlatesManager slatesManager;

    private void Awake()
    {
        slateDisplayInputAction.action.performed += ToggleEnableDisable;
    }

    void ToggleEnableDisable(InputAction.CallbackContext obj)
    {
        slatesManager.gameObject.SetActive(!slatesManager.gameObject.activeInHierarchy);
    }

    
}
