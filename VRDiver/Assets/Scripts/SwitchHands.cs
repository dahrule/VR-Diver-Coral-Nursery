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
    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    // Update is called once per frame
    public void SwapHands()
    {
        if(grabInteractable.selectingInteractor.name==leftHandInteractor.name)
        {
            grabInteractable.attachTransform = leftHandAttach;
        }

        if (grabInteractable.selectingInteractor.name == rightHandInteractor.name)
        {
            grabInteractable.attachTransform = rightHandAttach;
        }
    }
}
