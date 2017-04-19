using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaderButton : MonoBehaviour
{
    //Object has to have a FadeInOut script
    public void ToggleFade(GameObject fader)
    {
        fader.GetComponent<FadeInOut>().Toggle();
    }
}
