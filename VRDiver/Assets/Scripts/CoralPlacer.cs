using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

//Assigns each coral in the scene to a socket at the tower, and assigns randomly a IsPlantaionReady value to each coral.
public class CoralPlacer : MonoBehaviour
{
    [SerializeField] XRSocketInteractor[] coralSockets; //array containing all coral sockets in the scene.
    [SerializeField] Coral[] corals; //array containing all corals in the scene.
    [Tooltip("The probability of getting a plantation ready coral. The complement of this value will be set as  the opposite (not ready for plantation)")]
    [Range(0,100)]
    [SerializeField] float plantationReadyProbability = 50f;

    private void Awake()
    {
        for (int i = 0; i < corals.Length; i++)
        {
            corals[i].IsPlantationReady = AssignRandomValue();//Randomly assign a value to the field IsPlantation ready.
            coralSockets[i].startingSelectedInteractable = corals[i].gameObject.GetComponent<XRBaseInteractable>();//Assign the instantiated coral gameobject to a socket at the tower.
        }
    }

    private bool AssignRandomValue()
    {
        int randnumber = Random.Range(0, 100);
        if (randnumber < plantationReadyProbability)
        {
            return true;
        }
        else 
            return false;
    }
}
