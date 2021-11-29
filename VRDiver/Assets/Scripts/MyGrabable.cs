using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class MyGrabable : XRGrabInteractable
{
    [Tooltip("Is the object being held?")]
    public bool isHeld=false;
    public bool IsHeld
    { get {return isHeld; } set { isHeld=value; } }

}
