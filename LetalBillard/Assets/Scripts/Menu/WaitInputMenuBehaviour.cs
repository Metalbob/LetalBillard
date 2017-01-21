using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaitInputMenuBehaviour : MonoBehaviour {

    private bool player1Validation;
    private bool player2Validation;

    public Image imagePlayer1;
    public Image imagePlayer2;
    public Button backButton;
    
    void Update ()
    {
		if (Input.GetButtonDown("Submit_Player1"))
        {
            player1Validation = true;
            imagePlayer1.color = Color.green;
        }
        if (Input.GetButtonDown("Submit_Player2"))
        {
            player2Validation = true;
            imagePlayer2.color = Color.green;
        }
        if (Input.GetButtonDown("Cancel"))
        {
            player1Validation = player2Validation = false;
            imagePlayer1.color = imagePlayer2.color = Color.white;
            backButton.onClick.Invoke();
        }

        if (player1Validation && player2Validation)
            SceneManager.LoadScene(1);
	}
}
