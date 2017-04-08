using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class will be in charge of changing the vital signs variables, 
//as well as changing them when called

//a sphere will try and follow a sine wave, and will also have a trail renderer behind it
public class VitalsManager : MonoBehaviour {

    [Header("Vitals Assembly Parts")]
    public Transform     pulsePos;
    public GameObject    leftCorner;
    public GameObject    rightCorner;
    public TrailRenderer VitalTrailRend;
    public AudioClip     heartBeatSound;
    public AudioSource   AudioPlaybackPos;
    public RectTransform BackgroundImage;

    [Header("Monitor Variables")]
    //how many scribbles per given spot
    [Range(0, 10)]
    public float Frequency;
    Vector2 xRange, yRange, zRange;
    public bool xAxis, yAxis, zAxis;
    //protected Vector3 currAxis;
    public float HeartRate;
    public Vector3 startPos = Vector3.zero, endPos  = Vector3.zero;
    Vector3 followPos;
    public float resetTime;
    float rTimer = 0;

    public float VitalsScale = 1.0f;

    private void Start()
    {
        startPos =  leftCorner.transform.position;
        endPos   = rightCorner.transform.position;

        pulsePos.position = startPos;
        xRange = new Vector2(startPos.x, endPos.x) * VitalsScale;
        yRange = new Vector2(startPos.y, endPos.y) * VitalsScale;
        zRange = new Vector2(startPos.z, endPos.z) * VitalsScale;

        //if (xAxis){ currAxis += new Vector3(1, 0, 0);}
        //if (yAxis){ currAxis += new Vector3(0, 1, 0);}
        //if (zAxis){ currAxis += new Vector3(0, 0, 1);}
        
      

    }

    private void Update()
    {
        leftCorner.transform.position = startPos;
        rightCorner.transform.position = endPos;
        this.transform.localScale = new Vector3(VitalsScale, VitalsScale, VitalsScale);
        followPos.x += movePulse();
        heartBeat();

        startPos =  leftCorner.transform.position;
        endPos   = rightCorner.transform.position;

        if (pulsePos.position.x > xRange.y){followPos.x = startPos.x;}

        pulsePos.position = followPos;
        //Debug.Log(followPos);

        rTimer += Time.deltaTime;

        if (rTimer >= resetTime) {rTimer = 0.0f;}
    }

    void UpdateBackgroundImage()
    {
        //Make sure it's position & scale are within the boundaries
        BackgroundImage.transform.position.Set(startPos.x, startPos.y, 0);
        BackgroundImage.localScale = this.transform.localScale;
    }

    // Will make the vitals move left to right
    float movePulse()
    {
        float inVec = leftCorner.transform.position.x;
        inVec += Time.deltaTime * HeartRate;

        //resets x pos if past max distance
        if (pulsePos.position.x > xRange.y){return startPos.x;}
        else{return inVec;}

    }

    // create gates
    bool goUpY = true;
    bool goUpZ = true;

    //The funtion acts as the gates for when the position resets will happen for the Y axis
    void heartBeat()
    {
        //              Gates
        if (followPos.y > yRange.y){goUpY = false; AudioPlaybackPos.PlayOneShot(heartBeatSound); }
        else if (followPos.y < yRange.x){goUpY = true;}

        if (followPos.z > zRange.y){goUpZ = false;}
        else {goUpZ = true;}

        //              Actions
        if (goUpY && yAxis){followPos.y += Time.deltaTime * HeartRate * (Frequency / .75f) / Random.Range(.2f, 1.0f);
            
        }
        else{followPos.y = startPos.y - 2.5f;}

        if (goUpZ && zAxis){followPos.z += Mathf.SmoothStep(zRange.x, zRange.y, Time.deltaTime * HeartRate);}
        else{followPos.z += Mathf.SmoothStep(zRange.x, zRange.y, Time.deltaTime * HeartRate);}

    }

}
