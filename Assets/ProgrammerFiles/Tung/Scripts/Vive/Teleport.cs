using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Teleports to where your pointing
public class Teleport : MonoBehaviour
{
    /*Settings*/
    public GameObject Hand;
    public LineRenderer Laser;

    /*Controller*/
    [HideInInspector]
    private StickController Stick;

    void Start ()
    {
        Stick = GetComponent<StickController>();
    }
	
	void Update ()
    {
        UpdateInput();
    }

    void UpdateInput()
    {
        if (Stick.Controller.GetPress(Stick.TouchpadButton)) TelportSet();
        if (Stick.Controller.GetPressUp(Stick.TouchpadButton)) ApplyTeleport();
    }

    public void TelportSet()
    {
        Ray ray = new Ray();
        ray.origin = Hand.transform.position;
        ray.direction = Hand.transform.forward;

        RaycastHit[] hits = Physics.RaycastAll(ray);

        Vector3 hitPoint = Vector3.zero;
        bool hitGround = false;
        for (int i = 0; i < hits.Length; ++i)
        {
            if (hits[i].transform.tag == "Ground")
            {
                hitPoint = hits[i].point;
                hitGround = true;
            }
        }

        if (hitGround)
        {
            Laser.SetPosition(0, Hand.transform.position);
            Laser.SetPosition(1, hitPoint);
        }
        else
        {
            Laser.SetPosition(0, Vector3.zero);
            Laser.SetPosition(1, Vector3.zero);
        }
    }

    public void ApplyTeleport()
    {
        Laser.SetPosition(0, Vector3.zero);
        Laser.SetPosition(1, Vector3.zero);
        //Raycast to ground
        Ray ray = new Ray();
        ray.origin = Hand.transform.position;
        ray.direction = Hand.transform.forward;

        RaycastHit[] hits = Physics.RaycastAll(ray);

        for (int i = 0; i < hits.Length; ++i)
        {
            if (hits[i].transform.tag == "Ground") transform.root.position = new Vector3(hits[i].point.x, transform.root.position.y, hits[i].point.z);
        }
    }
}
