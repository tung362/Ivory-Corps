using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles grabbing physics and buttons for the hands
public class ObjectDetect : TungDoesMathForYou
{
    //The Grab component this hand is tracked with
    [HideInInspector]
    public Grab Grabber;

    [HideInInspector]
    public bool TouchingButton = false;

    void OnTriggerStay(Collider other)
    {
        if (Grabber == null) return;
        if (other.tag == "Lever" || other.tag == "Item")
        {
            if (Grabber.ObjectToGrab == null) Grabber.ObjectToGrab = other.transform.root.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (Grabber == null) return;
        if (other.tag == "Lever" || other.tag == "Item")
        {
            if (Grabber.ObjectToGrab != null)
            {
                if (Grabber.ObjectToGrab == other.transform.root.gameObject)
                {
                    if(other.tag == "Item")
                    {
                        Grabber.ObjectToGrab.GetComponent<Item>().IsDefault = true;
                        Grabber.ObjectToGrab.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        Grabber.ObjectToGrab.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                        Grabber.ObjectToGrab.GetComponent<Item>().DefaultToggle = true;
                    }
                    Grabber.ObjectToGrab = null;
                }
            }
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (other.collider.transform.gameObject.layer == LayerMask.NameToLayer("Buttons"))
        {
            GetComponent<Rigidbody>().isKinematic = true;
            TouchingButton = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.transform.gameObject.layer == LayerMask.NameToLayer("Buttons"))
        {
            GetComponent<Rigidbody>().isKinematic = false;
            TouchingButton = false;
        }
    }
}
