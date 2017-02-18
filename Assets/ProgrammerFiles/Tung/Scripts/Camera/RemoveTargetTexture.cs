using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//Removes the target texture if there is no authority
public class RemoveTargetTexture : MonoBehaviour
{
    /*Required components*/
    private ManagerTracker Tracker;
    private Camera TheCamera;

    void Start ()
    {
        Tracker = FindObjectOfType<ManagerTracker>();
        TheCamera = GetComponent<Camera>();
    }

    void Update()
    {
        if (!transform.root.GetComponent<NetworkIdentity>().hasAuthority) Destroy(TheCamera.gameObject);
        Destroy(this);
    }
}
