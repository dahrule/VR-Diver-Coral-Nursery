using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Slate : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI staghornCount;
    [SerializeField] TextMeshProUGUI elkhornCount;

    [SerializeField] TextMeshProUGUI staghornGoal;
    [SerializeField] TextMeshProUGUI elkhornGoal;

    [SerializeField] TextMeshProUGUI totalGoal;

    private void Awake()
    {
        CoralPlanting.OnPlantingActionComplete += UpdateDisplayValues;
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
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
