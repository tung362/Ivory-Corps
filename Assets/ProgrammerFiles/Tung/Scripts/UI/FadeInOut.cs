using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FadeInOut : MonoBehaviour
{
    /*Settings*/
    public bool FadeIn = true;
    public Color FadeInColor;
    public Color FadeOutColor;
    public float Duration = 1;
    public float CallThreshold = 0.05f;

    /*Data*/
    private bool RunOnce = true;
    private float CurrentTimer = 0;


    /*Callable functions*/
    public UnityEvent OnFadeIn;
    public UnityEvent OnFadeOut;

    /*Required components*/
    private Image TheImage;

    void Start()
    {
        TheImage = GetComponent<Image>();
        if (FadeIn) RunOnce = true;
        else RunOnce = false;
    }
	
	void Update ()
    {
        FadeUpdate();
    }

    void FadeUpdate()
    {
        if (FadeIn)
        {
            if(RunOnce)
            {
                CurrentTimer += Time.deltaTime;
                if (CurrentTimer > Duration) CurrentTimer = Duration;
                float progress = CurrentTimer / Duration;
                TheImage.color = Color.Lerp(FadeOutColor, FadeInColor, progress);

                if (TheImage.color == FadeInColor)
                {
                    OnFadeIn.Invoke();
                    CurrentTimer = 0;
                    RunOnce = false;
                }
            }
        }
        else
        {
            if (!RunOnce)
            {
                CurrentTimer += Time.deltaTime;
                if (CurrentTimer > Duration) CurrentTimer = Duration;
                float progress = CurrentTimer / Duration;
                TheImage.color = Color.Lerp(FadeInColor, FadeOutColor, progress);

                if (TheImage.color == FadeOutColor)
                {
                    OnFadeOut.Invoke();
                    CurrentTimer = 0;
                    RunOnce = true;
                }
            }
        }
    }

    public void Toggle()
    {
        CurrentTimer = 0;
        FadeIn = !FadeIn;
        if (FadeIn) RunOnce = true;
        else RunOnce = false;
    }
}
