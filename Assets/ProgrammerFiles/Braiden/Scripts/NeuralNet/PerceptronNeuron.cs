using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Things that I need in a Perceptron neuron model 
 
     threshold value
     current sum
     input data / neurons
     save output data to the input data of the next neuron
     rng seed function
     list of connected nodes
     

*/
public enum neuronType { LAYER_1, LAYER_2, LAYER_3 };
public class PerceptronNeuron : MonoBehaviour {
     // to tell weither it's input, abstract, or output data
    public List<PerceptronNeuron> m_inputNeuronList;//input data
   // public List<PerceptronNeuron> m_outputNeuronList;//output data, we may not need a list dedicated for the neuron to know who is taking in the data


    public neuronType m_type = neuronType.LAYER_1;
    [Range(-1.0f, 1.0f)]
    public float Threshold = 0.0f;
    private float weightSum;
    private bool aboveThreshold = false;

    // Use this for initialization
    void Start () {

       
	}
	
	// Update is called once per frame
	void Update () {
       aboveThreshold = TestThreshold(Threshold);	
	}

    bool TestThreshold(float l_threshold)
    {



        return true;
    }
}
