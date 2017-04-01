using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//Keeps track of the client's player, also all local settings
public class ManagerTracker : MonoBehaviour
{
    /*Access to client's player*/
    public PlayerControlPanel ThePlayerControlPanel;

    /*Check*/
    public bool IsFullyReady = false; //Very important! make sure every script that uses this checks if its true before accessing anything from this script

    /*Data*/
    public int FlameAmmo = 0;
    public int LeftSideAmmo = 0;
    public int RightSideAmmo = 0;
    public int TopAmmo = 0;

    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Update()
    {
        if(ThePlayerControlPanel != null)
            if (ThePlayerControlPanel.ID != -1 && ThePlayerControlPanel.TheResourceManager != null && ThePlayerControlPanel.TheCommandManager != null) IsFullyReady = true;
    }
}
