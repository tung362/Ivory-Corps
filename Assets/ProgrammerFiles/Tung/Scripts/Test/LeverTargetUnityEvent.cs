using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class LeverTargetUnityEvent : NetworkBehaviour
{
    /*Settings*/
    public string TargetedLever; //Make sure every lever is named differently
    public UnityEvent[] LeverStates;

    /*Required components*/
    private ManagerTracker Tracker;
    private MovementTest TheMovementTest;

    void Start ()
    {
		Tracker = FindObjectOfType<ManagerTracker>();
        TheMovementTest = GetComponent<MovementTest>();
    }
	
	void Update ()
    {
        if (!hasAuthority) return;
        for(int i = 0; i < LeverStates.Length; ++i)
        {
            if(i == GameObject.Find(TargetedLever).GetComponent<Lever>().CurrentState)
            {
                LeverStates[i].Invoke();
                return;
            }
        }
    }
}
