using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using UnityEngine.UI;

//Handles the button's tasks and determines which order in the menu select the button is in
public class MenuButton : TungDoesMathForYou
{
    /*Settings*/
    public int ButtonID = 1; //Determines if the button is currently selected or not depending on it's parent's CurrentID (If CurrentID matches ButtonID)
    public bool Animate = true; //Uses the animation handled in this script, for custom ones have this false and make your own script
    public Vector3 SelectedScale = new Vector3(1.4f, 1.4f, 1.4f);

    /*Data*/
    private Vector3 StartingSize = Vector3.one;

    /*Data*/
    public bool Toggle = true;

    /*Callable functions*/
    public UnityEvent OnToggled;

    /*Required components*/
    private RectTransform TheRectTransform;

    void Start()
    {
        TheRectTransform = GetComponent<RectTransform>();
        StartingSize = TheRectTransform.localScale;
    }

    void Update()
    {
        UpdateButton();
    }

    void UpdateButton()
    {
        if (!transform.parent.GetComponent<MenuManager>().Activated) return;
        if (transform.parent.GetComponent<MenuManager>().CurrentID == ButtonID) TheRectTransform.localScale = SelectedScale;
        else TheRectTransform.localScale = StartingSize;
    }
}
