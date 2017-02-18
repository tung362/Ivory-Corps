using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//Client player's central command
public class PlayerControlPanel : NetworkBehaviour
{
    /*Data*/
    [SyncVar]
    [HideInInspector]
    public int ID = -1;

    /*Managers*/
    [HideInInspector]
    public ResourceManager TheResourceManager;
    [HideInInspector]
    public CommandManager TheCommandManager;

    /*Required components*/
    private ManagerTracker Tracker;

    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        Tracker = FindObjectOfType<ManagerTracker>();

        GetManagers();

        if (isServer) ID = connectionToClient.connectionId;
        if (!hasAuthority) return;

        Tracker.ThePlayerControlPanel = this;
    }

    void GetManagers()
    {
        if (isServer || hasAuthority)
        {
            TheResourceManager = GetComponent<ResourceManager>();
            TheCommandManager = GetComponent<CommandManager>();
        }
    }
}