using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public GameObject Hand;
    public GameObject ObjectToGrab;

    //Settings
    public float MaximumGrabDistance = 2;
    public float MoveSpeed = 10;
    public float RotationSpeed = 2000;

    public bool IsGrabbingSomething = false;

    [HideInInspector]
    private StickController Stick;

    void Start ()
    {
        Stick = GetComponent<StickController>();
    }

	void FixedUpdate()
    {
        if (ObjectToGrab != null) return;
        if (Vector3.Distance(ObjectToGrab.transform.position, Hand.transform.position) > MaximumGrabDistance) return;

        UpdateHand(UpdateInput());
    }

    bool UpdateInput()
    {
        bool retval = true;
        //Start
        if (Stick.Controller.GetPressDown(Stick.GripyButton)) retval = OnFirstPressed();
        //Held
        if (Stick.Controller.GetPress(Stick.GripyButton)) retval = OnHeld();
        //End
        if (Stick.Controller.GetPressUp(Stick.GripyButton)) retval = OnLetGo();
        return retval;
    }

    void UpdateHand(bool CanRun)
    {
        if (!CanRun) return;
        Hand.transform.position = Vector3.MoveTowards(Hand.transform.position, transform.position, MoveSpeed * Time.fixedDeltaTime);
        Hand.transform.rotation = Quaternion.RotateTowards(Hand.transform.rotation, transform.rotation, RotationSpeed * Time.fixedDeltaTime);
    }

    bool OnFirstPressed()
    {
        bool retval = true;
        if (ObjectToGrab.tag == "Lever")
        {

        }
        else if (ObjectToGrab.tag == "Item")
        {
            
        }
        return retval;
    }

    bool OnHeld()
    {
        bool retval = true;
        if (ObjectToGrab.tag == "Lever")
        {
            retval = false;
        }
        else if (ObjectToGrab.tag == "Item")
        {
            ObjectToGrab.transform.position = Vector3.MoveTowards(ObjectToGrab.transform.position, Hand.transform.position, MoveSpeed * Time.fixedDeltaTime);
            ObjectToGrab.transform.rotation = Quaternion.RotateTowards(ObjectToGrab.transform.rotation, transform.rotation, RotationSpeed * Time.fixedDeltaTime);
        }
        return retval;
    }

    bool OnLetGo()
    {
        bool retval = true;
        if (ObjectToGrab.tag == "Lever")
        {

        }
        else if (ObjectToGrab.tag == "Item")
        {
        }

        ObjectToGrab = null;
        return retval;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Lever" || other.tag == "Item") ObjectToGrab = other.transform.gameObject;
    }
}
