using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles all grabbing mechanics
public class Grab : MonoBehaviour
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
        if (ObjectToGrab == null) return true;
        if (Vector3.Distance(ObjectToGrab.transform.position, Hand.transform.position) > MaximumGrabDistance)
        {
            ObjectToGrab = null;
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
        //Reapplies original layer to parent and childs
        ObjectToGrab.layer = LayerMask.NameToLayer("IgnoreHands");
        ApplyLayerToChilds(ObjectToGrab.transform, "IgnoreHands");
        if (ObjectToGrab.GetComponent<Rigidbody>() != null) ObjectToGrab.GetComponent<Rigidbody>().useGravity = false;

        return retval;
    }

    bool OnHeld()
    {
        bool retval = true;

        //Remove physics if there is any attacted
        //if (ObjectToGrab.GetComponent<Rigidbody>() != null) ObjectToGrab.GetComponent<Rigidbody>().isKinematic = true;
        if (Hand.GetComponent<Rigidbody>() != null) Hand.GetComponent<Rigidbody>().isKinematic = true;

        if (ObjectToGrab.tag == "Lever")
        {
            Hand.transform.position = Vector3.MoveTowards(Hand.transform.position, ObjectToGrab.GetComponent<Lever>().Handle.transform.position, MoveSpeed * Time.fixedDeltaTime);
            Hand.transform.rotation = Quaternion.RotateTowards(Hand.transform.rotation, transform.rotation, RotationSpeed * Time.fixedDeltaTime);

            CalculateLeverDragValue();

            //We don't want it to follow the controller when grabbing a lever but instead follow the lever
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

        //Apply layer tag to parent and child
        ObjectToGrab.layer = LayerMask.NameToLayer("IgnoreHands");
        ApplyLayerToChilds(ObjectToGrab.transform, "IgnoreHands");

        if (ObjectToGrab.tag == "Item")
        {
            //Note that readding physics is handed in ObjectDetect script

            //Applies throw force and reenable physics if there is physics attacted
            if (ObjectToGrab.GetComponent<Rigidbody>() != null)
            {
                Vector3 force = (transform.position - PreviousPosition) / Time.fixedDeltaTime;
                //ObjectToGrab.GetComponent<Rigidbody>().isKinematic = false;
                ObjectToGrab.GetComponent<Rigidbody>().useGravity = true;
                ObjectToGrab.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
            }
        }
        return retval;
    }

    void UpdateHand(bool CanRun)
    {
        if (!CanRun) return;
        //If not grabbing anything return to following the controller
        Hand.transform.position = Vector3.MoveTowards(Hand.transform.position, transform.position, MoveSpeed * Time.fixedDeltaTime);
        Hand.transform.rotation = Quaternion.RotateTowards(Hand.transform.rotation, transform.rotation, RotationSpeed * Time.fixedDeltaTime);
    }

    void ApplyLayerToChilds(Transform TheGameObject, string LayerName)
    {
        foreach (Transform child in TheGameObject)
        {
            child.gameObject.layer = LayerMask.NameToLayer(LayerName);
            ApplyLayerToChilds(child, LayerName);
        }
    }

    void CalculateLeverDragValue()
    {
        //Calculations for lever pull/push
        float controllerDistance = Vector3.Distance(transform.position, ObjectToGrab.GetComponent<Lever>().DragEnd.transform.position);
        float totalDistance = Vector3.Distance(ObjectToGrab.GetComponent<Lever>().DragEnd.transform.position, ObjectToGrab.GetComponent<Lever>().DragStart.transform.position);
        float controllerDistanceReversed = Vector3.Distance(transform.position, ObjectToGrab.GetComponent<Lever>().DragStart.transform.position);

        //Gets rid of the extra distance
        float extraDistance = controllerDistanceReversed - controllerDistance;

        float result = (controllerDistance / totalDistance) - extraDistance;
        ObjectToGrab.GetComponent<Lever>().LeverValue = result;
    }
}
