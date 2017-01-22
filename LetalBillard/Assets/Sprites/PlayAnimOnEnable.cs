using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimOnEnable : MonoBehaviour
{

    public string animationName = "play";
    public void OnEnable()
    {
        GetComponent<Animator>().Play(animationName);
    }
    
    
}
