using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
// if it collides with a cube object, it will trigger the event
public class CubeSocket : MonoBehaviour {
    //public CubeSystems manager;
    [Range(0,20)]
    public float socketRadius = 0;
    public SphereCollider sphere;// = new SphereCollider();
    GameObject Socket;
    public Rigidbody rigid;
    public Rigidbody sockRigid;

    public UnityEvent inputEvent;
    public CubeSystems Manager;
    public enum mySystem { VITALS, CANN_1, CANN_2, ADRENALINE, KEYBOARD};
    public mySystem systType;

    void Start()
    {

        rigid = gameObject.AddComponent<Rigidbody>();
        rigid.isKinematic = true;
        rigid.useGravity = false;

        Socket = new GameObject(systType.ToString() + "_Socket");
        Socket.transform.SetParent(this.transform);

        
        sphere = Socket.AddComponent<SphereCollider>();
        sphere.radius = socketRadius;
        sphere.isTrigger = true;

    }



    Animator otherAnimator = new Animator();

    void OnTriggerEnter(Collider other)
    {
        
        
        Debug.Log("BEING TRIGGERED");

        if (other.tag == "Cube")
        {
            otherAnimator = other.GetComponent<Animator>();
            //play animation
            switch (systType)
            {
                case mySystem.VITALS:
                    Debug.Log("I am VITALLY TRIGGERED");
                    otherAnimator.Play("CubeShuffle");
                    

                    break;
                case mySystem.CANN_1:
                    Debug.Log("I am Cannonly 1 TRIGGERED");
                    break;
                case mySystem.CANN_2:
                    Debug.Log("I am Cannonly 2 TRIGGERED");
                    break;
                case mySystem.ADRENALINE:
                    Debug.Log("I am ADRENALINLY TRIGGERED");
                    break;
                case mySystem.KEYBOARD:
                    Debug.Log("I am KEYBOARDLY TRIGGERED");
                    break;
                default:
                    Debug.Log("NO SYST SELECTED");
                    break;
            }
        }
    
    }
    // Use this for initialization

	
	// Update is called once per frame
	void Update () {
		
	}

    // when the object is within range, and when the object
    // is in range and let go (so the animation can start)

}
