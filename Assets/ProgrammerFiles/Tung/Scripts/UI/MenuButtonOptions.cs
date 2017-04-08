using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using UnityEngine.UI;

//Handles various button tasks
public class MenuButtonOptions : TungDoesMathForYou
{
    /*Settings*/
    public string[] Texts; //0 = IP, 1 = Port

    /*Required components*/
    private ManagerTracker Tracker;
    private NetworkManager TheNetworkManager;

    void Start()
    {
        Tracker = FindObjectOfType<ManagerTracker>();
    }

    //Adds a string into a text field
    public void AddStringToField(string InputLetter)
    {
        //Find all childs within the hierarchy
        List<GameObject> childs = FindAllChilds(transform);

        for (int i = 0; i < childs.Count; ++i)
        {
            MenuManager theMenuManager = childs[i].GetComponent<MenuManager>();
            if (theMenuManager != null)
            {
                if (theMenuManager.Activated)
                {
                    List<GameObject> managerChilds = FindAllChilds(theMenuManager.transform);

                    for(int j = 0; j < managerChilds.Count; ++j)
                    {
                        InputTarget theInputTarget = managerChilds[j].GetComponent<InputTarget>();
                        if (theInputTarget != null && theMenuManager.CurrentID == theInputTarget.transform.parent.GetComponent<MenuButton>().ButtonID) Texts[theInputTarget.TextID] = Texts[theInputTarget.TextID] = Texts[theInputTarget.TextID] + InputLetter;
                    }
                }
            }
        }
    }

    //Adds a string into a text field
    public void RemoveStringToField()
    {
        //Find all childs within the hierarchy
        List<GameObject> childs = FindAllChilds(transform);

        for (int i = 0; i < childs.Count; ++i)
        {
            MenuManager theMenuManager = childs[i].GetComponent<MenuManager>();
            if (theMenuManager != null)
            {
                if (theMenuManager.Activated)
                {
                    List<GameObject> managerChilds = FindAllChilds(theMenuManager.transform);

                    for (int j = 0; j < managerChilds.Count; ++j)
                    {
                        InputTarget theInputTarget = managerChilds[j].GetComponent<InputTarget>();
                        if (theInputTarget != null && Texts[theInputTarget.TextID].Length > 0 && theMenuManager.CurrentID == theInputTarget.transform.parent.GetComponent<MenuButton>().ButtonID) Texts[theInputTarget.TextID] = Texts[theInputTarget.TextID] = Texts[theInputTarget.TextID].Substring(0, Texts[theInputTarget.TextID].Length - 1);
                    }
                }
            }
        }
    }

    //Deactivates all ui objects with the script MenuManager attacted but not the MenuManager the called the function, Only affects objects inside this object's hierarchy
    public void RemoveAllMenu(GameObject IgnoredManager)
    {
        //Find all childs within the hierarchy
        List<GameObject> childs = FindAllChilds(transform);
        
        for(int i = 0; i < childs.Count; ++i)
        {
            //Find all active managers
            MenuManager theMenuManager = childs[i].GetComponent<MenuManager>();
            if(theMenuManager != null)
            {
                if (theMenuManager.gameObject != IgnoredManager)
                {
                    theMenuManager.Deactivate();
                    theMenuManager.gameObject.SetActive(false);
                }
            }
        }
    }

    //Moves the button select up
    public void MenuSelectUp()
    {
        //Find all childs within the hierarchy
        List<GameObject> childs = FindAllChilds(transform);

        for (int i = 0; i < childs.Count; ++i)
        {
            //Find all active managers
            MenuManager theMenuManager = childs[i].GetComponent<MenuManager>();
            if (theMenuManager != null)
            {
                if (theMenuManager.Activated) theMenuManager.CurrentID -= 1;
            }
        }
    }

    //Moves the button select down
    public void MenuSelectDown()
    {
        //Find all childs within the hierarchy
        List<GameObject> childs = FindAllChilds(transform);

        for (int i = 0; i < childs.Count; ++i)
        {
            //Find all active managers
            MenuManager theMenuManager = childs[i].GetComponent<MenuManager>();
            if (theMenuManager != null)
            {
                if (theMenuManager.Activated) theMenuManager.CurrentID += 1;
            }
        }
    }

    //Press the selected button and run the tasks that was assigned
    public void ToggleButtonSelect()
    {
        //Find all childs within the hierarchy
        List<GameObject> childs = FindAllChilds(transform);

        for (int i = 0; i < childs.Count; ++i)
        {
            //Find all active managers
            MenuManager theMenuManager = childs[i].GetComponent<MenuManager>();
            if (theMenuManager != null)
            {
                if (theMenuManager.Activated)
                {
                    //Find all buttons in the active manager
                    List<GameObject> buttons = FindAllChilds(theMenuManager.transform);
                    for (int j = 0; j < buttons.Count; ++j)
                    {
                        MenuButton theMenuButton = buttons[j].GetComponent<MenuButton>();
                        if(theMenuButton != null)
                        {
                            //Run all tasks assigned by the button
                            if (theMenuButton.ButtonID == theMenuManager.CurrentID) theMenuButton.OnToggled.Invoke();
                        }
                    }
                }
            }
        }
    }

    public void EnableObject(GameObject Target)
    {
        Target.SetActive(true);
    }

    //Host server
    public void Host()
    {
        TheNetworkManager.networkPort = int.Parse(Texts[1]);
        TheNetworkManager.StartHost();
    }

    //Connect to server
    public void Join()
    {
        TheNetworkManager.networkAddress = Texts[0];
        TheNetworkManager.networkPort = int.Parse(Texts[1]);
        TheNetworkManager.StartClient();
    }

    //Exits the game
    public void Exit()
    {
        Application.Quit();
    }
}
