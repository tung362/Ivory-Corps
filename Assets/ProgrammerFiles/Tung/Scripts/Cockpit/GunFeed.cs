using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Detects ammo for the cannons
public class GunFeed : TungDoesMathForYou
{
    private Gun TheGun;

    void Start()
    {
        TheGun = transform.root.GetComponent<Gun>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ammo") TheGun.BulletUpdate(other.transform.gameObject);
    }
}
