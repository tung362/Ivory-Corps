﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressableButton : MonoBehaviour
{
    /*Settings*/
    public float PressLimitY = -0.015f;
    public float ButtonRiseSpeed = 10;

    /*Data*/
    private float StartingY = 0;
    private bool Toggle = true;

    /*Callable functions*/
    public UnityEvent OnToggled;

    /*Required components*/
    private Rigidbody TheRigidbody;

    void Start ()
    {
        TheRigidbody = GetComponent<Rigidbody>();
        StartingY = transform.localPosition.y;
    }

    void Update()
    {
        UpdateButton();
    }

    void UpdateButton()
    {
        if (transform.localPosition.y < StartingY) TheRigidbody.velocity = transform.parent.transform.up * ButtonRiseSpeed * Time.fixedDeltaTime;
        if (transform.localPosition.y > StartingY) transform.localPosition = new Vector3(transform.localPosition.x, StartingY, transform.localPosition.z);
        if (transform.localPosition.y < PressLimitY) transform.localPosition = new Vector3(transform.localPosition.x, PressLimitY, transform.localPosition.z);

        if (transform.localPosition.y <= PressLimitY)
        {
            if(Toggle)
            {
                OnToggled.Invoke();
                Toggle = false;
            }
        }
        else Toggle = true;
    }
}
