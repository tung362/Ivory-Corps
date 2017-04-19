using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//This class will be in charge of all the Systems connected to the cubes

    /*
        CubeEvent Index Order
     * Vitals
     * Two cannons, trunk
     * adrenaline 
     * Keyboards
     */
     
public class CubeSystems : MonoBehaviour
{
                //EVENT STUFF\\
    //This will be called during any event
    public UnityEvent AnySystemActivatedEvent;
    //specific event
    [Tooltip("Vitals[0], Adren[1], Cann1[2], Cann2[3], keyboard[4], de-vital[5], de-keyboard[6], de-adren[7]")]
    public UnityEvent[] SystemActivation;

    //CUBE STUFF
    public GameObject Cube1, Cube2;
    public Vector3 cubePos1, cubePos2; 

    private void Start()
    {
       
    }

    public void Update()
    {
        
    }

    //invoke the start up of the vitals system
    public void SystemVitalBoot()
    {
        Debug.Log("Trying to boot the vitals");
        //should call on the vitals manager  and anything related to it
        SystemActivation[0].Invoke();
        //CubeEvent.Invoke();

    }

    public void SystemAdrenalineBoot()
    {
        SystemActivation[1].Invoke();
    }

    public void SystemCannonOneBoot()
    {
        SystemActivation[2].Invoke();
    }

    public void SystemCannonTwoBoot()
    {
        SystemActivation[3].Invoke();
    }



    public void SystemKeyboardBoot()
    {
        SystemActivation[4].Invoke();
    }

    public void DeactivateVitalSystem()
    {
        SystemActivation[5].Invoke();
    }

    public void DeactivateKeyboardSystem()
    {
        SystemActivation[6].Invoke();
    }

    public void DeactivateAdrenalineSystem()
    {
        SystemActivation[7].Invoke();
    }

}





//Each event will hold the activation for the functionality of the system it is currently in as well as the lighting that is in that section


//We are going to create a event system that will be called when we place the cube in a socket object 




 //   public GameObject Cube1, Cube2;
 //   // Use this for initialization
 //   //This will hold all of the systems that we want to either activate or deactivate
 //   public List<GameObject> ConnectedSystems;

 //   public List<GameObject> Sockets;
 //   //Allows you to add offsets to the individual socket
    

 //       //TODO  Lookup how to make the members for the Transforms in the list appear in the Editor
 //   public List<Transform> offsetTransformList;
 //   private bool hasOffset = false;

	//void Start () {
 //       //make sure to add the materials later once the artists are finished with them 
 //       Cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
 //       Cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);

 //       for (int i = 0; i < ConnectedSystems.Count; i++)
 //       {
 //           Sockets.Add(new GameObject());
 //           offsetTransformList.Add(Transform.Instantiate(ConnectedSystems[i].transform, this.transform));
 //           //Sockets[i].gameObject.transform.position += offsetList[i]; NEEDS TO BE DYNAMIC
 //           GiveSocketNames(i, ConnectedSystems[i]);
 //       }




	//}
	
	//// Update is called once per frame
	//void Update ()
 //   {
        
		
	//}


 //   void GiveSocketNames(int indexNum,GameObject systemObject)
 //   {
 //       //Create a name for each socket based on the system object that it is going to be a part of
 //       Sockets[indexNum].gameObject.name = "socket_" + systemObject.name;
 //   }

 //   //Add the offset values to the sockets from the SystemPositions
 //   void AddOffset(int indexNum, Transform t)
 //   {
 //       Sockets[indexNum].transform.position = ConnectedSystems[indexNum].transform.position + t.position;
 //       Sockets[indexNum].transform.rotation = t.rotation;
 //       Sockets[indexNum].transform.localScale = t.localScale;
 //   }


    /*
     *Vitals
     * Weapon1
     * Weapon2
     * MainCannon
     * Adrenaline



     */

