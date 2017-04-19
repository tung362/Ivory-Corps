using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressableButton : TungDoesMathForYou
{
    /*Settings*/
    public float PressLimitY = -0.015f;
    public float RepressableThresholdPercentage = 0.5f;
    public float ButtonRiseSpeed = 10;

    /*Data*/
    private float StartingY = 0;
    private Vector3 StartingPosition;
    private bool Toggle = true;

    /*Callable functions*/
    public UnityEvent OnToggled;

    /*Required components*/
    private Rigidbody TheRigidbody;

    void Start ()
    {
        TheRigidbody = GetComponent<Rigidbody>();
        StartingY = transform.localPosition.y;
        StartingPosition = transform.localPosition;
    }

    void Update()
    {
        UpdateButton();
    }

    void UpdateButton()
    {
        TheRigidbody.velocity = Vector3.zero;
        transform.localPosition = new Vector3(StartingPosition.x, transform.localPosition.y, StartingPosition.z);
        if (transform.localPosition.y < StartingY) TheRigidbody.velocity = transform.transform.up * ButtonRiseSpeed * Time.fixedDeltaTime;
        if (transform.localPosition.y > StartingY) transform.localPosition = new Vector3(transform.localPosition.x, StartingY, transform.localPosition.z);
        if (transform.localPosition.y < PressLimitY) transform.localPosition = new Vector3(transform.localPosition.x, PressLimitY, transform.localPosition.z);

        if (transform.localPosition.y <= PressLimitY)
        {
            if (Toggle)
            {
                OnToggled.Invoke();
                Toggle = false;
            }
        }
        else
        {
            float difference = (StartingY - PressLimitY) * RepressableThresholdPercentage;
            if (transform.localPosition.y >= PressLimitY + difference) Toggle = true;
        }
    }
}
