using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Boat : MonoBehaviour
{

    [SerializeField] Transform player; //The transform of the player.
    [SerializeField] int maxdistanceFromPlayer= 10; //the distance at which the boat starts following the player.
    [SerializeField] int mindistanceFromPlayer = 5; //the distance at which the boat stops following the player.

    [Header("SFX audio clips")]
    [SerializeField] AudioClip startEngine_sfx;
    [SerializeField] AudioClip stopEngine_sfx;
    [SerializeField] AudioClip runEngine_sfx;

    Vector2 playerPosition;
    Vector2 boatposition;

    AudioSource audiosource;
    

    void Awake()
    {
        if(player==null) player = GameObject.FindGameObjectWithTag("Player").transform;
        audiosource = GetComponent<AudioSource>();

    }
    // Start is called before the first frame update
    void Start()
    {
        //Create vectors describing the boat's and player's movement in the horizontal plane only.
        boatposition = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.z);
        boatposition = new Vector2(player.gameObject.transform.position.x, player.gameObject.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        //Get distance to player
        var distanceToPlayer = Vector3.Distance(boatposition,playerPosition);
        if (distanceToPlayer > maxdistanceFromPlayer)
        {
            //Play start engine sfx.
            if(!audiosource.isPlaying)
            {
                audiosource.PlayOneShot(startEngine_sfx);
            }
        }
        else if (distanceToPlayer < mindistanceFromPlayer)
        {
            audiosource.Stop();
            audiosource.clip = (stopEngine_sfx);
            audiosource.PlayOneShot(stopEngine_sfx);
        }

        
    }
}
