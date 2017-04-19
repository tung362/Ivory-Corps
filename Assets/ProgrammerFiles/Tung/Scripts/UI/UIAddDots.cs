using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Adds 1-3 dots after a text string and transitions between them, used for loading screnes
public class UIAddDots : MonoBehaviour
{
    /*Setting*/
    public float DotDelay = 0.5f;

    /*Data*/
    private float DotTimer = 0;
    private int CurrentStage = 0; //1 = ., 2 = .., 3 = ...
    private string StartingText = "";
    private bool Run = true;

    /*Required components*/
    private Text TheText;

    void Start ()
    {
        TheText = GetComponent<Text>();
        StartingText = TheText.text;

    }
	
	void Update ()
    {
        UpdateDots();
    }

    void UpdateDots()
    {
        //Timer
        DotTimer += Time.deltaTime;
        if(DotTimer >= DotDelay)
        {
            CurrentStage += 1;
            DotTimer = 0;
        }

        //Checks
        if (CurrentStage > 3) CurrentStage = 0;
        if (CurrentStage < 0) CurrentStage = 3;

        //Apply to text
        if (CurrentStage == 0) TheText.text = StartingText;
        else if (CurrentStage == 1) TheText.text = StartingText + ".";
        else if (CurrentStage == 2) TheText.text = StartingText + "..";
        else if (CurrentStage == 3) TheText.text = StartingText + "...";
    }

    //Can be called on by other scripts to stop or start the script
    public void Toggle()
    {
        Run = !Run;
    }
}
