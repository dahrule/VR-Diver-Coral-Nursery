using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

//Manages the  GameOver screens and level values, such as task goals.
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game level variables")]
    [Tooltip("How many corals of each type must the player plant to achieve the goal")]
    [Range(0, 5)] public int staghornGoal = 2; 
    [Range(0, 5)] public int elkhornGoal = 1;
    [Tooltip("Maximum time to complete task before game ends")]
    [SerializeField] int minutesToCompleteTask = 2;
    [Tooltip("No deco starting time (minutes)")]
    [SerializeField] int startTime = 5;
    [Tooltip("Safety margin for No-deco time. Time when diver should start surfacing (minutes)")]
    [SerializeField] int timeLimit = 1;

    public  bool playModeActive = true;//play mode is used to control the time at which game controls are activated.
    public bool PlayModeActive { get { return instance.playModeActive; } }

    [Tooltip("Screen appearing when the task is achieved")]
    [SerializeField] GameObject floatingScreenTaskComplete;
    [Tooltip("Screen appearing when the decompression time reaches the limit.")]
    [SerializeField] GameObject floatingScreenTimeEnds;

   

    private void Awake()
    {
        instance = this;
        CoralPlanting.OnPlantingActionComplete += CheckCompletedGoals; //register the response to the  OnPlantingActionComplete event.
        NoDecoTimer.OnNoDecoTimeOver += GameOver; //register the response to the OnNoDecoTimeOver event.
        

        //Disable endgame screens.
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
            floatingScreenTaskComplete.SetActive(true);
            playModeActive = false;
        }

    }

    private void GameOver()
    {
        floatingScreenTimeEnds.SetActive(true);
        playModeActive = false;
    }
    
    //Call on a Button click.
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

}
