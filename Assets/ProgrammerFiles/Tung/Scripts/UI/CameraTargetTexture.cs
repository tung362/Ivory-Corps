using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//Code generated target texture so it can can control the resolution
public class CameraTargetTexture : MonoBehaviour
{
    /*Settings*/
    public Material TargetMaterial;
    public bool IsNetworked = false; //Used for cameras that is over the network instead of already existing on the scene, ensures that there is authority to render

    /*Data*/
    private bool RunOnce = true;

    /*Required components*/
    private Camera TheCamera;

    void Start()
    {
        TheCamera = GetComponent<Camera>();
    }

    void Update()
    {
        //If there is no authority destroy the camera and prevent it from rendering onto the monitors
        if(RunOnce)
        {
            if (IsNetworked)
            {
                if (!transform.root.GetComponent<NetworkIdentity>().hasAuthority)
                {
                    Destroy(TheCamera.gameObject);
                    Destroy(this);
                    return;
                }

                RenderTexture TheTargetTexture = new RenderTexture(Screen.width, Screen.height, 24);
                TheCamera.targetTexture = TheTargetTexture;
                TargetMaterial.mainTexture = TheTargetTexture;
            }
            else
            {
                RenderTexture TheTargetTexture = new RenderTexture(Screen.width, Screen.height, 24);
                TheCamera.targetTexture = TheTargetTexture;
                TargetMaterial.mainTexture = TheTargetTexture;
            }
            RunOnce = false;
        }
    }
}
