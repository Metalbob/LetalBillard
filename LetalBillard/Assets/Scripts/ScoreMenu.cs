using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMenu : MonoBehaviour {

    public Transform barPlayerOne;
    public Transform barPlayerTwo;
    public GameObject imageEffect;

    public float animDuration = 1;
    public AnimationCurve scaleCurve;

    public void PlayAnim(int countPlayerOne, int plusPlayerOne, int countPlayerTwo, int plusPlayerTwo)
    {
        SetUpBar(barPlayerOne, countPlayerOne, plusPlayerOne);
        SetUpBar(barPlayerTwo, countPlayerTwo, plusPlayerTwo);
    }

    void SetUpBar(Transform bar, int count, int plus)
    {
        for (int i = 0; i < 5; i++)
        {
            var slot = bar.GetChild(i);
            if (slot.childCount >0)
            {
                DestroyImmediate(slot.GetChild(0).gameObject);
            }
            if (i<count)
            {
                GameObject go = Instantiate(imageEffect, slot.position, Quaternion.identity, slot);
                go.transform.localScale = Vector3.one;
            } else if (i<count + plus)
            {
                GameObject go = Instantiate(imageEffect, slot.position, Quaternion.identity, slot);
                StartCoroutine(Animate(go.transform));
            }
        }
    }

    IEnumerator Animate(Transform t)
    {
        float timer = 0;
        t.localScale = Vector3.one * scaleCurve.Evaluate(timer);
        yield return true;
        while((timer += Time.unscaledDeltaTime) < animDuration)
        {
            t.localScale = Vector3.one * scaleCurve.Evaluate(timer/animDuration);
            yield return true;
        }
        t.localScale = Vector3.one * scaleCurve.Evaluate(1);
    }
}
