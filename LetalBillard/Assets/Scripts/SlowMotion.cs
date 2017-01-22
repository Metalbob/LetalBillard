using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour {

    static public SlowMotion instance;

    public IEnumerator currentSlowMo;

	// Use this for initialization
	void Start () {
        instance = this;
	}

    public static bool IsSlowMoActive()
    {
        return instance.currentSlowMo != null;
    }

    public void _SlowMo(float duration, float intensity = 0.1f)
    {
        if (currentSlowMo != null)
        {
            StopCoroutine(currentSlowMo);
        }
        currentSlowMo = SlowMotionCoroutine(duration, intensity);
        StartCoroutine(currentSlowMo);
    }

    IEnumerator SlowMotionCoroutine(float duration, float intensity = 0.1f)
    {
        float timer = 0;
        Time.timeScale = intensity;
        while(timer < duration)
        {
            yield return true; //We could have use Wait for second but i'm planing on addind lerp.    
            timer += Time.deltaTime / intensity;
        }
        Time.timeScale = 1;
        currentSlowMo = null;
    }
	
	public static void SlowMo(float duration, float intensity = 0.1f)
    {
        instance._SlowMo(duration, intensity);
    }
}
