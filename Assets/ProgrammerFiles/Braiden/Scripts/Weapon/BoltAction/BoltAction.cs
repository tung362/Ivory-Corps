using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class will handle the weapon's reloading mechanic for the two cannons
public class BoltAction : MonoBehaviour {
    private enum Gunstates  {UP,BACK,FORWARD,DOWN} private Gunstates  m_gunState;
    private enum LoadStates {EMPTY, LOADED, FIRED} private LoadStates m_loadState;


    public GameObject CannonFrame;//child of lever
    public GameObject CannonLever;//child of bolt
    public GameObject  CannonBolt;//parent
    public GameObject CannonRound;//the bullet prefab for the cannon to fire

    private Vector3 BoltStartPos; 


        [System.Serializable]
    public struct GunParams
    {
        [Range(0,12)]
        public int pullDistance;

        [Range(0.0f,90.0f)]
        public float liftAngle;

        [Tooltip("Checks to see if the current cannon is the left or right one")]
        public bool   isLeftGun;

    } public GunParams m_gunParams;



    void Start () {
        CannonLever.transform.SetParent(CannonBolt.transform) ;
        CannonFrame.transform.SetParent(CannonLever.transform);
        m_gunState  =   Gunstates.DOWN;
        m_loadState = LoadStates.EMPTY;

        BoltStartPos = CannonBolt.transform.position;//gives us pull back pos
	}

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(BoltStartPos, new Vector3(0,0,-m_gunParams.pullDistance));
    }
	
	
	void Update () {
	
	}


    //will make sure the weapon follows the correct logic in order to reload
    void UpdateGunLogic()
    {
        
        CheckBoltState();
        CheckBulletState();
    }


    //what pos is the bolt in?
    void CheckBoltState()
    {

    }

    //is there a round in the chamber?
    void CheckBulletState()
    {

    }
}
