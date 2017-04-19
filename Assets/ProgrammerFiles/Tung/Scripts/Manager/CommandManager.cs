using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//Most client to server commands are located here
public class CommandManager : TungDoesNetworkingForyou
{
    /*Prefabs to spawn*/
    public GameObject Elephant1;
    public GameObject shellPrefab;
    /*Required components*/
    private ManagerTracker Tracker;

    void Start()
    {
        Tracker = FindObjectOfType<ManagerTracker>();
    }
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
    public void CmdFire(Vector3 Pos, Quaternion rot, int ID)
    {
        SpawnCannonShell(Pos, rot, ID);
    }



    //Server callbacks
    [ServerCallback]
    public void SpawnElephant(Vector3 Position, Quaternion Rotation, int ID)
    {
        GameObject spawnedObject = Instantiate(Elephant1, Position, Rotation) as GameObject;
        NetworkServer.SpawnWithClientAuthority(spawnedObject, NetworkServer.connections[ID]);
    }

    [ServerCallback]
    public void SpawnCannonShell(Vector3 pos, Quaternion rotation,int ID )
    {

        GameObject g = GameObject.Instantiate( shellPrefab, pos, rotation) as GameObject;
        NetworkServer.SpawnWithClientAuthority(g, NetworkServer.connections[ID]);
       Rigidbody r =  g.AddComponent<Rigidbody>();
        r.useGravity = false;
        r.AddForce(new Vector3(0, 20.0f * Time.deltaTime, 0), ForceMode.Force);
        GameObject.Destroy(g, 5.0f);
    }
    ////////////////////////////////////////////////////////////////////////////////
}
