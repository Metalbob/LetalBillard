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
    private bool isAnimating = false;
    
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
            StartCoroutine(waitAnimEnd(2.0f, imagePlayer1.gameObject.GetComponent<Animator>()));
            Debug.Log("toto sur un velo");
            imagePlayer2.gameObject.GetComponent<Animator>().SetBool("isUp", true);
            AudioManager.instance.Play(Resources.Load<AudioClip>("Audio/valid"));
        }
        if (Input.GetButtonDown("Submit_Player2") && player1Validation && !isRumbling && !isAnimating)
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
            imagePlayer2.gameObject.GetComponent<Animator>().SetBool("isUp", false);
            StartCoroutine(waitAnimEnd(2.0f, imagePlayer2.gameObject.GetComponent<Animator>()));
            AudioManager.instance.Play(Resources.Load<AudioClip>("Audio/valid"));
        }

        if (Input.GetButtonDown("Cancel"))
        {
            player1Validation = player2Validation = false;
            imagePlayer1.sprite = buttonUp;
            imagePlayer2.sprite = buttonUnavailable;
            backButton.onClick.Invoke();
        }

        if (player1Validation && player2Validation && !isRumbling && !isAnimating)
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
    IEnumerator waitAnimEnd(float time, Animator pAnim)
    {
        isAnimating = true;
        pAnim.SetBool("isPressed", true);
        yield return new WaitForSeconds(time);
        pAnim.Stop();
        isAnimating = false;
    }
}
