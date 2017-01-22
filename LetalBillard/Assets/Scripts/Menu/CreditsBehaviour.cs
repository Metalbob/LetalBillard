using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsBehaviour : MonoBehaviour {

    public float creditsDuration = 5.0f;
    public Image creditSprite;
    public Sprite imgBase;
    public Sprite[] glitchSprite;

    public MenuManager menuMgr;

    public float glitchChance = 20.0f;
    public float glitchTime;
    

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Submit") || Input.GetButtonDown("Cancel") || Input.GetMouseButtonDown(0))
        {
            menuMgr.CloseCredits();
            return;
        }

        if (glitchTime <= 0)
            creditSprite.sprite = imgBase;

        if (Random.Range(0.0f, 100.0f) < glitchChance && glitchTime <= 0)
        {
            glitchTime = Random.Range(0.1f, 0.2f);
            creditSprite.sprite = glitchSprite[Random.Range(0, glitchSprite.Length)];
        }
        if (glitchTime > 0.0f)
            glitchTime -= Time.deltaTime;
    
	}
}
