using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIConnectionCheck : MonoBehaviour
{
    /*Callable functions*/
    public UnityEvent OnToggle;

    /*Required components*/
    private ManagerTracker Tracker;

    void Start()
    {
        Tracker = FindObjectOfType<ManagerTracker>();
    }

    void Update()
    {
        UpdateNetworkCheck();
    }

    void UpdateNetworkCheck()
    {
        if (!Tracker.IsFullyReady) return;
        if(Tracker.ThePlayerControlPanel.ID != -1 && Tracker.ThePlayerControlPanel.ID + 1 <= Tracker.ThePlayerControlPanel.TheResourceManager.Actives.Count)
        {
            //Atleast 2 players NOTE: Can be improved later on to support moree players
            if (Tracker.ThePlayerControlPanel.TheResourceManager.Actives.Count >= 2)
            {
                if (Tracker.ThePlayerControlPanel.TheResourceManager.Actives[0] && Tracker.ThePlayerControlPanel.TheResourceManager.Actives[1]) OnToggle.Invoke();
            }
        }
    }
}
