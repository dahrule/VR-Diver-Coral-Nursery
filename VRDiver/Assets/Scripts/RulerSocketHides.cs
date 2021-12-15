using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class RulerSocketHides : MonoBehaviour
{
    [SerializeField] InputActionReference slateDisplayInputAction; //reference to the button mapped to this action.
    //[SerializeField] Slate slate;
    [SerializeField] GameObject ruler;
    [SerializeField] GameObject socketGraphic;
    [SerializeField] XRSocketInteractor socketInterator;


    void Awake()
    {
        slateDisplayInputAction.action.performed += ToggleVisibility; //subscribe the response to the  slateDisplayInputAction event.
    }

    private void ToggleVisibility(InputAction.CallbackContext obj)
    {
        //slate.gameObject.SetActive(!slate.gameObject.activeInHierarchy);// disable  slate object
        ruler.GetComponent<Collider>().enabled = !ruler.GetComponent<Collider>().enabled; //disable ruler collider
        ruler.GetComponent<MeshRenderer>().enabled = !ruler.GetComponent<MeshRenderer>().enabled; // hide ruler mesh
        socketGraphic.GetComponent<MeshRenderer>().enabled = !socketGraphic.GetComponent<MeshRenderer>().enabled; // hide ruler mesh
    }

}
