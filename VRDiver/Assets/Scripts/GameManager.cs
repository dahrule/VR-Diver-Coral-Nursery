using System.Collections.Generic;
using UnityEngine;

//Manages the  GameOver screens and level values, such as task goals and time to complete task.
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game level variables")]
    [Tooltip("How many corals of each type must the player plant to achieve the goal")]
    [Range(0, 5)] public int staghornGoal = 2; 
    [Range(0, 5)] public int elkhornGoal = 1;
    [Tooltip("Time to complete task before game ends")]
    [SerializeField] int minutesToCompleteTask = 6;

    private bool playModeActive = true;//play mode is used to control when game controls are active, they become inactive when gameover screens appear.
    public bool PlayModeActive { get { return instance.playModeActive; } }

    #region References to Game objects
    [SerializeField] NoDecoTimer timer;
    [SerializeField] SoundAlert diveComputerAlarm;

    [Tooltip("Screen appearing when the task is achieved")]
    [SerializeField] GameObject floatingScreenTaskComplete;
    [Tooltip("Screen appearing when the time to complete task is over.")]
    [SerializeField] GameObject floatingScreenTimeEnds;
    #endregion



    private void Awake()
    {
        //Get references to variables.
        instance = this;

        //register responses to events.
        CoralPlanting.OnPlantingActionComplete += CheckCompletedGoals; //register the response to the  OnPlantingActionComplete event.
        SoundAlert.OnTimeOver += GameOver;
        
        //Set the time on the NoDecoTimer script.
        timer.TimeLeftMin = minutesToCompleteTask; //set the timer inside the 
        

    }
    private void Start()
    {
       
        EnterPlayMode();
    }

    private void EnterPlayMode()
    {
        //Disable endgame screens && .
        floatingScreenTimeEnds.SetActive(false);
        floatingScreenTaskComplete.SetActive(false);
        playModeActive = true;
    }

    private void CheckCompletedGoals(Dictionary<CoralTypes, int> coralsPlanted)
    {
        bool goalA = coralsPlanted[CoralTypes.Staghorn] == staghornGoal;
        bool goalB = coralsPlanted[CoralTypes.Elkhorn] == elkhornGoal;

        if(goalA || goalB) //Play check mark sound.

        //When all goals are complete, activate the floatingScreenTaskComplete panel.
        if (goalA && goalB)
        {
            diveComputerAlarm.enabled = false; //disable dive computer alarm.
            floatingScreenTaskComplete.SetActive(true);
            playModeActive = false;
        }

    }


    private void GameOver()
    {
        floatingScreenTimeEnds.SetActive(true);
        playModeActive = false;
    }
    
    //Call on a Button click on the gameover screens.
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

}
