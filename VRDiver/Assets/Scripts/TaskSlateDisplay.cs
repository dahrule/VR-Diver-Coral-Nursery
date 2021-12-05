using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Displays the canvas elements of the TaskSlate gameobject and updates its values.
public class TaskSlateDisplay:MonoBehaviour 
{
    [SerializeField] TextMeshProUGUI staghornCount;
    [SerializeField] TextMeshProUGUI elkhornCount;

    [SerializeField] TextMeshProUGUI staghornGoal;
    [SerializeField] TextMeshProUGUI elkhornGoal;

    [SerializeField] TextMeshProUGUI totalGoal;

    private void Awake()
    {
        CoralPlanting.OnPlantingActionComplete += UpdateDisplayValues; //resgister the response to the  OnPlantingActionComplete event.

    }
    private void Start()
    {
        InitializeDisplayValues();
    }

    private void InitializeDisplayValues()
    {
        staghornGoal.text = GameManager.instance.staghornGoal.ToString();
        elkhornGoal.text = GameManager.instance.elkhornGoal.ToString();
        totalGoal.text = (GameManager.instance.staghornGoal + GameManager.instance.elkhornGoal).ToString();
    }

    public void UpdateDisplayValues(Dictionary<CoralTypes, int> corals)
    {
        staghornCount.text = corals[CoralTypes.Staghorn].ToString();
        elkhornCount.text = corals[CoralTypes.Elkhorn].ToString();
    }
}
