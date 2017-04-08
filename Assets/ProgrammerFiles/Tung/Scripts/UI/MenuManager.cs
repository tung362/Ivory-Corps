using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles a category in the menu (what options comes avaiable when a button in a category is pressed (sub buttons))
public class MenuManager : TungDoesMathForYou
{
    /*Setting*/
    public bool Activated = true; //If the current menu category
    public int CurrentID = 1; //Determine which button is currently selected
    public int MaxID = 3; //Amount of buttons in the menu

    void Update()
    {
        UpdateButtonManager();
    }

    void UpdateButtonManager()
    {
        if (CurrentID < 1) CurrentID = MaxID;
        if (CurrentID > MaxID) CurrentID = 1;
    }

    //Turn on this menu's category
    public void Activate()
    {
        Activated = true;
    }

    //Turn off this menu's category
    public void Deactivate()
    {
        Activated = false;
        CurrentID = 1;
    }
}
