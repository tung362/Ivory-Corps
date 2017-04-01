using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//Most client to server commands are located here
public class CommandManager : NetworkBehaviour
{
    /*Prefabs to spawn*/
    public GameObject Elephant1;

    /*Required components*/
    private ManagerTracker Tracker;

    void Start()
    {
        Tracker = FindObjectOfType<ManagerTracker>();
    }

    //Lobby Commands////////////////////////////////////////////////////////////////

    //Server callbacks

    ////////////////////////////////////////////////////////////////////////////////

    //UI Commands///////////////////////////////////////////////////////////////////

    //Server callbacks

    ////////////////////////////////////////////////////////////////////////////////

    //Game Commands/////////////////////////////////////////////////////////////////
    [Command]
    public void CmdSpawnElephant(Vector3 Position, Quaternion Rotation, int ID)
    {
        SpawnElephant(Position, Rotation, ID);
    }

    [Command]
    public void CmdFire()
    {

    }

    //Server callbacks
    [ServerCallback]
    public void SpawnElephant(Vector3 Position, Quaternion Rotation, int ID)
    {
        GameObject spawnedObject = Instantiate(Elephant1, Position, Rotation) as GameObject;
        NetworkServer.SpawnWithClientAuthority(spawnedObject, NetworkServer.connections[ID]);
    }
    ////////////////////////////////////////////////////////////////////////////////
}
