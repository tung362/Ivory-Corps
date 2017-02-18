using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//Spawns and respawns elephant
public class PlayerSpawnPoint : NetworkBehaviour
{
    /*Setting*/
    public int SpawnID = 0;

    /*Data*/
    public bool SpawnedFirstTime = true;

    /*Required components*/
    private ManagerTracker Tracker;

    void Start()
    {
        Tracker = FindObjectOfType<ManagerTracker>();
    }

    void Update ()
    {
        FirstTimeSpawn();
    }

    //Spawns player's elephants
    void FirstTimeSpawn()
    {
        if (!isServer) return;

        if (SpawnedFirstTime)
        {
            if (Tracker.IsFullyReady)
            {
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                for (int i = 0; i < players.Length; ++i)
                {
                    if (players[i].GetComponent<PlayerControlPanel>().ID == SpawnID)
                    {
                        Tracker.ThePlayerControlPanel.TheCommandManager.CmdSpawnElephant(transform.position, transform.rotation, SpawnID);
                        SpawnedFirstTime = false;
                    }
                }
            }
        }
    }
}
