using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

//Displays the ping of host to player
public class UIShowPing : MonoBehaviour
{
    /*Required components*/
    private Text TheText;
    private ManagerTracker Tracker;

    void Start ()
    {
        Tracker = FindObjectOfType<ManagerTracker>();
        TheText = GetComponent<Text>();
    }
	

	void Update ()
    {
        if(!Tracker.IsFullyReady) return;
        if(Tracker.ThePlayerControlPanel.ID != -1 && Tracker.ThePlayerControlPanel.ID + 1 <= Tracker.ThePlayerControlPanel.TheResourceManager.Pings.Count) TheText.text = "Ping: " + Tracker.ThePlayerControlPanel.TheResourceManager.Pings[Tracker.ThePlayerControlPanel.ID];

    }
}
