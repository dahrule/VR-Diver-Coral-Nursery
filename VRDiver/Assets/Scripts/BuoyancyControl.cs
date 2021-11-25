using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BuoyancyControl : MonoBehaviour
{
    [Tooltip("The total weight of the avatar")]
    [SerializeField] float weight;

    float initialVolume;
    float updatedVolume;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //Set neutral buoyancy.
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
