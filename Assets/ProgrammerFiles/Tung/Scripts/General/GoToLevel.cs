using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLevel : MonoBehaviour
{
    public string Level1 = "";
    public string Level2 = "";
    public string Level3 = "";
    public string Level4 = "";

    void Start ()
    {
        DontDestroyOnLoad(gameObject);
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SceneManager.LoadScene(Level1);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SceneManager.LoadScene(Level2);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SceneManager.LoadScene(Level3);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SceneManager.LoadScene(Level4);
    }
}
