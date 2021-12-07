using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class SlateHider : MonoBehaviour
{
    [SerializeField] InputActionReference slateDisplayInputAction; //reference to the button mapped to this action.
    [SerializeField] Slate slate;
    [SerializeField] Ruler ruler;
    [SerializeField] XRSocketInteractor socketInterator;

    void Awake()
    {
        slateDisplayInputAction.action.performed += ToggleVisibility; //subscribe the response to the  slateDisplayInputAction event.
    }

    private void ToggleVisibility(InputAction.CallbackContext obj)
    {
        slate.gameObject.SetActive(!slate.gameObject.activeInHierarchy);
        ruler.GetComponent<Collider>().enabled = !ruler.GetComponent<Collider>().enabled;
        ruler.GetComponent<MeshRenderer>().enabled = !ruler.GetComponent<MeshRenderer>().enabled;
    }

}
