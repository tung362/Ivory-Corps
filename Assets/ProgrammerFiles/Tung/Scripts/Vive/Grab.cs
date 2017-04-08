using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles all grabbing mechanics
public class Grab : TungDoesMathForYou
{
    /*Settings*/
    public GameObject Hand;
    public float MaximumGrabDistance = 2;
    public float MoveSpeed = 10;
    public float RotationSpeed = 2000;

    /*Data*/
    [HideInInspector]
    public GameObject ObjectToGrab;
    private bool StartupCheck = true;
    private Vector3 PreviousPosition = Vector3.zero;//Used for tracking velocity to throw

    /*Controller*/
    [HideInInspector]
    private StickController Stick;

    void Start ()
    {
        Stick = GetComponent<StickController>();
        Hand.GetComponent<ObjectDetect>().Grabber = this;
    }

	void FixedUpdate()
    {
        OnIniltalStart();
        UpdateHand(UpdateInput());
        PreviousPosition = transform.position;
    }

    void OnIniltalStart()
    {
        //Set position before start of game so that the hand doesn't knock everything to space
        if (StartupCheck)
        {
            if (GetComponent<SteamVR_TrackedObject>().isValid)
            {
                Hand.transform.position = transform.position;
                StartupCheck = false;
            }
        }
    }

    bool UpdateInput()
    {
        bool retval = true;

        //Checks
        if (ObjectToGrab == null)
        {
            if (Hand.GetComponent<Rigidbody>() != null && !Hand.GetComponent<ObjectDetect>().TouchingButton) Hand.GetComponent<Rigidbody>().isKinematic = false;
            return true;
        }
        if (Vector3.Distance(ObjectToGrab.transform.position, Hand.transform.position) > MaximumGrabDistance)
        {
            ObjectToGrab = null;
            if (Hand.GetComponent<Rigidbody>() != null && !Hand.GetComponent<ObjectDetect>().TouchingButton) Hand.GetComponent<Rigidbody>().isKinematic = false;
            return true;
        }

        //Passed all checks
        //Start
        if (Stick.Controller.GetPressDown(Stick.GripyButton)) retval = OnFirstPressed();
        //Held
        if (Stick.Controller.GetPress(Stick.GripyButton)) retval = OnHeld();
        //End
        if (Stick.Controller.GetPressUp(Stick.GripyButton)) retval = OnLetGo();
        return retval;
    }

    bool OnFirstPressed()
    {
        bool retval = true;
        if (ObjectToGrab.tag == "Item")
        {
            //Remove physics if there is any attacted
            ObjectToGrab.GetComponent<Item>().IsDefault = false;
            ObjectToGrab.GetComponent<Item>().GrabbedToggle = true;
            if (Hand.GetComponent<Rigidbody>() != null) Hand.GetComponent<Rigidbody>().isKinematic = true;
        }
        return retval;
    }

    bool OnHeld()
    {
        bool retval = true;

        if (ObjectToGrab.tag == "Lever")
        {
            Hand.transform.position = Vector3.MoveTowards(Hand.transform.position, ObjectToGrab.GetComponent<Lever>().Handle.transform.position, MoveSpeed * Time.fixedDeltaTime);
            Hand.transform.rotation = Quaternion.RotateTowards(Hand.transform.rotation, transform.rotation, RotationSpeed * Time.fixedDeltaTime);

            CalculateLeverDragValue();

            //We don't want it to follow the controller when grabbing a lever but instead follow the lever
            retval = false;
        }
        else if (ObjectToGrab.tag == "Crank")
        {
            //To do: maybe add up and forward together but block off one of the axis
            //Try rotatearound
            Hand.transform.position = Vector3.MoveTowards(Hand.transform.position, ObjectToGrab.GetComponent<Lever>().Handle.transform.position, MoveSpeed * Time.fixedDeltaTime);
            Hand.transform.rotation = Quaternion.RotateTowards(Hand.transform.rotation, transform.rotation, RotationSpeed * Time.fixedDeltaTime);

            CalculateCrankValue();

            //We don't want it to follow the controller when grabbing a crank but instead follow the crank
            retval = false;
        }
        else if (ObjectToGrab.tag == "Item")
        {
            OnFirstPressed();
            ObjectToGrab.transform.position = Vector3.MoveTowards(ObjectToGrab.transform.position, Hand.transform.position, MoveSpeed * Time.fixedDeltaTime);
            ObjectToGrab.transform.rotation = Quaternion.RotateTowards(ObjectToGrab.transform.rotation, transform.rotation, RotationSpeed * Time.fixedDeltaTime);
        }
        return retval;
    }

    bool OnLetGo()
    {
        bool retval = true;

        if (ObjectToGrab.tag == "Item")
        {
            ObjectToGrab.GetComponent<Item>().IsDefault = true;
            //Applies throw force
            ObjectToGrab.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ObjectToGrab.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            Vector3 force = (transform.position - PreviousPosition) / Time.fixedDeltaTime;
            ObjectToGrab.GetComponent<Item>().ThrowForce = force;
            ObjectToGrab.GetComponent<Item>().DefaultToggle = true;

            if (Hand.GetComponent<Rigidbody>() != null) Hand.GetComponent<Rigidbody>().isKinematic = false;
        }
        ObjectToGrab = null;
        return retval;
    }

    void UpdateHand(bool CanRun)
    {
        if (!CanRun) return;
        //If not grabbing anything return to following the controller
        Hand.transform.position = Vector3.MoveTowards(Hand.transform.position, transform.position, MoveSpeed * Time.fixedDeltaTime);
        Hand.transform.rotation = Quaternion.RotateTowards(Hand.transform.rotation, transform.rotation, RotationSpeed * Time.fixedDeltaTime);
    }

    void CalculateLeverDragValue()
    {
        Lever theLever = ObjectToGrab.GetComponent<Lever>();
        //Calculations for lever pull/push
        float controllerDistance = Vector3.Distance(transform.position, theLever.DragEnd.transform.position);
        float totalDistance = Vector3.Distance(theLever.DragEnd.transform.position, theLever.DragStart.transform.position);
        float controllerDistanceReversed = Vector3.Distance(transform.position, theLever.DragStart.transform.position);

        //Gets rid of the extra distance
        float extraDistance = controllerDistanceReversed - controllerDistance;

        float result = (controllerDistance / totalDistance) - extraDistance;
        ObjectToGrab.GetComponent<Lever>().LeverValue = result;
    }

    void CalculateCrankValue()
    {
        Crank theCrank = ObjectToGrab.GetComponent<Crank>();
        Vector3 handLocalPosition = ObjectToGrab.transform.InverseTransformPoint(transform.position); //Hand its tracking (in this case the controller)
        Vector3 handLocalDirection = (new Vector3(0, handLocalPosition.y, handLocalPosition.z) - new Vector3(0, theCrank.RotationSpot.transform.localPosition.y, theCrank.RotationSpot.transform.localPosition.z)).normalized;
        float handAngle = Vector3.Angle(theCrank.StartingForward, handLocalDirection);
        Vector3 handCross = Vector3.Cross(theCrank.StartingForward, ObjectToGrab.transform.position);
        float handDot = Vector3.Dot(handCross, handLocalDirection);

        if (handDot < 0) handAngle = 360 - handAngle;

        theCrank.RotationSpot.transform.localRotation = Quaternion.Euler(handAngle, 0, 0);


        //Crank theCrank = ObjectToGrab.GetComponent<Crank>();
        //Vector3 handLocalPosition = ObjectToGrab.transform.InverseTransformPoint(Hand.transform.position);
        //Vector3 handLocalDirection = (new Vector3(0, handLocalPosition.y, handLocalPosition.z) - new Vector3(0, theCrank.RotationSpot.transform.localPosition.y, theCrank.RotationSpot.transform.localPosition.z)).normalized;
        //float handAngle = Vector3.Angle(theCrank.StartingForward, handLocalDirection);
        //Vector3 handCross = Vector3.Cross(theCrank.StartingForward, ObjectToGrab.transform.position);
        //float handDot = Vector3.Dot(handCross, handLocalDirection);

        //if (handDot < 0) handAngle = 360 - handAngle;

        //theCrank.RotationSpot.transform.localRotation = Quaternion.Euler(handAngle, 0, 0);
    }
}
