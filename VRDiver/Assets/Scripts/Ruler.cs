using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
//Controls the sound behaviour when measuring a coral. //if the coral measured is ready to be planted plays a right sound, else plays a wrong sound.
public class Ruler : MonoBehaviour
{
    [Tooltip("The tag of the object that activates the measuring behaviour (corals)")]
    [SerializeField] [TagSelector] string Actioner;

    [Tooltip("The point that needs to be in contact with the actioner to start the behaviour")]
    [SerializeField] Collider measureContactPoint;

    [Tooltip("Time in seconds that the actioner needs to be in contact to start the behaviour")]
    [SerializeField] float timeToRegisterMeasurement = 1f;

    [SerializeField] AudioClip rightSelection;
    [SerializeField] AudioClip wrongSelection;
    [Tooltip("The attach transform for the ruler at the start of the game")]
    [SerializeField] Transform rulerPosAtStart;


    private AudioSource audioSource;


    private void Awake()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        //Set ruler's position and rotation to the attach point object on the slate.

        //this.gameObject.transform.position = rulerPosAtStart.transform.position;
        //this.gameObject.transform.rotation = rulerPosAtStart.transform.rotation;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Actioner))
        {

            bool IsTouchingACoral = other.gameObject.TryGetComponent<Coral>(out Coral coral);

            //if the coral measured is ready to be planted play a right soundfx, else play a wrong soundfx.
            if (IsTouchingACoral && coral.IsPlantationReady)  
            {
                audioSource.clip = rightSelection;
                audioSource.PlayDelayed(timeToRegisterMeasurement);
            }
            else
            {
                audioSource.clip = wrongSelection;
                audioSource.PlayDelayed(timeToRegisterMeasurement);
            }
        }
        
        
    }
}
