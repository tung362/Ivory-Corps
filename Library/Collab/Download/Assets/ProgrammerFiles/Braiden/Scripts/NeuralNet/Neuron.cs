using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron : MonoBehaviour {

    //Gizmos update toggle
    public bool showGiz;
    List<Neuron> Connections; //input
    response myData; //output data
    SphereCollider myCollider; //for gizmos drawing

    // Use this for initialization
    void Start () {
		
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(myCollider.transform.position, myCollider.radius);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
