using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//what all systems need
    public class InheritanceSystem : MonoBehaviour {
    //the mammoth
    public BeastMode beast;

    //the socket that we are creating
    protected CubeSocket socket;
    //the model that will be at the system location
    public GameObject modelPrefab;
    //an offset position for the socket 
    public Vector3 socketPos;
    
    public Animator systemAnimator;
    public AudioClip basicSoundEffect;

    [Tooltip("Socket prefab")]
    public GameObject prefab;

    //the cube system in the scene that must be connected
    public CubeSystems ManagerObject;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
