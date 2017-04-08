using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
// if it collides with a cube object, it will trigger the event




public class CubeSocket : MonoBehaviour {
    public bool hasCube = false;

    //public CubeSystems manager;
    [Range(0,20)]
    public float socketRadius = 0;
    public SphereCollider sphere;// = new SphereCollider();
    GameObject Socket;
    public GameObject Model;
    public Rigidbody rigid;
    public Rigidbody sockRigid;

   // public UnityEvent inputEvent;
    public CubeSystems Manager;
    public enum mySystem { VITALS, CANN_1, CANN_2, ADRENALINE, KEYBOARD};
    public mySystem systType;
    
    void Start()
    {
       
        tag = "Socket";
        
        rigid = gameObject.AddComponent<Rigidbody>();
        rigid.isKinematic = true;
        rigid.useGravity = false;

        Socket = new GameObject(systType.ToString() + "_Socket");
        Socket.transform.SetParent(this.transform);
        Socket.transform.position = this.transform.position;
        Socket.tag = "Socket";
        
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
            otherAnimator = other.GetComponentInChildren<Animator>();
            //play animation
            switch (systType)
            {
                case mySystem.VITALS:
                    //Debug.Log("I am VITALLY TRIGGERED");
                    otherAnimator.SetTrigger("EnteredVitals");
                    Manager.SystemVitalBoot();
                    

                    break;
                case mySystem.CANN_1:
                    //Debug.Log("I am Cannonly 1 TRIGGERED");
                    otherAnimator.SetTrigger("EnteredCannon");
                    Manager.SystemCannonOneBoot();
                    break;
                case mySystem.CANN_2:
                    //Debug.Log("I am Cannonly 2 TRIGGERED");
                    otherAnimator.SetTrigger("EnteredCannon");
                    Manager.SystemCannonTwoBoot();
                    break;
                case mySystem.ADRENALINE:
                    //Debug.Log("I am ADRENALINLY TRIGGERED");
                    otherAnimator.SetTrigger("EnteredAdrenaline");
                    Manager.SystemAdrenalineBoot();
                    break;
                case mySystem.KEYBOARD:
                    //Debug.Log("I am KEYBOARDLY TRIGGERED");
                    otherAnimator.SetTrigger("EnteredKeyboard");
                    Manager.SystemKeyboardBoot();
                    break;
                default:
                    Debug.Log("NO SYST SELECTED");
                    break;
            }
        }
    
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Leaving the triggered state");

        if (other.tag == "Cube")
        {
            otherAnimator = other.GetComponentInChildren<Animator>();
            //play animation
            switch (systType)
            {
                case mySystem.KEYBOARD:
                    //otherAnimator.ResetTrigger("EnteredKeyboard");
                    otherAnimator.SetTrigger("hasLeftKeyboard");
                    Manager.DeactivateKeyboardSystem();
                    break;
                case mySystem.VITALS:
                    Manager.DeactivateVitalSystem();
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
        switch (systType)
        {
            case mySystem.VITALS:
                //Model.GetComponent<Material>().color = Color.green;
                break;
            case mySystem.CANN_1:
                break;
            case mySystem.CANN_2:
                break;
            case mySystem.ADRENALINE:
                break;
            case mySystem.KEYBOARD:
                break;
            default:
                break;
        }
    }


    

    // when the object is within range, and when the object
    // is in range and let go (so the animation can start)

}
