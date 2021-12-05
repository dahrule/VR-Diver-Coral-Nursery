using UnityEngine;

public enum CoralTypes { Staghorn, Elkhorn }; // available coral species.

//Properties of coral objects
public class Coral : MonoBehaviour
{
    [Tooltip("The coral species")]
    public CoralTypes coralType;

    [SerializeField] bool isPlantationReady; //is the coral ready to be planted?
    public bool IsPlantationReady { get { return isPlantationReady; } }

}


