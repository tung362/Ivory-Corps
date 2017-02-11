using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MonoBehaviour
{
    public float RunSpeed = 20;
    public float WalkSpeed = 10;
    public float StrafeSpeed = 5;

    public void RunForward()
    {
        transform.position += new Vector3(0, 0, 1) * RunSpeed * Time.deltaTime;
    }

    public void WalkForward()
    {
        transform.position += new Vector3(0, 0, 1) * WalkSpeed * Time.deltaTime;
    }

    public void Stop()
    {

    }

    public void WalkBackward()
    {
        transform.position += new Vector3(0, 0, -1) * WalkSpeed * Time.deltaTime;
    }

    public void RunBackward()
    {
        transform.position += new Vector3(0, 0, -1) * RunSpeed * Time.deltaTime;
    }

    public void StrafeLeft()
    {
        transform.position += new Vector3(-1, 0, 0) * StrafeSpeed * Time.deltaTime;
    }

    public void StrafeRight()
    {
        transform.position += new Vector3(1, 0, 0) * StrafeSpeed * Time.deltaTime;
    }
}
