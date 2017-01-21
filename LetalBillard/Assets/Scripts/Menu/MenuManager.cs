using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    public GameObject menuContainer;
    public GameObject waitInputMenuContainer;
    public GameObject confirmQuitContainer;

    public void Play()
    {
        menuContainer.SetActive(false);
        waitInputMenuContainer.SetActive(true);
    }

    public void BackToMain()
    {
        menuContainer.SetActive(true);
        waitInputMenuContainer.SetActive(false);
    }

    public void ConfirmQuit()
    {
        confirmQuitContainer.SetActive(true);
    }

    public void CloseConfirmMenu()
    {
        confirmQuitContainer.SetActive(false);
    }

	public void Quit()
    {
        Application.Quit();
    }
}
