using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Handles the crank functions
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
    private Vector3 CurrentForward = Vector3.zero;
    private Vector3 PreviousForward = Vector3.zero;
    [HideInInspector]
    public Vector3 StartingForward = Vector3.zero;

    /*Callable functions*/
    public UnityEvent OnToggled;

    void Start ()
    {
        Vector3 localHandlePosition = transform.InverseTransformPoint(Handle.transform.position);
        Vector3 localHandleDirection = (new Vector3(0, localHandlePosition.x, localHandlePosition.z)).normalized;
        CurrentForward = localHandleDirection;
        PreviousForward = CurrentForward;
        StartingForward = localHandleDirection;
    }

	void Update ()
    {
        UpdateInput();
    }

    //Calculate's the crank's current progress towards the goal of amount of spins
    void UpdateInput()
    {
        float spinNumberToAngle = 360 * TickCrankEveryNumberOfSpins;
        Vector3 localHandlePosition = transform.InverseTransformPoint(Handle.transform.position);
        Vector3 localHandleDirection = (new Vector3(0, localHandlePosition.y, localHandlePosition.z)).normalized;
        CurrentForward = localHandleDirection;

        float angle = Vector3.Angle(CurrentForward, PreviousForward);
        Vector3 cross = Vector3.Cross(transform.position, CurrentForward);
        float dot = Vector3.Dot(cross, PreviousForward);

        if (dot > 0) CurrentValue += angle;
        else CurrentValue -= angle;

        PreviousForward = CurrentForward;

        if (CurrentValue >= spinNumberToAngle) OnToggled.Invoke();

        CurrentValue = CurrentValue % spinNumberToAngle;
    }
}
