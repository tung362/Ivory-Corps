using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//class that should be the overhead in the updating and value change 
//This class should also hold the back-prop function for error eval.
//will also act as the visualizer in the scene

 struct response
{
    string answer;
    char a;
    bool resp;
    int i;
    float f;

}


public class PerceptronModel : MonoBehaviour {
    public string FileLocation; //for file io
    public List<PerceptronInput> InputNeurons;
    List<PerceptronHidden> HiddenNeurons = new List<PerceptronHidden>();
    public List<PerceptronOutput> OutputNeurons = new List<PerceptronOutput>();



	// Use this for initialization
	void Start () {
        InputNeurons = new List<PerceptronInput>();
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        
    }

    // Update is called once per frame
    void Update () {
		
	}
}
