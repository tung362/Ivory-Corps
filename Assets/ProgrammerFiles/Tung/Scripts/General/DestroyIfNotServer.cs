using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DestroyIfNotServer : TungDoesNetworkingForyou
{
	void Start ()
    {
        if (!isServer) Destroy(gameObject);
        Destroy(this);
	}
}
