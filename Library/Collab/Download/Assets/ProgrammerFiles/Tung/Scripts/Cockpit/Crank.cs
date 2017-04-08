using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Crank : MonoBehaviour
{
    /*Settings*/
    [Header("Settings")]
    public GameObject RotationSpot;
    public GameObject Handle;
    public float TickCrankEveryNumberOfSpins = 1;

    /*Data*/
    [HideInInspector]
    public float CurrentValue = 0;
    [HideInInspector]
    public float PreviousRotation = 0;
    private Vector3 StartingForward = Vector3.zero;

    /*Callable functions*/
    public UnityEvent OnToggled;

    void Start ()
    {
        StartingForward = RotationSpot.transform.forward;
    }
	
	void Update ()
    {
        float spinNumberToAngle = 360 * TickCrankEveryNumberOfSpins;

        float change = RotationSpot.transform.localEulerAngles.x - PreviousRotation;
        CurrentValue += change;
        PreviousRotation = RotationSpot.transform.localEulerAngles.x;

        //if (CurrentValue >= spinNumberToAngle)
        //{
        //    OnToggled.Invoke();
        //    CurrentValue = 0;
        //}

        PreviousRotation = RotationSpot.transform.localEulerAngles.x;
    }
}
