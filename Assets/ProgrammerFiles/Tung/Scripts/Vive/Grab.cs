using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public GameObject Hand;
    public GameObject ObjectToGrab;

    [HideInInspector]
    private StickController Stick;

    void Start ()
    {
        Stick = GetComponent<StickController>();
    }

	void Update ()
    {
        UpdateInput();
    }

    void UpdateInput()
    {
        //Start
        if (Stick.Controller.GetPressDown(Stick.GripyButton)) OnFirstPressed();
        //Held
        if (Stick.Controller.GetPress(Stick.GripyButton)) OnHeld();
        //End
        if (Stick.Controller.GetPressUp(Stick.GripyButton)) OnLetGo();
    }

    void OnFirstPressed()
    {

    }

    void OnHeld()
    {

    }

    void OnLetGo()
    {

    }
}
