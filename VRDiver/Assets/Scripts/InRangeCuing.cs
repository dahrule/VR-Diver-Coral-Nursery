
using System.Collections.Generic;
using UnityEngine;

public class InRangeCuing : MonoBehaviour
{
    public static List<AudioSource> audioSources = new List<AudioSource>();

    [Header("Object references")]
    [SerializeField] AudioClip alertcueSFX;
    [SerializeField] AudioSource audioSource;

    [Tooltip("Objects with this tag activate the cue sound when entering the trigger zone")]
    [SerializeField] [TagSelector] string Actioner;
    [SerializeField] float repeatRate=2f;
    [SerializeField] float distanceToStopPlaying = 1f;

    private float clipLength;
    private Vector3 plantingSpotPosition;

    private void Awake()
    {
        if (audioSource == null) audioSource=GetComponentInParent<AudioSource>();
        plantingSpotPosition = GetComponentInParent<Transform>().position;
        
    }

    private void Start()
    {
        InRangeCuing.audioSources.Add(this.audioSource);
        clipLength = alertcueSFX.length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(Actioner)&& !IsASourceAlreadyPlaying())
        {
            audioSource.clip = alertcueSFX;
            InvokeRepeating("PlaySFX",1f,clipLength+repeatRate);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(Actioner))
        {
            CancelInvoke();
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(Actioner))
        {
            var playerPosition = other.gameObject.transform.position;
            //Get the distance between the player and the coral spot, if it is to close, stop playing the cue sound.
            float playerDistanceToPlantingSpot=Vector3.Distance(playerPosition,plantingSpotPosition);
            if(playerDistanceToPlantingSpot<distanceToStopPlaying) CancelInvoke();
        }
    }

    private void PlaySFX()
    {
        audioSource.PlayOneShot(alertcueSFX,0.1f);
    }

    private bool IsASourceAlreadyPlaying()
    {
        foreach(AudioSource source in InRangeCuing.audioSources)
        {
            if (source.isPlaying) return true;
        }
        return false;
    }
}
