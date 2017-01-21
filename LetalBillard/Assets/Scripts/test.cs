using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class test : MonoBehaviour {

    public void Rumble(int i)
    {
        StartCoroutine(RumbleEnum(i));
    }


    public IEnumerator RumbleEnum(int i)
    {
        GamePad.SetVibration((PlayerIndex)i, 1.0f, 1.0f);
        yield return new WaitForSeconds(0.5f);
        GamePad.SetVibration((PlayerIndex)i, 0.0f, 0.0f);
    }
}
