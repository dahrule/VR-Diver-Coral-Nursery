using UnityEngine;

public enum CoralTypes { Staghorn, Elkhorn };
public class Coral : MonoBehaviour
{
    
    [Tooltip("The species of the coral")]
    public CoralTypes coralType;

    public bool isPlantationReady; //is the coral ready to be planted?

}


