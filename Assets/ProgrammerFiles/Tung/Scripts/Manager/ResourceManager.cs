using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//Keeps track of server data
public class ResourceManager : TungDoesNetworkingForyou
{
    /*Data*/
    //Ping of every player to the server
    [HideInInspector]
    public SyncListInt Pings = new SyncListInt();
    [HideInInspector]
    //If the players are properly connected or not
    [SyncVar]
    public SyncListBool Actives = new SyncListBool();

    /*Required components*/
    private ManagerTracker Tracker;

    void Start()
    {
        Tracker = FindObjectOfType<ManagerTracker>();
    }

    void Update()
    {
        UpdatePings();
        UpdateActives();
    }

    void UpdatePings()
    {
        if (!isServer) return;

        //Dynamic
        for(int i = 0; i < 1; ++i)
        {
            //Add to list to fit connection count
            if (Pings.Count < NetworkServer.connections.Count)
            {
                Pings.Add(0);
                //Recheck
                i -= 1;
            }
            //remove from list to fit connection count
            if (Pings.Count > NetworkServer.connections.Count)
            {
                Pings.RemoveAt(Pings.Count - 1);
                //Recheck
                i -= 1;
            }
        }

        for (int i = 0; i < NetworkServer.connections.Count; ++i)
        {
            byte error;
            if (NetworkServer.connections[i] != null) Pings[i] = NetworkTransport.GetCurrentRTT(NetworkServer.connections[0].connectionId, NetworkServer.connections[i].connectionId, out error);
            else Debug.Log("A player has been disconnected! Warning!");
        }
    }

    void UpdateActives()
    {
        if (!isServer) return;

        //Dynamic
        for (int i = 0; i < 1; ++i)
        {
            //Add to list to fit connection count
            if (Actives.Count < NetworkServer.connections.Count)
            {
                Actives.Add(true);
                //Recheck
                i -= 1;
            }
            //remove from list to fit connection count
            if (Actives.Count > NetworkServer.connections.Count)
            {
                Actives.RemoveAt(Actives.Count - 1);
                //Recheck
                i -= 1;
            }
        }

        for (int i = 0; i < NetworkServer.connections.Count; ++i)
        {
            if (NetworkServer.connections[i] != null) Actives[i] = true;
            else Actives[i] = false;
        }
    }
}
