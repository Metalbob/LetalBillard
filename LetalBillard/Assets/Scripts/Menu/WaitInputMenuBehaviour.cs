using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using XInputDotNetPure;

public class WaitInputMenuBehaviour : MonoBehaviour {

    public bool player1Validation;
    public bool player2Validation;

    public Image imagePlayer1;
    public Image imagePlayer2;
    public Button backButton;
    public Sprite buttonUnavailable;
    public Sprite buttonPressed;
    public Sprite buttonUp;
    private bool isRumbling = false;
    
    void Update ()
    {
		if (Input.GetButtonDown("Submit_Player1") && !Input.GetButtonDown("Submit_Player2"))
        {
            int i;
            for (i = 0; i < 4; i++)
            {
                GamePadState state = GamePad.GetState((PlayerIndex)i);
                if (state.Buttons.A == ButtonState.Pressed)
                {
                    if (GamePadManager.instance.Bind(1, i))
                        break;
                }
            }

            if (i == 4)
                return;

            Rumble(0.3f, 0.3f, 1);
            

            player1Validation = true;
            imagePlayer1.sprite = buttonPressed;
            AudioManager.instance.Play(Resources.Load<AudioClip>("Audio/valid"));
            imagePlayer2.sprite = buttonUp;
        }
        if (Input.GetButtonDown("Submit_Player2") && player1Validation && !isRumbling)
        {
            int i;
            for (i = 0; i < 4; i++)
            {
                GamePadState state = GamePad.GetState((PlayerIndex)i);
                if (state.Buttons.A == ButtonState.Pressed)
                {
                    if (GamePadManager.instance.Bind(2, i))
                        break;
                }
            }
            if (i == 4)
                return;
            Rumble(0.3f, 0.3f, 2);

            player2Validation = true;
            imagePlayer2.sprite = buttonPressed;
            AudioManager.instance.Play(Resources.Load<AudioClip>("Audio/valid"));
        }

        if (Input.GetButtonDown("Cancel"))
        {
            player1Validation = player2Validation = false;
            imagePlayer1.sprite = buttonUp;
            imagePlayer2.sprite = buttonUnavailable;
            backButton.onClick.Invoke();
        }

        if (player1Validation && player2Validation && !isRumbling)
            SceneManager.LoadScene(1);
	}


    public void Rumble(float time, float strength, int index)
    {
        StartCoroutine(RumbleEnum(time, strength, index));
    }

    IEnumerator RumbleEnum(float time, float strength, int index)
    {
        isRumbling = true;
        GamePad.SetVibration((PlayerIndex)(GamePadManager.instance.gamePadAssoc[index]), strength, strength);
        yield return new WaitForSeconds(time);
        GamePad.SetVibration((PlayerIndex)(GamePadManager.instance.gamePadAssoc[index]), 0, 0);
        isRumbling = false;
    }
}
