using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The actual cube object that the player will interact with and set into sockets to activate systems
public class Cube : MonoBehaviour {
    public Animator cubeAnimator;
    public enum SocketType {VITALS, CANN_1, CANN_2, ADRENALINE, KEYBOARD, NONE };
    SocketType currSocket;
	// Use this for initialization
	void Start () {
        currSocket = SocketType.NONE;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.tag == "Socket")
        {
            CubeSocket c;
            c = other.GetComponent<CubeSocket>();
            //currSocket = c.systType;
        }
    }



    public void InvokeAnimation(string stateName)
    {
        if (cubeAnimator != null)
        {
            cubeAnimator.Play(stateName, 0);
        }
    }
}
