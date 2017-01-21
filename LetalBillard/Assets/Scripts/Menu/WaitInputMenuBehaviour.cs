using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaitInputMenuBehaviour : MonoBehaviour {

    public bool player1Validation;
    public bool player2Validation;

    public Image imagePlayer1;
    public Image imagePlayer2;
    public Button backButton;
    public Sprite buttonPressed;
    public Sprite buttonUp;
    
    void Update ()
    {
		if (Input.GetButtonDown("Submit_Player1"))
        {
            player1Validation = true;
            imagePlayer1.sprite = buttonPressed;
            AudioManager.instance.Play(Resources.Load<AudioClip>("Audio/valid"));
        }
        if (Input.GetButtonDown("Submit_Player2"))
        {
            player2Validation = true;
            imagePlayer2.sprite = buttonPressed;
            AudioManager.instance.Play(Resources.Load<AudioClip>("Audio/valid"));
        }

        if (Input.GetButtonDown("Cancel"))
        {
            player1Validation = player2Validation = false;
            imagePlayer1.sprite = imagePlayer2.sprite = buttonUp;
            backButton.onClick.Invoke();
        }

        if (player1Validation && player2Validation)
            SceneManager.LoadScene(1);
	}
}
