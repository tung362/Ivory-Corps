using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollisionTest : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item") Debug.Log("Augoo");
    }
}
