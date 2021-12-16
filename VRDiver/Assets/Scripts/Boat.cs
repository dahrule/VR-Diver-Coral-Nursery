using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Boat : MonoBehaviour
{

    [SerializeField] Transform player; //The transform of the player.
    [SerializeField] int maxdistanceFromPlayer= 10; //the distance at which the boat starts following the player.
    
    [SerializeField] float interval=2f; //interval of time in seconds of the BoatRoutine coroutine.
    

    [Header("SFX audio clips")]
   [SerializeField] AudioClip runEngine_sfx;

    Vector2 playerhorizontalPosition;
    Vector2 boathorizontalPosition;
    float speed;

    AudioSource audiosource;
    

    void Awake()
    {
        if(player==null) player = GameObject.FindGameObjectWithTag("Player").transform;
        audiosource = GetComponent<AudioSource>();

    }
    // Start is called before the first frame update
    void Start()
    {
        speed = maxdistanceFromPlayer / runEngine_sfx.length;
        this.gameObject.transform.position = new Vector3(player.transform.position.x+10,20,player.transform.position.z);
        StartCoroutine(BoatRoutine());
    }

    // Update is called once per frame
    IEnumerator BoatRoutine()
    {
        while(true)
        {
            boathorizontalPosition = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.z);
            playerhorizontalPosition = new Vector2(player.transform.position.x, player.transform.position.z);
            //Get distance to player
            var distanceToPlayer = Vector2.Distance(boathorizontalPosition, playerhorizontalPosition);
            if (distanceToPlayer > maxdistanceFromPlayer)
            {
                Vector2 positionInPlane = Vector2.MoveTowards(boathorizontalPosition, playerhorizontalPosition, speed*interval);
                this.gameObject.transform.position = new Vector3(positionInPlane.x, gameObject.transform.position.y, positionInPlane.y);

                //Play start engine sfx.
                if (!audiosource.isPlaying)
                {
                    audiosource.PlayOneShot(runEngine_sfx);
                }
            }
            yield return new WaitForSeconds(interval);
        }
        

        
    }
}
