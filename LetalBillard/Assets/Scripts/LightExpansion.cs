using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightExpansion : MonoBehaviour {

    private Light light;
    [SerializeField]
    private float apparitionDuration = 0.3f;
    [SerializeField]
    private float lightMaxAngle = 25f;
    [SerializeField]
    private float stayDuration = 2.0f;
    [SerializeField]
    private float fadeOutDuration = 1.0f;

	void Start ()
    {
        light = GetComponent<Light>();
        StartCoroutine(Apparition());
	}

    IEnumerator Apparition()
    {
        float time = 0.0f;
        while(time < apparitionDuration)
        {
            Debug.Log(time + " " + apparitionDuration);
            time += Time.deltaTime;
            light.spotAngle = lightMaxAngle * Mathf.Clamp01(time / apparitionDuration);
            yield return null;
        }
        light.spotAngle = lightMaxAngle;

        yield return new WaitForSeconds(stayDuration);

        time = 0.0f;
        float baseIntensity = light.intensity;
        while (time < fadeOutDuration)
        {
            time += Time.deltaTime;
            light.intensity = baseIntensity * Mathf.Clamp01((fadeOutDuration - time) / fadeOutDuration);
            yield return null;
        }
        light.intensity = 0.0f;

        Destroy(gameObject);
    }
}
