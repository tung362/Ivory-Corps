using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this is a placeholder class for the beast 
public class BeastMode : MonoBehaviour {

    [Header("Intake Systems")]
    public AdrenalineSystem adr;
    public VitalsManager vitals;
    public DamageSystem damage;
    //Movement class
    public CubeSystems cubeSystem;
    public CannonSystem cannons;

    [Header("System Transform Offsets")]
    public Transform adrenalinePosition;
    public Transform vitalsPosition;
    public Transform damagePosition;
    public Transform cannonPosition;// may take this out later

	
}
