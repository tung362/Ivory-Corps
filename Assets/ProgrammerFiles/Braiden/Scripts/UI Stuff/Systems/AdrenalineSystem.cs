using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This will be the pill-like affect for the beast 

    /*
        it will increase the stamina of the beast 
        will allow for more resistance to damage
        is a single use (last resort) item
     */

public class AdrenalineSystem : InheritanceSystem {
    //use to increase the heart-rate during boost
    AdrenalineSystem self;

    bool boostUsed = false;
    bool boostComplete = false;
    float adrenalineTimer = 0.0f;
    private float resetAdrenalineTime;

    GameObject m_model;
    GameObject sock;
	// Use this for initialization


	void Start () {
        self = this;
        newInitAdrenaline();
        //initAdrenaline();

        
	}
	
	// Update is called once per frame
	public void Update () {
		
	}



    void newInitAdrenaline()
    {


        m_model = GameObject.Instantiate(modelPrefab, this.transform);
        m_model.name = "System Model ";
        sock = GameObject.Instantiate(prefab);
        sock.transform.SetParent(self.transform);
        sock.transform.position = this.transform.position;
        sock.name = "Socket Object";
        socket = sock.GetComponent<CubeSocket>();
        socket.systType = CubeSocket.mySystem.ADRENALINE;
        socket.Manager = beast.cubeSystem;
        
    }


    void initAdrenaline()
    {
        prefab = GameObject.Instantiate(modelPrefab, this.transform);
        prefab.name = "AdrenalineObject";
        socket = prefab.AddComponent<CubeSocket>();
        socket.rigid = prefab.AddComponent<Rigidbody>();
        socket.rigid.isKinematic = false;
        socket.rigid.useGravity = false;
        socket.systemTransform = this.transform;
        //socket.sphere = prefab.AddComponent<SphereCollider>();
        //socket.sphere.radius = socket.socketRadius;
        //socket.sphere.isTrigger = true;

        resetAdrenalineTime = adrenalineTimer;
        socket.socketRadius = 3.0f;
        socket.Manager = beast.cubeSystem;
        socket.systType = CubeSocket.mySystem.ADRENALINE;
    }
    void UseBoost(bool isBoostUsed)
    {
        if (!isBoostUsed)
        {
            //effect the different systems 
            beast.vitals.HeartRate += .01f;
            beast.damage.MidigateDamage(this.GetComponentInParent<GameObject>(), 10.0f, 2.0f);
            systemAnimator.SetTrigger("UseBoost");

        }
    }
}


/*  THINGS THAT THIS WILL AFFECT
 *  Heart Rate in VitalsManager
 *  Damage Percent in DamageSystem
 *  the animation state for the different effects attached
 to the adrenaline system (the lighting, the cables, etc...)

 *  We need to have a sound effect for when the adrenaline is going off
 */