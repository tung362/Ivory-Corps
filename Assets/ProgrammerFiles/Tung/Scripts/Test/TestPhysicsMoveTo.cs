using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPhysicsMoveTo : MonoBehaviour
{
    public float MoveSpeed = 10;
    public float RotationSpeed = 2000;
    public GameObject Hand;

	void Start ()
    {
    }
	
	void FixedUpdate ()
    {
        Hand.transform.position = Vector3.MoveTowards(Hand.transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z), MoveSpeed * Time.fixedDeltaTime);
        Hand.transform.rotation = Quaternion.RotateTowards(Hand.transform.rotation, transform.rotation, RotationSpeed * Time.fixedDeltaTime);
    }
}
