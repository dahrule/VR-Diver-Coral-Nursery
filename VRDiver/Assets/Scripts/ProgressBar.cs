using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [Tooltip("An image Component of Type Filled")]
    [SerializeField] Image progressBar;

    //[Tooltip("Speed at which the progress bar fills.")]
    //[SerializeField] float speed = 10f;

    [Tooltip("Starting fill percentage")]
    [Range(0, 1)]
    [SerializeField] float startingFillAmount = 0f;

    float currentAmount;

    //set the fillamount from an external class.
    public float FillAmount
    {
        set { progressBar.fillAmount = value; }
    }


    private void Start()
    {
        currentAmount = startingFillAmount;
        progressBar.fillAmount = currentAmount;


    }

    /*// Update is called once per frame
    void Update()
    {
        //If progress bar is not filled, fill it at the speed rate.
        if(currentAmount<100)
        {
            currentAmount += speed * Time.deltaTime;
        }

        progressBar.fillAmount = currentAmount / 100;
    }*/
}
