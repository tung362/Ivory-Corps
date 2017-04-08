using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTracker : TungDoesMathForYou
{
    public bool BuilderMode = false;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) BuilderMode = !BuilderMode;
    }
}
