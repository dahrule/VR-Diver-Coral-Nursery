using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SwitchHands : MonoBehaviour
{
    XRGrabInteractable grabInteractable;
    [SerializeField] Transform leftHandAttach;
    [SerializeField] Transform rightHandAttach;
    [SerializeField] Transform leftHandInteractor;
    [SerializeField] Transform rightHandInteractor;

    // Start is called before the first frame update
    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    //Called from the unity events on the interectable (select event).
    public void SwapHands()
    {
        if(grabInteractable.selectingInteractor.name==leftHandInteractor.name)
        {
            grabInteractable.attachTransform = leftHandAttach;
        }

        else if (grabInteractable.selectingInteractor.name == rightHandInteractor.name)
        {
            grabInteractable.attachTransform = rightHandAttach;
        }
    }
}
