using UnityEngine;

//Base class for Screen objects.
public class Screen : MonoBehaviour
{
    public virtual void HandleUpButtonPress() { }
    public virtual void HandleDownButtonPress() { }
    public virtual void HandleSelectButtonPress() { }
    protected virtual void SetInitialState() { }
}
