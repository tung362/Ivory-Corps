using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach to all item objects, used for enabling/disabling physics, changing layer masks, and keeping track of throw data
public class Item : TungDoesMathForYou
{
    /*Settings*/
    public string GrabbedLayer = "IgnoreHands";

    /*Data*/
    [HideInInspector]
    public string DefaultLayer = "Default";
    [HideInInspector]
    public bool IsDefault = true;
    [HideInInspector]
    public Vector3 ThrowForce = Vector3.zero;
    [HideInInspector]
    public bool DefaultToggle = true;
    [HideInInspector]
    public bool GrabbedToggle = true;

    /*Required components*/
    private Rigidbody TheRigidbody;

    void Start()
    {
        TheRigidbody = GetComponent<Rigidbody>();
        DefaultLayer = LayerMask.LayerToName(gameObject.layer);
    }

    void FixedUpdate()
    {
        UpdateGrabbedLayer();
        UpdateDefaultLayer();
    }

    void UpdateGrabbedLayer()
    {
        //Set to grabbed state
        if (!IsDefault && GrabbedToggle)
        {
            gameObject.layer = LayerMask.NameToLayer(GrabbedLayer);
            ApplyLayerToChilds(transform, GrabbedLayer);
            if (TheRigidbody != null) TheRigidbody.useGravity = false;
            DefaultToggle = true;
            GrabbedToggle = false;
        }
    }

    void UpdateDefaultLayer()
    {
        //Set to ungrabbed but still inside of item state
        if (IsDefault && DefaultToggle)
        {
            if (TheRigidbody != null && DefaultToggle)
            {
                TheRigidbody.useGravity = true;
                if(ThrowForce != Vector3.zero) TheRigidbody.AddForce(ThrowForce, ForceMode.Impulse);
                ThrowForce = Vector3.zero;
                GrabbedToggle = true;
                DefaultToggle = false;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Set to ungrabbed state
        if (other.transform.tag == "GrabZone" && IsDefault)
        {
            //Note Might need to change to item self track
            if(other.transform.root.GetComponent<ObjectDetect>().Grabber.ObjectToGrab == gameObject)
            {
                gameObject.layer = LayerMask.NameToLayer(DefaultLayer);
                ApplyLayerToChilds(transform, DefaultLayer);
                TheRigidbody.useGravity = true;
                if (ThrowForce != Vector3.zero) TheRigidbody.AddForce(ThrowForce, ForceMode.Impulse);
                ThrowForce = Vector3.zero;
            }
        }
    }
}
