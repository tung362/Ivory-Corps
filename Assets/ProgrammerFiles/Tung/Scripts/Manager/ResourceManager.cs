using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//Keeps track of server data
public class ResourceManager : TungDoesNetworkingForyou
{
    /*Required components*/
    private ManagerTracker Tracker;

    void Start()
    {
        Tracker = FindObjectOfType<ManagerTracker>();
    }


}
