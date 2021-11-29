using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRangeCuing : MonoBehaviour
{
    [SerializeField] AudioClip inRangeCue;
    [SerializeField] AudioSource audioSource;

    [Tooltip("Objects with this tag activate the with in range sound cuing")]
    [SerializeField] [TagSelector] string Actioner;

    private void Awake()
    {
        if (audioSource == null) GetComponentInParent<AudioSource>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(Actioner))
        {
            audioSource.clip = inRangeCue;
            audioSource.Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(Actioner))
        {
            audioSource.clip = inRangeCue;
            audioSource.Stop();
        }
    }
}
