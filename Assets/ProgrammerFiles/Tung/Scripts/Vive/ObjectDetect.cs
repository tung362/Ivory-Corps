using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetect : MonoBehaviour
{
    //The Grab component this hand is tracked with
    [HideInInspector]
    public Grab Grabber;
    [HideInInspector]
    public string StartingLayerName = "";

    void OnTriggerStay(Collider other)
    {
        if (Grabber == null) return;
        if (other.tag == "Lever" || other.tag == "Item")
        {
            if (Grabber.ObjectToGrab == null)
            {
                Grabber.ObjectToGrab = other.transform.root.gameObject;
                StartingLayerName = LayerMask.LayerToName(Grabber.ObjectToGrab.layer);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (Grabber == null) return;
        if (Grabber.ObjectToGrab == null) return;

        //Readd physics if there is any attacted
        if (GetComponent<Rigidbody>() != null) GetComponent<Rigidbody>().isKinematic = false;
        //Reapplies original layer to parent and childs
        Grabber.ObjectToGrab.layer = LayerMask.NameToLayer(StartingLayerName);
        ApplyLayerToChilds(Grabber.ObjectToGrab.transform, StartingLayerName);

        Grabber.ObjectToGrab = null;
    }

    void ApplyLayerToChilds(Transform TheGameObject, string LayerName)
    {
        foreach (Transform child in TheGameObject)
        {
            child.gameObject.layer = LayerMask.NameToLayer(LayerName);
            ApplyLayerToChilds(child, LayerName);
        }
    }
}
