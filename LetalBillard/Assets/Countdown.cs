using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    [SerializeField]
    private float CountdownFrom = 3;
    private float CountdownTime;
    private bool OnEnabled = false;

    void Update()
    {
        if (OnEnabled)
        {
            CountdownTime -= Time.deltaTime;
        }
        GetComponent<Text>().text = CountdownTime.ToString("0.0");
    }

    void OnDisable()
    {
        CountdownTime = 0;
        OnEnabled = false;
    }
    void OnEnable()
    {
        CountdownTime = CountdownFrom;
        OnEnabled = true;
    }
}
