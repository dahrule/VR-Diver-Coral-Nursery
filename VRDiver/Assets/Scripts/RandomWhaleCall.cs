using UnityEngine;

[RequireComponent(typeof(AudioSource))]

//Runs a function repeatedly that randomly decides if a sound is played or not. 
public class RandomWhaleCall : MonoBehaviour
{
    [Tooltip("Probability of playing the sound when choosing a number between 0 and 10")]
    [Range(0, 10)]
    [SerializeField] int probability=4; 
    [Tooltip("How often in seconds, is the repeating function called.")]
    [SerializeField] float repeatRate= 5 * 60; 
    [Tooltip("Time in seconds before the first call of the repeating fuction")]
    [SerializeField] float firstplayTime=5*60;
    [SerializeField] AudioClip callSFX; //audio to be played.

    private AudioSource audiosource;


      private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("PlaySoundRandomly",firstplayTime, repeatRate);
    }

    void PlaySoundRandomly()
    {
        int randnumber=Random.Range(0, 10);
        if(randnumber<probability)
        { 
            if(!audiosource.isPlaying) audiosource.PlayOneShot(callSFX);
        } 
    }
}
