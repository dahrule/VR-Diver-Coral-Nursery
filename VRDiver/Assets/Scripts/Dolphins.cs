
using UnityEngine;

public class Dolphins : MonoBehaviour
{

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip dolphinSFX;

    // Start is called before the first frame update


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
           
            audioSource.clip = dolphinSFX;
            audioSource.loop = true;
            audioSource.Play();
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.loop = false;
        }
    }


}
