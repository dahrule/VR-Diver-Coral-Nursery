using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Depth sensing functionality. This class is referenced by other objects requiring an updated depth value (eg. dive computer for displaying or charater for buoyancy calculation.
public class DepthSensor : MonoBehaviour
{
    [Tooltip("Reference point to measure depth (m).")]
    [SerializeField] Transform surface;

    [Tooltip("Time update interval in seconds in which depth is measured relative to the surface")]
    [SerializeField] float updateInterval = 1f; 

    private IEnumerator depthSense; //variable to store the coroutine in charge of measuring depth in the -sensingTime- interval.

    private float depth; //stores the updated depth value.
    private float maxdepth; //stores the max depth reached.
    public float Depth { get { return depth; } }

    private Dictionary<string, float> depthInfo = new Dictionary<string, float>();

    public static event Action<Dictionary<string,float>> OnDepthChange; //event that notifies when depth has significantly changed and returns the updated depth value.

   
    #region BuiltInMethods

    //Initializes the coroutine variable when the script instance is being loaded. Note that the coroutine runs later when the object is enabled.
    private void Awake()
    {
        //Preventive code: Handle case if no surface object is given.
        if (surface == null) surface = GameObject.Find("Surface").transform;
       
        depthSense = DepthSenseRoutine(updateInterval);//assign coroutine to this variable.
    }

    private void Start()
    {
        //At start maxdepth is always equal to depth.
        maxdepth = depth;
    }

    //When the object containing this script becomes enabled and active, the coroutine -depth sense- starts runnnig.
    private void OnEnable()
    {
        StartCoroutine(depthSense);
    }
    private void OnDisable()
    {
        StopCoroutine(depthSense);
    }
    #endregion


    #region CustomMethods

    //Coroutine that updates the depth value. Every -sensingTime- seconds it calculates the distance from this gameobject to the surface reference point.
    private IEnumerator DepthSenseRoutine(float interval)
    {
        float temp_previousDepth=depth; //temporal depth value to compare with updated value and determine if it has changed.
        while (true)
        {
            //Get the vertical distance from player to surface.
            depth = Mathf.Abs(this.transform.position.y-surface.position.y);

            //Update maxdepth
            if (depth > maxdepth) maxdepth = depth;
            
            // TO CHECK: Probably is not neccesary to do the following check condition....
            //OnDepthChange is only invoked if depth has significantly changed (ie. first decimal after point changes). This is to reduce the total number of event calls.
            depth = RoundtoTens(depth);
            maxdepth=RoundtoTens(maxdepth);

            if (temp_previousDepth != depth)
            {
                //Encapsulate depth info. into dictionary.
                depthInfo["depth"] = depth;
                depthInfo["maxdepth"] = maxdepth;

                OnDepthChange?.Invoke(depthInfo);//broadcast the new depth values.
                
            }
            yield return new WaitForSeconds(interval);
        }
    }

    //Rounds a float number to the tens.
    private float RoundtoTens(float number)
    {
        return float.Parse(number.ToString("F1"));
    }
    #endregion
}
