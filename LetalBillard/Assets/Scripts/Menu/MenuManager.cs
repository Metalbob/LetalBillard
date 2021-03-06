﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public GameObject menuContainer;
    public GameObject waitInputMenuContainer;
    public GameObject confirmQuitContainer;
    public GameObject creditsContainer;
    public Button defaultSelectedMainMenu;
    public Button defaultSelectedQuitMenu;
    public AudioClip buttonClip;
    public GameObject background;

    void Start()
    {
        defaultSelectedMainMenu.Select();
    }

    public void Play()
    {
        background.SetActive(false);
        menuContainer.SetActive(false);
        waitInputMenuContainer.SetActive(true);
        AudioManager.instance.Play(buttonClip);
    }

    public void BackToMain()
    {
        background.SetActive(true);
        defaultSelectedMainMenu.Select();
        menuContainer.SetActive(true);
        waitInputMenuContainer.SetActive(false);
        AudioManager.instance.Play(buttonClip);
    }

    public void ConfirmQuit()
    {
        confirmQuitContainer.SetActive(true);
        defaultSelectedQuitMenu.Select();
        AudioManager.instance.Play(buttonClip);
    }

    public void ShowCredits()
    {
        background.SetActive(false);
        menuContainer.SetActive(false);
        creditsContainer.SetActive(true);
    }

    public void CloseCredits()
    {
        background.SetActive(true);
        creditsContainer.SetActive(false);
        menuContainer.SetActive(true);
    }

    public void CloseConfirmMenu()
    {
        confirmQuitContainer.SetActive(false);
        defaultSelectedMainMenu.Select();
        AudioManager.instance.Play(buttonClip);
    }

	public void Quit()
    {
        AudioManager.instance.Play(buttonClip);
        Application.Quit();
    }
}
