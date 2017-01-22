using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour {

    static public SlowMotion instance;

    IEnumerator currentSlowMo;

	// Use this for initialization
	void Start () {
        instance = this;
	}

    public void SlowMo(float duration, float intensity = 0.1f)
    {
        if (currentSlowMo != null)
        {
            StopCoroutine(currentSlowMo);
        }
        currentSlowMo = slowLerp(duration, intensity);
        StartCoroutine(currentSlowMo);
    }

    IEnumerator slowLerp(float duration, float intensity = 0.1f)
    {
        float timer = 0;
        Time.timeScale = intensity;
        Debug.Log("Go " + timer );
        while(timer < duration)
        {
            yield return true; //We could have use Wait for second but i'm planing on addind lerp.    
            timer += Time.deltaTime / intensity;
            Debug.Log(timer);
        }
        Time.timeScale = 1;
        currentSlowMo = null;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
