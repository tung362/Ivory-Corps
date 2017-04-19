using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Hologram keypad 
//-transition for keys to turn on individualy, flicker on, background edge color change (clockwise rotation)

//hologram bottom lights up, keys will flicker on, background edge will show up, then the line shader (hologram shader) fades in
public class ScreenPopUp : MonoBehaviour {
    public bool ScreenUp;
    public bool UseTungKeys = true;
    public bool UseScreenObjectAsOrigin;
    //Key object init's
    public GameObject keyModel; //the 3d model of an individual key
    public List<GameObject> numKeys = new List<GameObject>(); // 10 keys (0-9)
    public List<GameObject> othrKeys = new List<GameObject>(); //3 keys (period, enter, backspace)
    //Spawn positions and rotations
    public Vector2 dimensions;
    private float width, height;
    public Vector3 numKeyOffset;
    public Vector3 othrKeyOffset;
    public Vector3 KeyRotation;
    public Vector3 originPoint;
    //public Vector3 numKeyRotationOffset;

    //Activate Timer
    private float keyTimer = 0.0f;
    public float keyIntervalTime;
    //Deactivate Timer
    public float deactivateKeyIntervalTime;
    private float deaKeyTimer = 0.0f;
    //private bool activateComplete, deactivateComplete;
    

	// Use this for initialization
	void Start () {
        width = dimensions.x; height = dimensions.y;
        //ScreenUp = false;

        //Create new keys
        if (!UseTungKeys)
        {
            Debug.Log("Using Braiden's Keys");
            numKeys = addKeysToList(10);
            othrKeys = addKeysToList(3);

            for (int i = 0; i < numKeys.Count; i++) { numKeys[i].transform.SetParent(this.transform); }
            for (int i = 0; i < othrKeys.Count; i++) { othrKeys[i].transform.SetParent(this.transform); }

        }

        //set the new key's parent to this

        //set the new keys positions 
        numKeys = setKeyPositions(numKeys, 0f, 0f, numKeyOffset);
       othrKeys = setKeyPositions(othrKeys, -.1f, -.2f, othrKeyOffset);


        keyTimer = keyIntervalTime;
        deaKeyTimer = deactivateKeyIntervalTime;
        //activateComplete = false; deactivateComplete = false;

        if (UseScreenObjectAsOrigin){originPoint = this.transform.position;}

	}

    public void ScreenUpBoot()
    {
        ScreenUp = !ScreenUp;
    }


	// Update is called once per frame
	void Update () {
        keyTimer -= Time.deltaTime;
        deaKeyTimer -= Time.deltaTime;
        if (ScreenUp)
        {
            deaCurrNumKey = 0;
            deaCurrOthrKey = 0;
            activateKeys(numKeys, keyTimer, true);
            activateKeys(othrKeys, keyTimer, false);
        }
        else
        {
            currNumKey = 0;
            currOthrKey = 0;
            deactivateKeys(numKeys, deaKeyTimer, true);
            deactivateKeys(othrKeys, deaKeyTimer, false);
        }

        if (keyTimer <= 0)
        {
            keyTimer = keyIntervalTime;
        }

        if (deaKeyTimer <= 0)
        {
            deaKeyTimer = deactivateKeyIntervalTime;
        }

	}


    //will need 13 individual keys 10 for numbers, 1 for period, 1 for backspace, and 1 for enter
    public  List<GameObject> addKeysToList(int numOfKeys)
    {
        int num = numOfKeys;
        List<GameObject> outList = new List<GameObject>();
        
        while (num > 0)
        {
            GameObject l_keyModel = GameObject.Instantiate(keyModel);
            outList.Add(l_keyModel);
            outList[0].transform.position = originPoint;
            num -= 1;
        }


        return outList;
    }


    //these should be the final positions that the keys will reach after the animations are finished
    //should take the list of keys and place them in the correct transform positions
    public List<GameObject> setKeyPositions(List<GameObject> l_list,float l_yDist, float l_zDist, Vector3 l_offset)
    {
        List<GameObject> outputList = new List<GameObject>();
        float l_xDist = width / l_list.Count;
        //float l_yDist = height / l_list.Count;

        //create a local list to save the variables in     DO THIS AND IT WILL BE FIXED
        for (int i = 0; i < l_list.Count; i++)
        {
            outputList.Add(l_list[i]);
            outputList[i].transform.position = new Vector3(this.transform.position.x + i * l_xDist,this.transform.position.y + l_yDist , this.transform.position.z + l_zDist ) + l_offset;
            outputList[i].transform.Rotate(KeyRotation);
            outputList[i].SetActive(false);
        }


        
       
        return outputList;
    }


    int currNumKey = 0;
    int currOthrKey = 0;
        //trying to get the keys to activate one by one with a timer as the buffer
    public void activateKeys(List<GameObject> l_list, float timer, bool isNumKeyList)
    {

        if (timer <= 0)
        {
            if(isNumKeyList && currNumKey < l_list.Count)
            {
                l_list[currNumKey].SetActive(true);
                currNumKey += 1;
            }
            else if (!isNumKeyList && currOthrKey < l_list.Count)
            {
                l_list[currOthrKey].SetActive(true);
                currOthrKey += 1;
            }
        }
    }

    int deaCurrNumKey = 0;
    int deaCurrOthrKey = 0;
    public void deactivateKeys(List<GameObject> l_list, float timer, bool isNumKeyList)
    {
        if (timer <= 0)
        {
            if (isNumKeyList && deaCurrNumKey < l_list.Count)
            {
                l_list[deaCurrNumKey].SetActive(false);
                deaCurrNumKey += 1;
            }
            else if (!isNumKeyList && deaCurrOthrKey < l_list.Count)
            {
                l_list[deaCurrOthrKey].SetActive(false);
                deaCurrOthrKey += 1;
            }
        }
    }

    /*              STEPS NEEDED TO CREATE ANIMATIONS
     *      [x]-Start all keys in the same position
     *      []-create a vector from the starting pos to the ending pos, add in a scale to allow for the keys to show up 
     *      at different times
     *      []-make sure that it is done in bursts (aka, not all of the keys will fire off at once)
     */



}
