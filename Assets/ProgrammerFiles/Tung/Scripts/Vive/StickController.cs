using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach this to controllers
public class StickController : TungDoesMathForYou
{
    //Get Input keys
    public Valve.VR.EVRButtonId GripyButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    public Valve.VR.EVRButtonId TriggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    public Valve.VR.EVRButtonId MenuButton = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu; //Top Button
    public Valve.VR.EVRButtonId TouchpadButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad; //Middle Button

    //Get the controller
    [HideInInspector]
    public SteamVR_Controller.Device Controller { get { return SteamVR_Controller.Input((int)TrackedObj.index); } }
    private SteamVR_TrackedObject TrackedObj;

    void Start()
    {
        TrackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {
        if (Controller == null)
        {
            Debug.Log("Controller is not initialized");
            return;
        }
    }
}
