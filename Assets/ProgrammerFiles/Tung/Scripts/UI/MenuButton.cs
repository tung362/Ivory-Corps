using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    /*Settings*/
    public InputField IP;
    public InputField Port;

    /*Required components*/
    private ManagerTracker Tracker;
    private NetworkManager TheNetworkManager;

    void Start()
    {
        Tracker = FindObjectOfType<ManagerTracker>();
    }

    //Adds a string into a text field
    public void InputStringToField(string InputLetter, GameObject TargetInputField)
    {
        
    }

    //Host server
    public void Host()
    {
        TheNetworkManager.networkPort = int.Parse(Port.text);
        TheNetworkManager.StartHost();
    }

    //Connect to server
    public void Join()
    {
        TheNetworkManager.networkAddress = IP.text;
        TheNetworkManager.networkPort = int.Parse(Port.text);
        TheNetworkManager.StartClient();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
