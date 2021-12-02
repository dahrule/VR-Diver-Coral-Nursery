using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Range(0, 5)] public int staghornGoal = 3;
    [Range(0, 5)] public int elkhornGoal = 2;

    [SerializeField] GameObject floatingScreenTaskComplete;
    [SerializeField] GameObject floatingScreenTimeEnds;

   

    private void Awake()
    {
        instance = this;
        CoralPlanting.OnPlantingActionComplete += CheckGoal;
       
    }

    public void CheckGoal(Dictionary<CoralTypes,int> coralsPlanted)
    {

        //test for task goal accomplishment
       
    }

}
