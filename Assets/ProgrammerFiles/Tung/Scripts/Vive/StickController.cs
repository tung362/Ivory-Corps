using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach this to controllers
public class StickController : MonoBehaviour
{
    //Get Input keys
    public Valve.VR.EVRButtonId GripyButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    public Valve.VR.EVRButtonId TriggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    //Untested
    public Valve.VR.EVRButtonId MenuButton = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;
    public Valve.VR.EVRButtonId AButton = Valve.VR.EVRButtonId.k_EButton_A;
    public Valve.VR.EVRButtonId A0Button = Valve.VR.EVRButtonId.k_EButton_Axis0;
    public Valve.VR.EVRButtonId A1Button = Valve.VR.EVRButtonId.k_EButton_Axis1;
    public Valve.VR.EVRButtonId A2Button = Valve.VR.EVRButtonId.k_EButton_Axis2;
    public Valve.VR.EVRButtonId A3Button = Valve.VR.EVRButtonId.k_EButton_Axis3;
    public Valve.VR.EVRButtonId A4Button = Valve.VR.EVRButtonId.k_EButton_Axis4;
    public Valve.VR.EVRButtonId BackButton = Valve.VR.EVRButtonId.k_EButton_Dashboard_Back;
    public Valve.VR.EVRButtonId DDownButton = Valve.VR.EVRButtonId.k_EButton_DPad_Down;
    public Valve.VR.EVRButtonId DLeftButton = Valve.VR.EVRButtonId.k_EButton_DPad_Left;
    public Valve.VR.EVRButtonId DRightButton = Valve.VR.EVRButtonId.k_EButton_DPad_Right;
    public Valve.VR.EVRButtonId DUpButton = Valve.VR.EVRButtonId.k_EButton_DPad_Up;
    public Valve.VR.EVRButtonId MaxButton = Valve.VR.EVRButtonId.k_EButton_Max;
    public Valve.VR.EVRButtonId TouchpadButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
    public Valve.VR.EVRButtonId SystemButton = Valve.VR.EVRButtonId.k_EButton_System;

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

        ManageInputs();
    }

    //Interactions
    void ManageInputs()
    {
        //DebugInputs();
    }

    //Test a few buttons
    void DebugInputs()
    {
        if (Controller.GetPressDown(GripyButton)) print("Grip Button Down");
        if (Controller.GetPressUp(GripyButton)) print("Grip Button Up");
        if (Controller.GetPress(GripyButton)) print("Grip Button Pressed"); //Held

        if (Controller.GetPressDown(TriggerButton)) print("Trigger Button Down");
        if (Controller.GetPressUp(TriggerButton)) print("Trigger Button Up");
        if (Controller.GetPress(TriggerButton)) print("Trigger Button Pressed"); //held

        //Untested
        if (Controller.GetPressDown(MenuButton)) print("MenuButton");
        if (Controller.GetPressDown(AButton)) print("AButton");
        if (Controller.GetPressDown(A0Button)) print("A0Button");
        if (Controller.GetPressDown(A1Button)) print("A1Button");
        if (Controller.GetPressDown(A2Button)) print("A2Button");
        if (Controller.GetPressDown(A3Button)) print("A3Button");
        if (Controller.GetPressDown(A4Button)) print("A4Button");
        if (Controller.GetPressDown(BackButton)) print("BackButton");
        if (Controller.GetPressDown(DDownButton)) print("DDownButton");
        if (Controller.GetPressDown(DLeftButton)) print("DLeftButton");
        if (Controller.GetPressDown(DRightButton)) print("DRightButton");
        if (Controller.GetPressDown(DUpButton)) print("DUpButton");
        if (Controller.GetPressDown(MaxButton)) print("MaxButton");
        if (Controller.GetPressDown(TouchpadButton)) print("TouchpadButton");
        if (Controller.GetPressDown(SystemButton)) print("SystemButton");
    }
}
