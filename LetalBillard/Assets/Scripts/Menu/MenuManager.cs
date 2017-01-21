using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public GameObject menuContainer;
    public GameObject waitInputMenuContainer;
    public GameObject confirmQuitContainer;
    public Button defaultSelectedMainMenu;
    public Button defaultSelectedQuitMenu;

    void Start()
    {
        defaultSelectedMainMenu.Select();
    }

    public void Play()
    {
        menuContainer.SetActive(false);
        waitInputMenuContainer.SetActive(true);
    }

    public void BackToMain()
    {
        defaultSelectedMainMenu.Select();
        menuContainer.SetActive(true);
        waitInputMenuContainer.SetActive(false);
    }

    public void ConfirmQuit()
    {
        confirmQuitContainer.SetActive(true);
        defaultSelectedQuitMenu.Select();
    }

    public void CloseConfirmMenu()
    {
        confirmQuitContainer.SetActive(false);
        defaultSelectedMainMenu.Select();
    }

	public void Quit()
    {
        Application.Quit();
    }
}
