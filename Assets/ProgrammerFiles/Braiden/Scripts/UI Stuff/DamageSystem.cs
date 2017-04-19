using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//This system will be attached to each beast 
//and will act as the interface for interacting with the
//oher manager classes
public class DamageSystem : MonoBehaviour {
    public GameObject Beast_One, Beast_Two;
    public UnityEvent b1_damageEvent;
    public UnityEvent b2_damageEvent;

    [Tooltip("X is for Beast One, and Y is for Beast Two")]
    Vector2 DamageMultiplier;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void Update () {
		
	}

    //This will be used by the network to send damage to each other
    void SendDamage(GameObject other, Vector2 multiplier)
    {
        
    }



    static float resetTime; bool reseted = false;
    public void MidigateDamage(GameObject other, float amt, float dTime)
    {
        if (!reseted) { resetTime = dTime; reseted = true; }
        if (dTime <= 0.0f) dTime = resetTime;

        
        dTime -= Time.deltaTime;
        
        
    }
}
