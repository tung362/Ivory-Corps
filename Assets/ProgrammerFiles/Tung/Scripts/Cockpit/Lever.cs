using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//[ExecuteInEditMode]
public class Lever : MonoBehaviour
{
    //Settings
    //1 = Rotational Lever, 2 = Push/Pull Lever
    public int LeverType = 1;
    [Range(0.0f, 1.0f)]
    public float LeverValue = 0.5f;
    public Vector2 TransitionRange = new Vector2(-62.29f, 62.29f);
    public bool IsClunky = true;
    public float ClunkyTransitionSpeed = 200;

    //Callable functions
    public UnityEvent[] OnToggled;

    private Vector3 StartingRotation = Vector3.zero;
    private Vector3 StartingPosition = Vector3.zero;

    void Start()
    {
        StartingRotation = transform.localEulerAngles;
        StartingPosition = transform.localPosition;
    }

    void Update()
    {
        if(LeverType == 1) UpdateRotation();
        if (LeverType == 2) UpdateTranslation();
        UpdateInput();
    }

    //Rotates the crank depending on the number of lever states
    void UpdateRotation()
    {
        Quaternion MinimumRotation = Quaternion.Euler(TransitionRange.x, StartingRotation.y, StartingRotation.z);
        Quaternion MaximumRotation = Quaternion.Euler(TransitionRange.y, StartingRotation.y, StartingRotation.z);

        if (IsClunky)
        {
            float PreviousRotation = 0;
            float PreviousLimit = 0;
            for(int i = 0; i < OnToggled.Length; ++i)
            {
                PreviousRotation = i / (OnToggled.Length - 1.0f);
                float progress = transform.eulerAngles.x / TransitionRange.y;
                if (i == 0)
                {
                    Quaternion Goal = Quaternion.Lerp(MinimumRotation, MaximumRotation, 0);
                    if (LeverValue >= PreviousLimit && LeverValue <= (i + 1.0f) / OnToggled.Length) transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Goal, ClunkyTransitionSpeed * Time.deltaTime);
                }
                else
                {
                    Quaternion Goal = Quaternion.Lerp(MinimumRotation, MaximumRotation, PreviousRotation);
                    if (LeverValue > PreviousLimit && LeverValue <= (i + 1.0f) / OnToggled.Length) transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Goal, ClunkyTransitionSpeed * Time.deltaTime);
                }
                PreviousLimit = (i + 1.0f) / OnToggled.Length;
            }
        }
        else transform.localRotation = Quaternion.Lerp(MinimumRotation, MaximumRotation, LeverValue);
    }

    void UpdateTranslation()
    {
        Vector3 MinimumPosition = new Vector3(StartingPosition.x, StartingPosition.y, TransitionRange.x);
        Vector3 MaximumPosition = new Vector3(StartingPosition.x, StartingPosition.y, TransitionRange.y);

        if (IsClunky)
        {
            float PreviousPosition = 0;
            float PreviousLimit = 0;
            for (int i = 0; i < OnToggled.Length; ++i)
            {
                PreviousPosition = i / (OnToggled.Length - 1.0f);
                float progress = transform.eulerAngles.x / TransitionRange.y;
                if (i == 0)
                {
                    Vector3 Goal = Vector3.Lerp(MinimumPosition, MaximumPosition, 0);
                    if (LeverValue >= PreviousLimit && LeverValue <= (i + 1.0f) / OnToggled.Length) transform.localPosition = Vector3.MoveTowards(transform.localPosition, Goal, ClunkyTransitionSpeed * Time.deltaTime);
                }
                else
                {
                    Vector3 Goal = Vector3.Lerp(MinimumPosition, MaximumPosition, PreviousPosition);
                    if (LeverValue > PreviousLimit && LeverValue <= (i + 1.0f) / OnToggled.Length) transform.localPosition = Vector3.MoveTowards(transform.localPosition, Goal, ClunkyTransitionSpeed * Time.deltaTime);
                }
                PreviousLimit = (i + 1.0f) / OnToggled.Length;
            }
        }
        else transform.localPosition = Vector3.Lerp(MinimumPosition, MaximumPosition, LeverValue);
    }

    //Invokes a function depending on which state the lever is on
    void UpdateInput()
    {
        float PreviousLimit = 0;
        for (int i = 0; i < OnToggled.Length; ++i)
        {
            if (i == 0)
            {
                if (LeverValue >= PreviousLimit && LeverValue <= (i + 1.0f) / OnToggled.Length) OnToggled[i].Invoke();
            }
            else
            {
                if (LeverValue > PreviousLimit && LeverValue <= (i + 1.0f) / OnToggled.Length) OnToggled[i].Invoke();
            }
            PreviousLimit = (i + 1.0f) / OnToggled.Length;
        }
    }
}
